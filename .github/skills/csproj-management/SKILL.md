---
name: csproj-management
description: "Use when adding files, NuGet references, or configuration settings to the VSDCRequestSubmitter project. Covers .csproj edits, packages.config, and App.config management."
---

# Project File Management

Project file: `VSDCRequestSubmitter/VSDCRequestSubmitterV3.0.csproj`

## Adding Source Files

This is a legacy .csproj format (not SDK-style) — files are NOT auto-included. Every new `.cs` file must be explicitly added:

```xml
<ItemGroup>
  <Compile Include="Models\NewModel.cs" />
</ItemGroup>
```

Place the entry in the appropriate `<ItemGroup>` near similar files. Models with Models, Proxies with Proxies.

## Adding NuGet Packages

Two files must be updated:

### 1. packages.config
```xml
<package id="PackageName" version="1.0.0" targetFramework="net461" />
```

### 2. .csproj Reference
```xml
<Reference Include="PackageName, Version=1.0.0.0, Culture=neutral, PublicKeyToken=...">
  <HintPath>..\packages\PackageName.1.0.0\lib\net461\PackageName.dll</HintPath>
</Reference>
```

After adding, run `nuget restore` then `msbuild /t:Build` to verify.

## App.config Settings

Application-scoped settings go in the `<applicationSettings>` section:

```xml
<applicationSettings>
  <VSDCRequestSubmitter.Properties.Settings>
    <setting name="NewSettingName" serializeAs="String">
      <value>DEFAULT_VALUE</value>
    </setting>
  </VSDCRequestSubmitter.Properties.Settings>
</applicationSettings>
```

Also update these two files to match:
- `Properties/Settings.settings` — add the setting definition
- `Properties/Settings.Designer.cs` — add the generated property accessor

Access pattern in code:
```csharp
var value = Properties.Settings.Default.NewSettingName;
```

## Adding Forms

WinForms require three files — all must be added to .csproj:

```xml
<Compile Include="NewForm.cs">
  <SubType>Form</SubType>
</Compile>
<Compile Include="NewForm.Designer.cs">
  <DependentUpon>NewForm.cs</DependentUpon>
</Compile>
<EmbeddedResource Include="NewForm.resx">
  <DependentUpon>NewForm.cs</DependentUpon>
</EmbeddedResource>
```
