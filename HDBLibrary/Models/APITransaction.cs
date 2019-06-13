using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBLibrary.Models
{
    public class APITransaction
    {
        public HDBTransaction HDB { get; set; }
        public HCTransaction HC { get; set; }
    }
}
