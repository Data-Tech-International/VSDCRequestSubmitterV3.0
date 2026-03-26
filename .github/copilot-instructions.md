# Copilot Instructions

## Project Overview

VSDCRequestSubmitter is a .NET Framework 4.6.1 WinForms desktop application that helps POS developers test invoice signing against Serbia's Tax Authority VSDC system. It POSTs JSON invoice requests to the VSDC API using client certificate authentication.

## Build

```shell
# Restore NuGet packages and build
msbuild VSDCRequestSubmitterV3.0.sln /t:Restore;Build /p:Configuration=Debug

# Release build
msbuild VSDCRequestSubmitterV3.0.sln /p:Configuration=Release
```

There are no tests in this project.

## Formatting & Hooks

```shell
# First-time setup after cloning
dotnet tool restore
dotnet husky install

# Manual format check
dotnet format VSDCRequestSubmitterV3.0.sln --no-restore --verify-no-changes

# Manual format fix
dotnet format VSDCRequestSubmitterV3.0.sln --no-restore
```

Pre-commit hooks automatically run `dotnet format` on staged `.cs` files and scan for hardcoded secrets. Pre-push hooks verify the build passes. Code style rules are in `.editorconfig`.

## Architecture

The app is a single-form WinForms application with three layers:

- **UI** (`VSDCRequestSubmitter.cs` + `.Designer.cs`) — Main form where users edit a JSON invoice request and submit it. Settings (VSDC URL, certificate name, PAC) are loaded from `App.config` on startup.
- **Proxies** (`Proxies/VSDCApiProxy.cs`) — HTTP client that authenticates via X.509 client certificate (loaded from the current user's certificate store by Subject CN) and POSTs to `/api/v3/invoices`. Custom headers include `PAC`, `Accept-Language`, and optional `RequestId`.
- **Models** (`Models/InvoiceRequest.cs`) — POCO classes (`InvoiceRequest`, `Item`, `Payment`) and enums (`InvoiceType`, `TransactionType`, `PaymentType`) representing the VSDC invoice schema.

The response is written to `{ExePath}\Result\Response.Json`.

## Key Conventions

- **NuGet package management**: Uses `packages.config` (not PackageReference). Dependencies include `Newtonsoft.Json` 12.0.1 for JSON serialization.
- **JSON formatting**: All JSON output uses `Newtonsoft.Json` with `Formatting.Indented`.
- **Decimal precision**: `Payment.Amount` auto-rounds to 4 decimal places via the setter.
- **Configuration**: Three application-scoped settings in `App.config` — `VSDCTargetAddress`, `CertificateName`, `PAC`. Accessed through the generated `Settings.Designer.cs` class.
- **WinForms controls**: Prefixed by type (`txt`, `btn`, `lbl`, `comboBox`). UI layout uses anchor styles for responsive resizing.
- **Namespaces**: Root `VSDCRequestSubmitter`, with `.Models` and `.Proxies` sub-namespaces matching folder structure.
- **No hardcoded secrets**: All sensitive values (URL, certificate CN, PAC) are externalized to `App.config`.

## MCP Servers

Two MCP servers are configured in `.vscode/mcp.json`:

- **Context7** — Fetches live, version-specific documentation for libraries (Newtonsoft.Json, System.Net.Http, etc.). Add `use context7` to your prompt for up-to-date API references.
- **GitHub** — Access PRs, issues, and code search directly from Copilot Chat. Requires a GitHub PAT (prompted on first use).
