using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TREX.Utilities
{
    public class SystemConstants
    {
       
        public enum Status { SUCCESS = 0, ERROR };
       
        public const int queryFrequency=(MarketDataManager.SLEEP_INTERVAL)/1000; //in seconds
       
        public enum Signals { EXIT,DEFAULT,SHORTSELL, SELL, BUY };
       
        public enum StrategyType { BOLLINGER_BAND, MOVING_AVERAGE, PRICE_BREAKOUT };
       
        public const int HISTORY_LENGTH = 365;

        public enum TradePosition { OPEN, CLOSE };


    }
}
