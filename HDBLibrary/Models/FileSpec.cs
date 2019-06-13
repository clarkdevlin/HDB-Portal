using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBLibrary.Models
{
    public class FileSpec
    {
        public string DataItem { get; set; }
        public int NoOfBytes { get; set; }
        public bool Enabled { get; set; }
        public int SetPos { get; set; }
    }
}
