[![](https://img.shields.io/nuget/v/soenneker.blazor.interops.floating.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.interops.floating/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.interops.floating/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.interops.floating/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.interops.floating.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.interops.floating/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.interops.floating/codeql.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.interops.floating/actions/workflows/codeql.yml)

# Soenneker.Blazor.Interops.Floating

Provides shared Blazor interop for loading Floating UI browser dependencies.

## Install

```bash
dotnet add package Soenneker.Blazor.Interops.Floating
```

## Quick start

```csharp
using Soenneker.Blazor.Interops.Floating.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddFloatingInteropAsScoped();
```

Adds `IFloatingUiInterop` as a scoped service.

## What you get

- `IFloatingUiInterop` — Provides shared Blazor interop for loading Floating UI browser dependencies.
- `FloatingUiInteropRegistrar` — Registration for the interop and utility services.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `IFloatingUiInterop.Initialize(useCdn, cancellationToken)` | Ensures the Floating UI core and DOM browser globals are available. | A task that completes when the Floating Ui is ready for use. |
| `FloatingUiInteropRegistrar.AddFloatingInteropAsScoped(services)` | Adds `IFloatingUiInterop` as a scoped service. | The same service collection, so additional registrations can be chained. |
| `FloatingUiInteropRegistrar.AddFloatingUiInteropAsScoped(services)` | Adds `IFloatingUiInterop` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
- Dispose instances you own when their scope ends so held resources can be released.
