using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VSDCRequestSubmitter.Models;
using VSDCRequestSubmitter.Proxies;

namespace VSDCRequestSubmitter
{
    public partial class VSDCRequestSubmitter : Form
    {
        InvoiceRequest invoiceRequest;
        public VSDCRequestSubmitter()
        {
            InitializeComponent();

            txtVSDCAddress.Text = Properties.Settings.Default.VSDCTargetAddress;

            txtCertificateName.Text = Properties.Settings.Default.CertificateName;

            txtPAC.Text = Properties.Settings.Default.PAC;

            invoiceRequest = JsonConvert.DeserializeObject<InvoiceRequest>("{ \"InvoiceType\": \"Normal\", \"TransactionType\": \"Sale\", \"Payment\": [{\"Amount\": 1000, \"PaymentType\": \"Cash\"}], \"Items\": [{\"Name\": \"Network Cable\", \"Quantity\": 1,\"UnitPrice\":100.87, \"Labels\":[\"A\"], \"TotalAmount\": 100.87 }]}");

            txtRequest.Text = JsonConvert.SerializeObject(invoiceRequest, Formatting.Indented);
        }

        private void btnSubmittRequest_Click(object sender, EventArgs e)
        {
            VSDCApiProxy proxy = new VSDCApiProxy(txtVSDCAddress.Text, txtCertificateName.Text, txtPAC.Text);

            Directory.CreateDirectory($"{Path.GetDirectoryName(Application.ExecutablePath)}\\Result");

            File.WriteAllText($"{Path.GetDirectoryName(Application.ExecutablePath)}\\Result\\Response.Json", proxy.ExecuteRequest(JsonConvert.DeserializeObject<InvoiceRequest>(txtRequest.Text)));

            MessageBox.Show($"Signing was successful, find signing result at: {$"{Path.GetDirectoryName(Application.ExecutablePath)}\\Result"}");
        }
    }
}
