//Models a single candle
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TREX.Utilities
{
    public class Candle
    {

        public Candle(decimal value)
        {
            Minimum = value;
            Maximum = value;
        }

        public void ValueUpdated(decimal value)
        {
            if (value < Minimum)
            {
                Minimum = value;
            }
            if (value > Maximum)
            {
                Maximum = value;
            }
        }

        public decimal Maximum { get; private set; }
        public decimal Minimum { get; private set; }

        public decimal Median
        {
            get { return (Maximum + Minimum) / 2; }
        }
    }

}
