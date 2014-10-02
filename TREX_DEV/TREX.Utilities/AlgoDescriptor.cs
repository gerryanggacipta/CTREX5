
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TREX.Utilities
{
    public class AlgoDescriptor
    {
        public decimal askValue { get; set; }
        public String duration {get;set;}
        public bool shortLong { get; set; }
        public String bidAsk { get; set; }

        public bool shortSell { get; set; }

        public decimal bidValue { get; set; }

        
    }
}
