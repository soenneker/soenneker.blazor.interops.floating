using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Blazor.Interops.Floating.Abstract;

/// <summary>
/// Blazor interop for browser-facing functionality exposed by this package.
/// </summary>
public interface IFloatingInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures the JavaScript module for this package has been loaded and initialized.
    /// </summary>
    ValueTask Initialize(CancellationToken cancellationToken = default);
}
