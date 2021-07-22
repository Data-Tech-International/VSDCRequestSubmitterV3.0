# VSDCRequestSubmitter
Basic VSDC invoice sign submitting app, created with intention to help POS developers to integrate with TaxCore system

Use this mini app to send an invoice request in order to get signature from VSDC system.

Contact Tax Authority to obtain POS PFX file along with corresponding ROOT and Issuing certificates.

### Follow these steps:

* Install ROOT and Issuing certificates to machine certificate store based on guide provided by the Tax Authority.
* Install POS PFX file to the current user personal store.
* Set VSDC target address.
* Set POS PFX file Subject CN.
* Edit Request to suit your personal needs.
* Do not forget to edit PAC code received along with POS PFX file and password.
* Submit request and find results on the link provided from MessageBox.

_To avoid constant setting of VSDC server address and PFX CN from app interface, you can set them by editing app.config file._

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
