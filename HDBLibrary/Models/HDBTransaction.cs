using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBLibrary.Models
{
    public class HDBTransaction: Transaction
    {
        public string TransactionType { get; set; }
        public decimal? SumInsured { get; set; }
        
    }
}
