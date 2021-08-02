using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDCRequestSubmitter.Models
{
    public class InvoiceRequest
    {
        #region Header

        public DateTime? DateAndTimeOfIssue { get; set; }

        public string Cashier { get; set; }

        public string BuyerId { get; set; }

        public string BuyerCostCenterId { get; set; }

        public InvoiceType? InvoiceType { get; set; }

        public TransactionType? TransactionType { get; set; }

        public List<Payment> Payment { get; set; } = new List<Payment>();

        public string InvoiceNumber { get; set; }

        public string ReferentDocumentNumber { get; set; }

        public DateTime? ReferentDocumentDT { get; set; }

        #endregion

        #region Items

        public List<Item> Items { get; set; } = new List<Item>();

        #endregion
    }

    public enum InvoiceType
    {
        Normal,

        ProForma,

        Copy,

        Training,

        Advance
    }

    public enum TransactionType
    {
        Sale,

        Refund
    }

    public enum PaymentType
    {
        Other,

        Cash,

        Card,

        Check,

        WireTransfer,

        Voucher,

        MobileMoney
    }
    public class Payment
    {
        private decimal _amount;
        
        public decimal Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = Decimal.Round(value, 4);
            }
        }

        public PaymentType PaymentType { get; set; }
    }

    public class Item
    {
        public string GTIN { get; set; }

        public string Name { get; set; }

        public decimal? Quantity { get; set; }

        public decimal? Discount { get; set; }

        public List<char> Labels { get; set; }

        public decimal? UnitPrice { get; set; }
      
        public decimal? TotalAmount { get; set; }
     
    }
}