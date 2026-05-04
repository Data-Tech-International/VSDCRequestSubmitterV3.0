# VSDCRequestSubmitter

## Project Description

The **VSDCRequestSubmitter** application is a .NET Framework Windows Forms utility for electronically signing and submitting fiscal invoice requests to the VSDC (Virtual Signed Digital Certificate) system operated by the Tax Authority.

The main purpose is to provide Point of Sale (POS) developers and administrators with an easy-to-use tool to:
- Prepare and submit electronic invoice requests to the VSDC system
- Sign invoices using POS-specific digital certificates
- Integrate with TaxCore fiscalization infrastructure
- Retrieve and display fiscal signatures from the Tax Authority
- Support multi-country tax compliance workflows

The system is designed for **multi-country deployment** with initial support for Serbia, providing a simple interface for POS developers integrating with tax authority systems.

---

## Quick Start

1. **Prerequisites**: Obtain POS PFX certificate file, ROOT, and Issuing certificates from the Tax Authority
2. **Installation**: Install certificates and configure the application
3. **Configuration**: Set VSDC target address and certificate details in app.config
4. **Usage**: Prepare invoice request and submit for signature
5. **Results**: View signed invoice results via the response link

For detailed setup instructions, see [Setting up a Development Environment](#setting-up-a-development-environment) below.

---

## Table of Contents

- [Project Description](#project-description)
- [Quick Start](#quick-start)
- [Version and Applicability](#version-and-applicability)
- [Tech / Framework](#tech--framework)
- [Setting up a Development Environment](#setting-up-a-development-environment)
- [Build and Run](#build-and-run)
- [Configuration](#configuration)
- [Troubleshooting](#troubleshooting)
- [Credits](#credits)

---

## Version and Applicability

This version applies to the following environments:

- **Serbia** — Full support for Serbian tax authority VSDC system

---

## Tech / Framework

- .NET Framework 4.8
- C# 7.0+
- Windows Forms
- X.509 Digital Certificates (PFX)
- SOAP Web Services (TaxCore VSDC)

---

## Setting up a Development Environment

### Prerequisites

- Visual Studio 2019 or later (or Visual Studio 2025+)
- .NET Framework 4.8 SDK
- NuGet package manager
- POS PFX certificate file (obtained from Tax Authority)
- ROOT and Issuing certificates (obtained from Tax Authority)
- Windows certificate store access for certificate installation
- Access to VSDC system endpoint (provided by Tax Authority)

### Instructions

1. **Clone or download the repository** from the source control system
2. **Open the solution** — Open `VSDCRequestSubmitterV3.0.sln` in Visual Studio
3. **Restore NuGet packages**:
   ```bash
   dotnet restore
   ```
4. **Install SSL/TLS certificates**:
   - Import ROOT certificate to Machine Personal certificate store
   - Import Issuing certificate to Machine Personal certificate store
   - Import POS PFX file to Current User Personal certificate store
   - Follow the guide provided by the Tax Authority for certificate installation procedures

5. **Configure application settings** — Edit `App.config` and set:
   - `VSDCTargetAddress`: The VSDC web service endpoint URL
   - `CertificateName`: The Subject CN of your POS PFX certificate
   - `PACCode`: The PAC code provided with your PFX file
   - `CertificatePassword`: The password for your PFX file (secure storage recommended)

   See [Configuration](#configuration) section for details.

6. **Build the solution**:
   ```bash
   msbuild VSDCRequestSubmitterV3.0.sln /p:Configuration=Debug
   ```

7. **Run the application** — Execute the compiled `.exe` from Visual Studio or directly

---

## Build and Run

**Build (Debug):**
```bash
dotnet restore
msbuild VSDCRequestSubmitterV3.0.sln /p:Configuration=Debug
```

**Build (Release):**
```bash
dotnet restore
msbuild VSDCRequestSubmitterV3.0.sln /p:Configuration=Release
```

**Run the application:**
- From Visual Studio: Press F5 or select Debug → Start Debugging
- From command line: Navigate to the output directory and run `VSDCRequestSubmitter.exe`

---

## Configuration

To avoid constant manual configuration through the application interface, configure the application by editing the `App.config` file:

```xml
<applicationSettings>
    <VSDCRequestSubmitter.Properties.Settings>
      <setting name="VSDCTargetAddress" serializeAs="String">
        <value>https://vsdc.tax-authority.rs/service</value>
      </setting>
      <setting name="CertificateName" serializeAs="String">
        <value>CN=YourPOSName, OU=Organization, O=Company, C=RS</value>
      </setting>
      <setting name="PACCode" serializeAs="String">
        <value>YOUR_PAC_CODE</value>
      </setting>
    </VSDCRequestSubmitter.Properties.Settings>
</applicationSettings>
```

### Configuration Parameters

- **VSDCTargetAddress**: The VSDC web service endpoint provided by the Tax Authority
- **CertificateName**: The full Subject CN (Common Name) of your POS PFX certificate
- **PACCode**: The Program Access Code provided along with your PFX file

---

## Troubleshooting

### Certificate Issues

- **Certificate not found**: Verify that the PFX certificate is installed in the Current User Personal certificate store
- **Certificate password error**: Confirm the correct password is specified in the configuration
- **Untrusted certificate**: Ensure ROOT and Issuing certificates are installed in the Machine Personal certificate store

### Connection Issues

- **Unable to connect to VSDC**: Verify the VSDC target address is correct and the system has network access
- **Timeout errors**: Check network connectivity and VSDC service availability
- **SOAP fault responses**: Review the error message in the response and consult Tax Authority documentation

### Request Submission Issues

- **Invalid request format**: Ensure the invoice request is properly formatted according to TaxCore specifications
- **Signature validation failure**: Verify the certificate is valid and has appropriate permissions
- **Response link not provided**: Check the application logs and VSDC system response for details

---

## Credits
 
**Team A** is responsible for onboarding new members, maintaining the codebase, and implementing new features for the VSDCRequestSubmitterV3.0 application.
