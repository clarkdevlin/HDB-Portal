using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBLibrary.Models
{
    public class Transaction
    {
        public string TransactionNo { get; set; }
        public string ReceiptNo { get; set; }
        public string HDBReferenceNo { get; set; }
        public string PolicyNo { get; set; }
        public string CertificateNo { get; set; }
        public DateTime? InsuranceEffectiveDate { get; set; }
        public int InsuranceTerm { get; set; }
        public decimal? PremiumAmount { get; set; }
        public decimal? PremiumAmountGST { get; set; }
        public decimal? PremiumDiscountAmount { get; set; }
        public decimal? TotalPremiumAmount { get; set; }
        public string IssueId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentChequeNo { get; set; }
        public string PaymentCreditCardNo { get; set; }
        public string PaymentReferenceNo { get; set; }
        public decimal? PaymentAmount { get; set; }
        public Guid EntryUserId { get; set; }
        public DateTime? EntryDate { get; set; }

        public PolicyHolder PolicyHolder { get; set; } = new PolicyHolder();
    }
}
