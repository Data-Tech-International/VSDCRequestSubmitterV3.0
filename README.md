# VSDCRequestSubmitter

A .NET Framework 4.6.1 WinForms desktop application for testing invoice signing against Serbia's Tax Authority **VSDC** (Virtual Secure Digital Card) system. Built to help POS developers integrate with the **TaxCore** fiscalization platform by submitting JSON invoice requests to the VSDC API using X.509 client certificate authentication.

## Version and Applicability

This version applies to the **Serbia** environment.

## Prerequisites

| Requirement | Purpose |
|-------------|---------|
| Visual Studio 2017+ (or Build Tools) with **.NET desktop development** workload | Build & run |
| .NET Framework 4.6.1 Developer Pack | Target framework |
| .NET SDK 8.0+ | Tooling (`dotnet format`, Husky.NET) |
| Node.js 18+ | MCP servers (Context7, GitHub) |
| POS PFX certificate + ROOT and Issuing certificates | Client authentication — obtained from Tax Authority |
| PAC (Payment Authorization Code) | Invoice submission — obtained from Tax Authority |

## Getting Started

### 1. Clone the repository

```shell
git clone <repository-url>
cd VSDCRequestSubmitterV3.0
```

### 2. Install certificates

| Certificate | Store | Scope |
|-------------|-------|-------|
| ROOT certificate | Trusted Root Certification Authorities | Local Machine |
| Issuing certificate | Intermediate Certification Authorities | Local Machine |
| POS PFX file | Personal (My) | Current User |

Install certificates using the Windows Certificate Manager (`certmgr.msc` / `certlm.msc`) or follow the guide provided by the Tax Authority.

### 3. Configure App.config

Update the application settings with your environment values:

```xml
<applicationSettings>
    <VSDCRequestSubmitter.Properties.Settings>
      <setting name="VSDCTargetAddress" serializeAs="String">
        <value>REPLACE WITH VSDC URL</value>
      </setting>
      <setting name="CertificateName" serializeAs="String">
        <value>REPLACE WITH POS PFX Certificate Name (Subject CN)</value>
      </setting>
    </VSDCRequestSubmitter.Properties.Settings>
</applicationSettings>
```

Set the **PAC** value in the corresponding App.config setting.

### 4. Set up dev tools

```shell
dotnet tool restore
dotnet husky install
```

### 5. Build

```shell
msbuild VSDCRequestSubmitterV3.0.sln /t:Restore;Build /p:Configuration=Debug
```

### 6. Run

Launch the application, verify the loaded settings, edit the JSON request body as needed, and submit a test invoice.

## Build Commands

```shell
# Debug build
msbuild VSDCRequestSubmitterV3.0.sln /t:Restore;Build /p:Configuration=Debug

# Release build
msbuild VSDCRequestSubmitterV3.0.sln /p:Configuration=Release

# Clean
msbuild VSDCRequestSubmitterV3.0.sln /t:Clean
```

## Architecture

Single-form WinForms application with three layers:

| Layer | Files | Responsibility |
|-------|-------|----------------|
| **UI** | `VSDCRequestSubmitter.cs`, `.Designer.cs` | Main form — loads settings from App.config, provides an editable JSON request body, submits to VSDC |
| **Proxies** | `Proxies/VSDCApiProxy.cs` | HTTP client with X.509 client certificate authentication, POSTs to `/api/v3/invoices` |
| **Models** | `Models/InvoiceRequest.cs` | POCO classes (`InvoiceRequest`, `Item`, `Payment`) and enums |

## VSDC API Reference

**Endpoint:** `POST {VSDCTargetAddress}/api/v3/invoices`

### Headers

| Header | Source | Required |
|--------|--------|----------|
| `Content-Type` | `application/json` | Yes |
| `PAC` | App.config | Yes |
| `Accept-Language` | User selection (`en-US`, `sr-Cyrl-RS`, or omit for default) | No |
| `RequestId` | User input | No |

### Request Body Example

```json
{
  "DateAndTimeOfIssue": "2024-01-01T12:00:00",
  "InvoiceType": 0,
  "TransactionType": 0,
  "Payment": [{ "Amount": 1000.0000, "PaymentType": 1 }],
  "Items": [
    {
      "Name": "Network Cable",
      "Quantity": 1.0,
      "Labels": ["A"],
      "UnitPrice": 100.87,
      "TotalAmount": 100.87
    }
  ]
}
```

### Enums

**InvoiceType**

| Name | Value |
|------|-------|
| Normal | 0 |
| ProForma | 1 |
| Copy | 2 |
| Training | 3 |
| Advance | 4 |

**TransactionType**

| Name | Value |
|------|-------|
| Sale | 0 |
| Refund | 1 |

**PaymentType**

| Name | Value |
|------|-------|
| Other | 0 |
| Cash | 1 |
| Card | 2 |
| Check | 3 |
| WireTransfer | 4 |
| Voucher | 5 |
| MobileMoney | 6 |

### Response

The VSDC response is written to `{ExePath}\Result\Response.Json`.

## Configuration Reference

| Setting | Description | Location |
|---------|-------------|----------|
| `VSDCTargetAddress` | VSDC server URL | App.config |
| `CertificateName` | POS PFX certificate Subject CN | App.config |
| `PAC` | Payment Authorization Code | App.config |

```xml
<applicationSettings>
    <VSDCRequestSubmitter.Properties.Settings>
      <setting name="VSDCTargetAddress" serializeAs="String">
        <value>REPLACE WITH VSDC URL</value>
      </setting>
      <setting name="CertificateName" serializeAs="String">
        <value>REPLACE WITH POS PFX Certificate Name (Subject CN)</value>
      </setting>
    </VSDCRequestSubmitter.Properties.Settings>
</applicationSettings>
```

## Security

- **Client certificate authentication** — X.509 certificate loaded from the `CurrentUser\My` store.
- **No hardcoded secrets** — all sensitive values are stored in `App.config` (excluded from source control).
- **Pre-commit hooks** — Husky.NET hooks scan for accidentally committed secrets.
- **TLS** — all communication with the VSDC API is encrypted in transit.

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines on how to contribute to this project.

## License

See [LICENSE](LICENSE) for details.
