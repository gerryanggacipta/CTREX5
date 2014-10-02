using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TREX.Entities
{
    public class HistStock
    {
        public string Symbol { get; set; }
        public decimal Open { get; set; }        
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal AdjClose { get; set; }
        public decimal Vol { get; set; }
        public string When { get; set; }

        public override string ToString()
        {
            return Symbol + " , " + Open + " , " + High + " , " + Low + " , " +
                Close + " , " + Vol + " , " + AdjClose + " , " + When;
        }
    }
}
