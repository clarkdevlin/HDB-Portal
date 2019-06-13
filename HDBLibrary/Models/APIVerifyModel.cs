using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBLibrary.Models
{
    public class APIVerifyModel
    {
        [MaxLength(11)]
        [Required]
        public string HDBReferenceNo { get; set; }
        [MaxLength(2)]
        [Required]
        public string AddressLevel { get; set; }
        [MaxLength(7)]
        [Required]
        public string AddressUnit { get; set; }
        [MaxLength(6)]
        [Required]
        public string AddressPostalCode { get; set; }
    }
}
