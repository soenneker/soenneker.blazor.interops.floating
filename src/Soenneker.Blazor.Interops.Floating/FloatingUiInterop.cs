using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.Interops.Floating.Abstract;

namespace Soenneker.Blazor.Interops.Floating;

/// <inheritdoc cref="IFloatingUiInterop"/>
public sealed class FloatingUiInterop : IFloatingUiInterop
{
    private readonly IFloatingInterop _interop;

    public FloatingUiInterop(IFloatingInterop interop)
    {
        _interop = interop ?? throw new ArgumentNullException(nameof(interop));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        return _interop.Initialize(cancellationToken);
    }
}
