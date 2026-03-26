# AGENTS.md

Instructions for AI coding agents (Codex, Jules, OpenCode) working in this repository.
See `.github/copilot-instructions.md` for additional project-specific guidance.

## Project overview

VSDCRequestSubmitter is a .NET Framework 4.6.1 WinForms desktop application for testing invoice signing against Serbia's Tax Authority VSDC system. It POSTs JSON invoice requests to the VSDC API (`/api/v3/invoices`) using X.509 client certificate authentication.

## Build

```
msbuild VSDCRequestSubmitterV3.0.sln /t:Restore;Build /p:Configuration=Debug
```

No test suite exists. Code formatting is enforced via pre-commit hooks:

```
dotnet tool restore && dotnet husky install
```

Pre-commit hooks (Husky.NET) run `dotnet format` on staged `.cs` files and scan for hardcoded secrets. Pre-push hooks verify the build.

## Architecture

The solution contains a single project (`VSDCRequestSubmitter/`) with three layers:

| Layer | File(s) | Responsibility |
|-------|---------|----------------|
| **UI** | `VSDCRequestSubmitter.cs` + `.Designer.cs` | WinForms main form — user input, request composition, response display |
| **Proxies** | `Proxies/VSDCApiProxy.cs` | HTTP client — loads X.509 cert, attaches custom headers (PAC, Accept-Language, RequestId), POSTs JSON to VSDC API |
| **Models** | `Models/InvoiceRequest.cs` | POCO classes: `InvoiceRequest`, `Item`, `Payment` + enums (`InvoiceType`, `TransactionType`, `PaymentType`) |

## Conventions

- **Namespaces:** `VSDCRequestSubmitter`, `VSDCRequestSubmitter.Models`, `VSDCRequestSubmitter.Proxies`
- **Naming:** PascalCase for public members, `_camelCase` for private fields
- **WinForms controls:** Prefixed — `txt`, `btn`, `lbl`, `comboBox`
- **JSON serialization:** Newtonsoft.Json with `Formatting.Indented`
- **Payment.Amount:** Auto-rounds to 4 decimal places
- **Configuration:** Application-scoped settings in `App.config` (`VSDCTargetAddress`, `CertificateName`, `PAC`), accessed via `Settings.Designer.cs`
- **NuGet:** Uses `packages.config` (not PackageReference) — Newtonsoft.Json 12.0.1, System.Net.Http, cryptography libs
- **Binding redirects:** Present in `App.config` for `System.Net.Http` and `Newtonsoft.Json`

## Security

- **Certificate auth:** X.509 client certificate loaded from `CurrentUser\My` store by Subject CN (configured in `App.config` as `CertificateName`)
- **No hardcoded secrets:** All sensitive values (PAC, certificate name, target address) are externalized to `App.config`
- **Custom headers:** Each request includes `PAC`, `Accept-Language`, and a unique `RequestId`

## Hooks

### Git hooks (Husky.NET)

Pre-commit and pre-push hooks are managed by Husky.NET. After cloning:

```
dotnet tool restore
dotnet husky install
```

| Hook | Task | What it does |
|------|------|-------------|
| pre-commit | `dotnet-format-staged` | Formats staged `.cs` files (excludes `*.Designer.cs`) |
| pre-commit | `check-secrets` | Scans for hardcoded PAC codes, URLs, cert passwords |
| pre-push | `build-verify` | MSBuild debug build verification |

Skip hooks in emergencies: `git commit --no-verify`

Configuration: `.husky/task-runner.json`, `.config/dotnet-tools.json`

### Copilot agent hooks

Pre-tool-use hooks in `.github/hooks/hooks.json` protect critical files from accidental agent operations:
- Blocks deletion of `.sln`, `.csproj`, `App.config`, `packages.config`
- Blocks destructive `rm -rf` on project root or `.git`
- Blocks modifications to `.github/hooks/` (self-protection)

## Custom agents

Copilot custom agents are available in `.github/agents/`:

- **`scaffolder`** — .NET scaffolding (new models, forms, proxy methods). Tools: read, edit, search, shell.
- **`security-reviewer`** — Security analysis (certificate handling, secret management, HTTP configuration). Tools: read, search, shell.

## Skills

Modular skills in `.github/skills/` are loaded on-demand when relevant:

| Skill | When to use |
|-------|------------|
| **`dotnet-build`** | Building, restoring, or troubleshooting MSBuild and NuGet issues |
| **`csproj-management`** | Adding files, NuGet references, App.config settings, or WinForms to the project |
| **`certificate-ops`** | X.509 certificate store operations, PFX handling, cert validation |
| **`vsdc-api`** | VSDC Tax Authority API endpoint format, headers, JSON schema, error handling |

## MCP servers

Two MCP servers are configured in `.vscode/mcp.json` (requires Node.js 18+):

| Server | Package | Purpose |
|--------|---------|---------|
| **Context7** | `@upstash/context7-mcp` | Live, version-specific library documentation. Add `use context7` to prompts for current API references. |
| **GitHub** | `@modelcontextprotocol/server-github` | PR management, issue tracking, code search. Requires GitHub PAT (prompted on first use). |
