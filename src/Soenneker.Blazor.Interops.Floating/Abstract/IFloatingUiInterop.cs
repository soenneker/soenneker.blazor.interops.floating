using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Blazor.Interops.Floating.Abstract;

/// <summary>
/// Provides shared Blazor interop for loading Floating UI browser dependencies.
/// </summary>
public interface IFloatingUiInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures the Floating UI core and DOM browser globals are available.
    /// </summary>
    /// <param name="useCdn">Whether to load Floating UI from CDN or from this package's static web assets.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    ValueTask Initialize(bool useCdn = true, CancellationToken cancellationToken = default);
}
