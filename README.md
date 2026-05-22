[![](https://img.shields.io/nuget/v/soenneker.blazor.interops.floating.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.interops.floating/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.interops.floating/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.interops.floating/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.interops.floating.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.interops.floating/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.interops.floating/codeql.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.interops.floating/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Blazor.Interops.Floating
### A Blazor interop library for Floating UI

## Installation

```bash
dotnet add package Soenneker.Blazor.Interops.Floating
```

## Setup

Register services in `Program.cs`:

```csharp
builder.Services.AddFloatingUiInteropAsScoped();
```

Inject the higher-level utility where you need it:

```csharp
@inject IFloatingUiInterop Floating
```

## Usage

Initialize the package once before first use:

```csharp
await Floating.Initialize();
```
