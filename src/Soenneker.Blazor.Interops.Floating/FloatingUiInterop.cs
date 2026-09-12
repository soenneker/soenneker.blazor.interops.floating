using Soenneker.Asyncs.Locks;
using System;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Asyncs.Initializers;
using Soenneker.Atomics.ValueBools;
using Soenneker.Blazor.Interops.Floating.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;

namespace Soenneker.Blazor.Interops.Floating;

public sealed class FloatingUiInterop : IFloatingUiInterop
{
    private const string _floatingUiCoreCdnPath =
        "https://cdn.jsdelivr.net/npm/@floating-ui/core@1.8.0/dist/floating-ui.core.umd.min.js";

    private const string _floatingUiDomCdnPath =
        "https://cdn.jsdelivr.net/npm/@floating-ui/dom@1.8.0/dist/floating-ui.dom.umd.min.js";

    private const string _floatingUiCoreIntegrity = "sha256-ZZQNhmprbYMTlKS77ZntCjkzC6yYtkM4bZovh6Gh1do=";
    private const string _floatingUiDomIntegrity = "sha256-YaRvlDxOmTeerwc0R4Ea7H6LjxINL2RjFuAbdpS8kKM=";

    private const string _floatingUiCoreLocalPath =
        "_content/Soenneker.Blazor.Interops.Floating/js/floating-ui.core.umd.min.js";

    private const string _floatingUiDomLocalPath =
        "_content/Soenneker.Blazor.Interops.Floating/js/floating-ui.dom.umd.min.js";

    private readonly IResourceLoader _resourceLoader;
    private readonly AsyncInitializer<bool> _scriptInitializer;
    private readonly CancellationTokenSource _lifetimeCancellation = new();
    private readonly CancellationToken _lifetimeToken;
    private ValueAtomicBool _disposed;
    private readonly AsyncLock _lifetimeGate = new();

    public FloatingUiInterop(IResourceLoader resourceLoader)
    {
        _lifetimeToken = _lifetimeCancellation.Token;
        _resourceLoader = resourceLoader ?? throw new ArgumentNullException(nameof(resourceLoader));
        _scriptInitializer = new AsyncInitializer<bool>(InitializeScripts);
    }

    public ValueTask Initialize(bool useCdn = true, CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_disposed.Value, this);
        cancellationToken.ThrowIfCancellationRequested();
        return _scriptInitializer.IsInitialized ? ValueTask.CompletedTask : InitializeCore(useCdn, cancellationToken);
    }

    private async ValueTask InitializeCore(bool useCdn, CancellationToken cancellationToken)
    {
        CancellationToken linked =
            GetLifetimeToken().Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
            await _scriptInitializer.Init(useCdn, linked);
    }

    private async ValueTask InitializeScripts(bool useCdn, CancellationToken token)
    {
        if (useCdn)
        {
            await _resourceLoader.LoadScript(_floatingUiCoreCdnPath,
                _floatingUiCoreIntegrity, cancellationToken: token);
            await _resourceLoader.LoadScript(_floatingUiDomCdnPath,
                _floatingUiDomIntegrity, cancellationToken: token);
            return;
        }

        // These UMD bundles publish their globals synchronously before the load event.
        // DOM depends on core, so preserve this order without polling either global.
        await _resourceLoader.LoadScript(_floatingUiCoreLocalPath,
            cancellationToken: token);
        await _resourceLoader.LoadScript(_floatingUiDomLocalPath,
            cancellationToken: token);
    }

    private CancellationToken GetLifetimeToken()
    {
        using (_lifetimeGate.LockSync())
        {
            ObjectDisposedException.ThrowIf(_disposed.Value, this);
            return _lifetimeToken;
        }
    }

    private async ValueTask CancelLifetime()
    {
        try
        {
            await _lifetimeCancellation.CancelAsync().ConfigureAwait(false);
        }
        catch
        {
            // A cancellation callback must not prevent reference cleanup.
        }
        finally
        {
            _lifetimeCancellation.Dispose();
        }
    }

    public async ValueTask DisposeAsync()
    {
        using (await _lifetimeGate.Lock().ConfigureAwait(false))
        {
            if (!_disposed.TrySetTrue())
                return;
        }

        // Cancel an in-flight load before waiting for the initializer's gate.
        await CancelLifetime();
        await _scriptInitializer.DisposeAsync();
    }
}
