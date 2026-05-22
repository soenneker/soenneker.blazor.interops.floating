using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Blazor.Interops.Floating.Abstract;

/// <summary>
/// A higher-level Blazor utility built on top of <see cref="IFloatingInterop"/>.
/// </summary>
public interface IFloatingUiInterop
{
    /// <summary>
    /// Ensures the underlying JavaScript module has been loaded and is ready for use.
    /// </summary>
    ValueTask Initialize(CancellationToken cancellationToken = default);
}
