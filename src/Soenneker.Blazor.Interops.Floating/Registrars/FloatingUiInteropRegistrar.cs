using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Interops.Floating.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Blazor.Interops.Floating.Registrars;

/// <summary>
/// Registration for the interop and utility services.
/// </summary>
public static class FloatingUiInteropRegistrar
{
    /// <summary>
    /// Adds <see cref="IFloatingUiInterop"/> as a scoped service.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddFloatingInteropAsScoped(this IServiceCollection services)
    {
        return services.AddFloatingUiInteropAsScoped();
    }

    /// <summary>
    /// Adds <see cref="IFloatingUiInterop"/> as a scoped service.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddFloatingUiInteropAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped();
        services.TryAddScoped<IFloatingUiInterop, FloatingUiInterop>();

        return services;
    }
}
