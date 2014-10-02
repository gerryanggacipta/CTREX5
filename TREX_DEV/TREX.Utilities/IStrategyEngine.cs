using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREX.Utilities;
using TREX.Entities;

namespace TREX.Utilities
{
    public interface IStrategy
    {
        //void getPriceListByStock(String symbol,String startDateTime,String endDateTime);

        void movingUpdate(Object o);

        decimal MakeTrade(Trade t,IStrategy strategy,Stock s);
         //SystemConstants.Status saveStratConfig();

         //Strategy getStratConfig(int strategyID);

    }
}
