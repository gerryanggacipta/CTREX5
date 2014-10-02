using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TREX.Entities
{
    public class PortfolioEntry
    {
        public string Stock { get; set; }
        public decimal IncrementalBalance { get; set; }

        public int AvailableSize { get; set; }

        public int TotalSize { get; set; }

    }
}
