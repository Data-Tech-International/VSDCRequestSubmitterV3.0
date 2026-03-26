---
name: scaffolder
description: "Scaffolds new .NET classes, interfaces, proxies, and configuration entries for the VSDCRequestSubmitter project following its established patterns."
tools:
  - read
  - edit
  - search
  - shell
---

Scaffolds new .NET classes, interfaces, proxies, and configuration entries for the VSDCRequestSubmitter project, following its established patterns and conventions.

## Instructions

### Project structure

- New model classes go in `VSDCRequestSubmitter/Models/`.
- New proxy classes go in `VSDCRequestSubmitter/Proxies/`.
- New WinForms forms go in `VSDCRequestSubmitter/` alongside `VSDCRequestSubmitter.cs`.

### Namespace convention

Match the folder path:

- `VSDCRequestSubmitter` — root namespace for forms and top-level classes.
- `VSDCRequestSubmitter.Models` — all model classes.
- `VSDCRequestSubmitter.Proxies` — all proxy/HTTP client classes.

### Model scaffolding

- Create POCO classes with auto-properties.
- Use `List<T>` for collections, initialized at declaration: `public List<Item> Items { get; set; } = new List<Item>();`
- Where decimal precision matters, use a backing field with rounding in the setter (4 decimal places), matching the `Payment.Amount` pattern:

```csharp
private decimal _amount;
public decimal Amount
{
    get { return _amount; }
    set { _amount = Math.Round(value, 4); }
}
```

### Proxy scaffolding

- Use `HttpClient` with `WebRequestHandler` for certificate-based authentication.
- Set `Content-Type: application/json` on requests.
- Include these headers per the existing pattern in `VSDCApiProxy.cs`:
  - `PAC` — point-of-sale activation code.
  - `Accept-Language` — locale header.
  - `RequestId` — unique request identifier.
- Read base URLs and certificate paths from `Settings.Default`.

### Configuration

- Add new application-scoped settings to `App.config` under the `VSDCRequestSubmitter.Properties.Settings` section.
- Update `Properties/Settings.settings` and `Properties/Settings.Designer.cs` to match.
- Access settings via `Properties.Settings.Default.SettingName`.

### NuGet packages

- Add entries to `packages.config` using `targetFramework="net461"`.
- Add corresponding `<Reference>` elements in `VSDCRequestSubmitter.csproj` pointing to the `packages/` folder with the correct `HintPath`.

### JSON serialization

- Use `Newtonsoft.Json` (v12.0.1) for all JSON work.
- Always serialize with `Formatting.Indented`.

### Naming conventions

- **PascalCase** for classes, properties, and methods.
- **_camelCase** for private fields (leading underscore).
- **WinForms controls**: prefix with `txt` (TextBox), `btn` (Button), `lbl` (Label), `comboBox` (ComboBox).

### After scaffolding

- Remind the user to add any new `.cs` files to `VSDCRequestSubmitter.csproj` under the appropriate `<Compile Include="...">` item group if they were not auto-included.
- Verify the build succeeds: `msbuild VSDCRequestSubmitterV3.0.sln /t:Restore;Build /p:Configuration=Debug`
