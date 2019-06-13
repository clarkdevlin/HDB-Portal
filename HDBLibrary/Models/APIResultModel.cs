using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBLibrary.Models
{
    public class APIResultModel
    {
        public string HDBReferenceNo { get; set; }
        public string AddressBlk { get; set; }
        public string AddressLevel { get; set; }
        public string AddressUnit { get; set; }
        public string AddressStreetName { get; set; }
        public string AddressPostalCode { get; set; }
        public string FlatTypeClassification { get; set; }
        public string HDBFlatType { get; set; }
        public DateTime? RenewalEffectiveDate { get; set; }

        public HDBFlatPremium HDBFlatPremium
        {
            get {
                var output = new HDBFlatPremium();
                output.LoadFlatPremium(this.HDBFlatType);
                return output;
            }
        }

    }
}
