using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREX.Entities;

namespace TREX.Utilities
{
    interface IProviderManager
    {
        #region Historical data
        
        List<HistStock> getHistoricalStocks(string symbol, string startDateTime, string endDateTime, HistoricalPeriod dayOrWeek = HistoricalPeriod.Day);
        List<HistStock> getHistoricalStocks(List<string> symbol, string startDateTime, string endDateTime, HistoricalPeriod dayOrWeek = HistoricalPeriod.Day);

        List<HistStock> getHistoricalStocks(string symbol, string dateTime);                
        List<HistStock> getHistoricalStocks(List<string> symbol, string dateTime);
        
        #endregion


        #region Real-time data

        Stock getRealtimeSingleStock(string symbol);
        List<Stock> getRealtimeMultipleStocks(List<string> symbolList);

        #endregion
    }

    public enum HistoricalPeriod
    {
        Day,
        Week
    }

}
