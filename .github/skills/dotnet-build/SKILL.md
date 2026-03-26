---
name: dotnet-build
description: "Use when building, restoring, or troubleshooting the .NET Framework 4.6.1 solution. Covers MSBuild commands, NuGet restore via packages.config, and binding redirect issues."
---

# .NET Framework Build

## Solution

Single solution: `VSDCRequestSubmitterV3.0.sln` with one project in `VSDCRequestSubmitter/`.

Target framework: .NET Framework 4.6.1 (`net461`).

## Build Commands

Restore and build:
```shell
msbuild VSDCRequestSubmitterV3.0.sln /t:Restore;Build /p:Configuration=Debug
```

Release build:
```shell
msbuild VSDCRequestSubmitterV3.0.sln /p:Configuration=Release
```

Clean:
```shell
msbuild VSDCRequestSubmitterV3.0.sln /t:Clean
```

## NuGet Restore

This project uses `packages.config` (not PackageReference). NuGet packages are restored to a `packages/` folder at the solution root.

If restore fails, try:
```shell
nuget restore VSDCRequestSubmitterV3.0.sln
```

## Binding Redirects

`App.config` contains assembly binding redirects for:
- `System.Net.Http` → 4.1.1.3
- `Newtonsoft.Json` → 12.0.0.0

If adding or upgrading a NuGet package causes runtime `FileLoadException` or `MissingMethodException`, check whether a new binding redirect is needed in `App.config` under `<runtime><assemblyBinding>`.

## Common Issues

- **Missing MSBuild**: Ensure Visual Studio Build Tools or Visual Studio with .NET desktop workload is installed. MSBuild is at `C:\Program Files\Microsoft Visual Studio\2022\...\MSBuild\Current\Bin\MSBuild.exe`.
- **packages/ not found**: Run `nuget restore` before `msbuild`. The `packages.config` file lists all dependencies.
- **Target framework not installed**: .NET Framework 4.6.1 Developer Pack must be installed.
