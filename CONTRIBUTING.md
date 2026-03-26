# Contributing to VSDC Request Submitter V3.0

## Development Setup

```bash
git clone git@github.com:Data-Tech-International/VSDCRequestSubmitterV3.0.git
cd VSDCRequestSubmitterV3.0
dotnet tool restore
dotnet husky install
msbuild VSDCRequestSubmitterV3.0.sln /t:Restore;Build /p:Configuration=Debug
```

**Prerequisites**: Visual Studio 2017+ with .NET Framework 4.6.1 targeting pack, MSBuild on PATH.

## Code Style

Code style is enforced by `.editorconfig` and `dotnet format`. Key rules:

| Rule | Convention |
|---|---|
| Indentation | 4 spaces |
| Classes, properties, methods | `PascalCase` |
| Private fields | `_camelCase` (leading underscore) |
| Brace style | Allman (braces on new lines) |
| Namespaces | Block-scoped (`namespace X { }`, not file-scoped) |
| WinForms controls | `txt` (TextBox), `btn` (Button), `lbl` (Label), `comboBox` (ComboBox) |

**Check formatting** (CI-safe, no modifications):

```bash
dotnet format VSDCRequestSubmitterV3.0.sln --no-restore --verify-no-changes
```

**Fix formatting**:

```bash
dotnet format VSDCRequestSubmitterV3.0.sln --no-restore
```

## Pre-commit Hooks

Managed by [Husky.NET](https://alirezanet.github.io/Husky.Net/). Hooks run automatically — no manual setup beyond `dotnet husky install`.

**Pre-commit** (every commit):

- `dotnet format` on staged `.cs` files (excludes `*.Designer.cs`)
- Secrets scanner — blocks commits containing hardcoded PAC codes, VSDC URLs, or certificate passwords

**Pre-push**:

- Full MSBuild build verification

**Skip in emergencies**:

```bash
git commit --no-verify
```

Configuration lives in `.husky/task-runner.json`.

## Project Structure

```
VSDCRequestSubmitter/
├── Models/InvoiceRequest.cs           # POCO models + enums
├── Proxies/VSDCApiProxy.cs            # HTTP client with X.509 cert auth
├── VSDCRequestSubmitter.cs            # Main WinForms form logic
├── VSDCRequestSubmitter.Designer.cs   # Auto-generated UI (DO NOT edit manually)
├── Properties/                        # Assembly info, settings
├── App.config                         # Runtime configuration
└── packages.config                    # NuGet dependencies
```

## Adding New Code

### New models

Add to `VSDCRequestSubmitter/Models/`, namespace `VSDCRequestSubmitter.Models`.

### New proxies

Add to `VSDCRequestSubmitter/Proxies/`, namespace `VSDCRequestSubmitter.Proxies`.

### New forms

Add to the project root, namespace `VSDCRequestSubmitter`.

### Legacy .csproj requirement

This project uses a legacy (non-SDK-style) `.csproj`. New `.cs` files are **not** automatically included — you must manually add them to `VSDCRequestSubmitterV3.0.csproj`:

```xml
<Compile Include="Models\YourNewModel.cs" />
```

### NuGet packages

Update **both** `packages.config` **and** the `.csproj` `HintPath` references when adding or upgrading packages.

## Security Guidelines

- **NEVER** hardcode PAC codes, certificate names, VSDC URLs, or passwords in source code.
- All sensitive values belong in `App.config`.
- The pre-commit secrets scanner will block commits containing hardcoded secrets.
- Use `SecureString` for certificate passwords where possible.

## Branching & Pull Requests

1. Create feature branches from `main`: `feature/description` or `fix/description`.
2. Keep PRs focused — one feature or fix per PR.
3. Ensure the build passes before submitting (the pre-push hook verifies this).
4. Reference related issues in the PR description (e.g., `Closes #2`).

## AI-Assisted Development

This repository includes Copilot configuration for AI-assisted development.

### Custom Agents (`.github/agents/`)

| Agent | Purpose |
|---|---|
| `@scaffolder` | Generates new .NET classes, proxies, and configuration following project patterns |
| `@security-reviewer` | Reviews code for security issues (certs, secrets, HTTP/TLS, OWASP) |

### Skills (`.github/skills/`)

| Skill | Purpose |
|---|---|
| `dotnet-build` | Build and NuGet troubleshooting |
| `csproj-management` | Project file and config management |
| `certificate-ops` | X.509 certificate operations |
| `vsdc-api` | VSDC API integration reference |

### MCP Servers (`.vscode/mcp.json`)

- **Context7** — Add `use context7` to prompts for live library documentation.
- **GitHub** — PR/issue management from Copilot Chat.

See `AGENTS.md` and `.github/copilot-instructions.md` for full AI tooling details.
