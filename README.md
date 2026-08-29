[![](https://img.shields.io/nuget/v/soenneker.blazor.interops.floating.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.interops.floating/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.interops.floating/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.interops.floating/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.interops.floating.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.interops.floating/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.interops.floating/codeql.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.interops.floating/actions/workflows/codeql.yml)

# Soenneker.Blazor.Interops.Floating

A small shared loader for the browser builds of Floating UI Core and Floating UI DOM. It makes the `FloatingUICore` and `FloatingUIDOM` JavaScript globals available to other Blazor interop libraries.

This package does not expose `computePosition` or render a tooltip, popover, or floating window by itself. Application code looking for a ready-made component should use a higher-level package such as `Soenneker.Blazor.Floating.Tooltips`.

## Installation

```bash
dotnet add package Soenneker.Blazor.Interops.Floating
```

## Registration

```csharp
using Soenneker.Blazor.Interops.Floating.Registrars;

builder.Services.AddFloatingUiInteropAsScoped();
```

`AddFloatingInteropAsScoped()` is retained as an alias for the same registration.

## Initialize the browser dependencies

Inject `IFloatingUiInterop` into the component or interop service that needs the JavaScript globals, then initialize after browser interop is available:

```razor
@using Soenneker.Blazor.Interops.Floating.Abstract
@inject IFloatingUiInterop FloatingUi

@code {
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await FloatingUi.Initialize(useCdn: false);
    }
}
```

With `useCdn: true` (the default), pinned Floating UI scripts are loaded from jsDelivr with integrity checks. With `false`, the package's static web assets are loaded instead. The scoped loader initializes once, so all consumers in the same scope should use the same source choice; the first successful call determines it.

Initialization is safe to call repeatedly and concurrently. Cancellation stops a pending caller but does not unload scripts already added to the page. Disposing the scoped service releases its .NET initialization resources; browser globals remain on the page.
