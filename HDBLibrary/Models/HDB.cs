using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBLibrary.Models
{
    public class HDB
    {
        public string AddressBlk { get; set; }
        public string AddressLevel { get; set; }
        public string AddressPostalCode { get; set; }
        public string AddressStreetName { get; set; }
        public string AddressUnit { get; set; }
        public string BranchCode { get; set; }
        public string CompulsoryStatus { get; set; }
        public DateTime? InsuranceEffectiveDate { get; set; }
        public int InsuranceTerm { get; set; }
        public string HDBReferenceNo { get; set; }
        public string HeaderIdentifier { get; set; }
        public DateTime? ProcessDate { get; set; }
        public string NameOfInsurer { get; set; }
        public DateTime? NewInsuranceEffectiveDate { get; set; }
        public int NewInsuranceTerm { get; set; }
        public decimal? PremiumAmount { get; set; }
        public decimal? PremiumAmountGST { get; set; }
        public decimal? AddonPremiumAmount { get; set; }
        public decimal? AddonPremiumAmountGST { get; set; }
        public DateTime? SendDateToInsurer { get; set; }
        public string TransactionType { get; set; }
        public string FlatTypeClassification { get; set; }
        public DateTime? RenewalDate { get; set; }
        public DateTime? CurrentFireInsuranceExpiryDate { get; set; }
        public DateTime? ReturnDateToHDB { get; set; }
        public string TrailerIdentifier { get; set; }
        public int TrailerNoOfCases { get; set; }
        public DateTime? TrailerProcessDate { get; set; }
        public string TrailerNameOfInsurer { get; set; }
    }
}
