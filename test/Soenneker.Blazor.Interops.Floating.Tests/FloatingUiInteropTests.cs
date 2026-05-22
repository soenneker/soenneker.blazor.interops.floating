using Soenneker.Blazor.Interops.Floating.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Blazor.Interops.Floating.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class FloatingUiInteropTests : HostedUnitTest
{
    private readonly IFloatingUiInterop _blazorlibrary;

    public FloatingUiInteropTests(Host host) : base(host)
    {
        _blazorlibrary = Resolve<IFloatingUiInterop>(true);
    }

    [Test]
    public void Default()
    {

    }
}
