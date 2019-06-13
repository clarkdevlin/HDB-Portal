using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBLibrary.Models
{
    public class HCTransaction: Transaction
    {
        public string AddressBlk { get; set; }
        public string AddressLevel { get; set; }
        public string AddressUnit { get; set; }
        public string AddressStreetName { get; set; }
        public string AddressPostalCode { get; set; }
        public string PlanName { get; set; }
        public decimal? Section1SumInsured { get; set; }
        public decimal? Section2SumInsured { get; set; }
        public decimal? Section3SumInsured { get; set; }
        public decimal? TotalSumInsured { get; set; }
        public string PromoCode { get; set; }
       
    }
}
