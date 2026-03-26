---
name: vsdc-api
description: "Use when working with the VSDC Tax Authority API — invoice submission endpoint, request/response format, required headers, and error handling."
---

# VSDC API v3 Integration

## Endpoint

```
POST {VSDCTargetAddress}/api/v3/invoices
```

Base URL is configured in App.config as `VSDCTargetAddress`.

## Required Headers

| Header | Source | Notes |
|--------|--------|-------|
| `Content-Type` | Hardcoded | `application/json` |
| `PAC` | App.config | Payment Authorization Code from Tax Authority |
| `Accept-Language` | User selection | `en-US`, `sr-Cyrl-RS`, or omit for default |
| `RequestId` | User input | Optional unique request identifier |

## Invoice Request Schema

```json
{
  "DateAndTimeOfIssue": "2024-01-01T12:00:00",
  "Cashier": "string (optional)",
  "BuyerId": "string (optional)",
  "BuyerCostCenterId": "string (optional)",
  "InvoiceType": 0,
  "TransactionType": 0,
  "Payment": [
    {
      "Amount": 1000.0000,
      "PaymentType": 0
    }
  ],
  "InvoiceNumber": "string (optional)",
  "ReferentDocumentNumber": "string (optional)",
  "ReferentDocumentDT": "datetime (optional)",
  "Items": [
    {
      "GTIN": "string (barcode, optional)",
      "Name": "string",
      "Quantity": 1.0,
      "Discount": 0.0,
      "Labels": ["A"],
      "UnitPrice": 100.87,
      "TotalAmount": 100.87
    }
  ]
}
```

## Enums

**InvoiceType**: Normal (0), ProForma (1), Copy (2), Training (3), Advance (4)

**TransactionType**: Sale (0), Refund (1)

**PaymentType**: Other (0), Cash (1), Card (2), Check (3), WireTransfer (4), Voucher (5), MobileMoney (6)

## Decimal Precision

`Payment.Amount` is rounded to 4 decimal places automatically. Apply the same precision to any new monetary fields.

## Response Handling

The API response (JSON) is written to:
```
{Application.ExecutablePath}\Result\Response.Json
```

The `Result` directory is created if it doesn't exist before writing.

## Error Handling Pattern

```csharp
try
{
    var response = await client.PostAsJsonAsync(
        VSDCAddress + "/api/v3/invoices", request);
    var result = await response.Content.ReadAsStringAsync();
    // write result to file
}
catch (HttpRequestException ex)
{
    // Network or TLS error — check certificate and URL
}
catch (TaskCanceledException ex)
{
    // Timeout — VSDC server may be unreachable
}
```

## Language Support

Three options for `Accept-Language`:
- `default` — omit header entirely (server default)
- `en-US` — English
- `sr-Cyrl-RS` — Serbian Cyrillic
