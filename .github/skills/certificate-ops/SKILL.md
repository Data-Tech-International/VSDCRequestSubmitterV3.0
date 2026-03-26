---
name: certificate-ops
description: "Use when working with X.509 certificates, PFX files, or certificate store operations in this VSDC integration project."
---

# Certificate Operations

This application uses X.509 client certificate authentication to communicate with the VSDC Tax Authority API.

## Certificate Store Pattern

Load a certificate by Subject CN from the current user's personal store:

```csharp
private static X509Certificate2 LoadMyCertificate(string cn)
{
    using (var store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
    {
        store.Open(OpenFlags.ReadOnly);
        var certs = store.Certificates.Find(
            X509FindType.FindBySubjectName, cn, validOnly: false);

        if (certs.Count == 0)
            throw new InvalidOperationException(
                $"Certificate with Subject CN '{cn}' not found in CurrentUser\\My store.");

        return certs[0];
    }
}
```

Key points:
- Always use `using` or `Dispose()` on `X509Store`
- Store location: `CurrentUser` (not `LocalMachine` — avoids needing admin privileges)
- Store name: `My` (personal certificate store)
- The `cn` value comes from App.config setting `CertificateName`

## Attaching Certificate to HttpClient

```csharp
var handler = new WebRequestHandler();
handler.ClientCertificateOptions = ClientCertificateOptions.Manual;
handler.ClientCertificates.Add(LoadMyCertificate(cn));

using (var client = new HttpClient(handler))
{
    // make request
}
```

## Certificate Prerequisites

Before the app can authenticate:
1. ROOT certificate from Tax Authority → install to machine `Trusted Root Certification Authorities` store
2. Issuing (intermediate) certificate → install to machine `Intermediate Certification Authorities` store
3. POS PFX file → import to `CurrentUser\My` (Personal) store using the password provided by Tax Authority

## Validation Checklist

When reviewing or writing certificate code:
- [ ] `X509Store` is disposed (using block)
- [ ] Empty certificate collection is handled (not null, but `.Count == 0`)
- [ ] `ServerCertificateValidationCallback` does NOT blindly return `true`
- [ ] No PFX passwords hardcoded in source — use App.config or SecureString
- [ ] Certificate is loaded once and reused per request, not per-call store lookup in a loop
