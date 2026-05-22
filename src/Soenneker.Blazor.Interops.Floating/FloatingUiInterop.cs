using System.Threading;
using System.Threading.Tasks;
using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Interops.Floating.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;

namespace Soenneker.Blazor.Interops.Floating;

/// <inheritdoc cref="IFloatingUiInterop"/>
public sealed class FloatingUiInterop : IFloatingUiInterop
{
    private const string _floatingUiCoreCdnPath =
        "https://cdn.jsdelivr.net/npm/@floating-ui/core@1.7.2/dist/floating-ui.core.umd.min.js";

    private const string _floatingUiDomCdnPath =
        "https://cdn.jsdelivr.net/npm/@floating-ui/dom@1.7.2/dist/floating-ui.dom.umd.min.js";

    private const string _floatingUiCoreIntegrity = "sha256-OhWDdFHrIg8eNZaNgWL2ax7tjKNFOBQq3WErqxfHdlQ=";
    private const string _floatingUiDomIntegrity = "sha256-cycZmidLw+l9uWDr4bUhL26YMJg1G6aM0AnUEPG9sME=";

    private const string _floatingUiCoreLocalPath =
        "_content/Soenneker.Blazor.Interops.Floating/js/floating-ui.core.umd.min.js";

    private const string _floatingUiDomLocalPath =
        "_content/Soenneker.Blazor.Interops.Floating/js/floating-ui.dom.umd.min.js";

    private readonly IResourceLoader _resourceLoader;
    private readonly AsyncInitializer<bool> _scriptInitializer;
    private readonly CancellationScope _cancellationScope = new();

    public FloatingUiInterop(IResourceLoader resourceLoader)
    {
        _resourceLoader = resourceLoader;
        _scriptInitializer = new AsyncInitializer<bool>(InitializeScripts);
    }

    public async ValueTask Initialize(bool useCdn = true, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
            await _scriptInitializer.Init(useCdn, linked);
    }

    private async ValueTask InitializeScripts(bool useCdn, CancellationToken token)
    {
        if (useCdn)
        {
            await _resourceLoader.LoadScriptAndWaitForVariable(_floatingUiCoreCdnPath, "FloatingUICore",
                _floatingUiCoreIntegrity, cancellationToken: token);
            await _resourceLoader.LoadScriptAndWaitForVariable(_floatingUiDomCdnPath, "FloatingUIDOM",
                _floatingUiDomIntegrity, cancellationToken: token);
            return;
        }

        await _resourceLoader.LoadScriptAndWaitForVariable(_floatingUiCoreLocalPath, "FloatingUICore",
            cancellationToken: token);
        await _resourceLoader.LoadScriptAndWaitForVariable(_floatingUiDomLocalPath, "FloatingUIDOM",
            cancellationToken: token);
    }

    public async ValueTask DisposeAsync()
    {
        await _scriptInitializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}