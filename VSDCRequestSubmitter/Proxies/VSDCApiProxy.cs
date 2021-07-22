using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VSDCRequestSubmitter.Models;

namespace VSDCRequestSubmitter.Proxies
{
    public class VSDCApiProxy
    {
        private string VSDCAddress { get; set; }

        private string Cn { get; set; }
        private string PAC { get; set; }
        public VSDCApiProxy(string vSDCAddress, string cn, string pac)
        {
            this.VSDCAddress = vSDCAddress;
            this.Cn = cn;
            this.PAC = pac;
        }

        public string ExecuteRequest(InvoiceRequest request)
        {
            using (var handler = new WebRequestHandler())
            {
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.ClientCertificates.Add(LoadMyCertificate(Cn));
                
                
                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(VSDCAddress);

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("PAC", PAC);
                    return client.PostAsJsonAsync($"{VSDCAddress}/api/v3/invoices", request).Result.Content.ReadAsStringAsync().Result;
                }
            }
        }

        private X509Certificate2 LoadMyCertificate(string cn)
        {
            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);
            var result = store.Certificates.Find(X509FindType.FindBySubjectName, cn, true);

            return (result.Count > 0) ? result[0] : null;
        }
    }
}
