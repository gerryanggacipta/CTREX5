using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TREX.Entities
{
    public class Portfolio
    {
        public string Stock { get; set; }
        public decimal IncrementalBalance { get; set; }

        public decimal AvailableSize { get; set; }

        public decimal TotalSize { get; set; }

    }
}
