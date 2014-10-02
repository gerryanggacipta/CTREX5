using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TREX.DAL;
using TREX.Entities;
using TREX.Common;

namespace TREX.Utilities
{
    //singleton class
    public class StrategyManager
    {
        private class DataAggregate
        {
            public Trade trade { get; set; }
            public IStrategy strategy { get; set; }
            public Stock stock { get; set; }
        }
        private Queue<IStrategy> _strategyList;
        private static StrategyManager _instance;
        private Queue<Trade> _orderQueue;
        private List<SystemConstants.StrategyType> _strategyNames;
       // public event Action DataArrival;

        private StrategyManager() {
            _orderQueue = new Queue<Trade>();
            _strategyNames = new List<SystemConstants.StrategyType>();
            _strategyNames.Add(SystemConstants.StrategyType.MOVING_AVERAGE);
            _strategyList = new Queue<IStrategy>();
            //QueueManager.Instance.TradeReplied += new Action<Trade>(hearFromExchange);
        }

        
        
        public static StrategyManager Instance {

           get {
               if (_instance == null)
               {
                   _instance = new StrategyManager();

                   
               }
               return _instance;
           }

        }

        public void InvokeExecute(Object obj)
        {

            DataAggregate d = obj as DataAggregate;
            
            ExecuteStrategy(d.trade,d.strategy,d.stock);
            
            
        }

        public void ExecuteStrategy(Trade trade,IStrategy strategy,Stock s) {
            decimal bidVal,askVal;
            String bidAsk;

            setBidAsk(trade, s, out bidVal, out askVal,out bidAsk);
           
            
            strategy.MakeTrade(trade,strategy,s);
            _orderQueue.Dequeue();
            routeToExchange(trade);

        }

        private static void setBidAsk(Trade trade, Stock s, out decimal bidVal,out decimal askVal, out String bidAsk)
        {
           
            askVal = s.Ask;
            bidVal = s.Bid;
            if (trade.Buy)
            {
                bidAsk = "ASK";
            }
            else
            {
                bidAsk = "BID";
            }
        }
        
        
        public void UpdateParams(Trade trade,Stock s,IStrategy strategy)
        {
            Console.WriteLine("Update Parameters.....");
            String bidAsk;
            decimal bidVal, askVal; ;
            AlgoDescriptor desc = new AlgoDescriptor();
            setBidAsk(trade, s, out bidVal, out askVal,out bidAsk);
            desc.bidValue= bidVal;
            desc.askValue = askVal;
           
            desc.bidAsk = bidAsk;
            desc.duration = "MINUTES";
            desc.shortSell = trade.Short;
            desc.shortLong = false;

            strategy.movingUpdate(desc);
        }

        public Stock GetRealTimeData(string symbol){

            var marketDataManager = MarketDataManager.Instance;
            return marketDataManager.getStock(symbol);
            //return marketDataManager.getStockFromLocalCsv(symbol);
        }

        public void ServiceQueue() {

            var trade = _orderQueue.Last();
            Console.WriteLine("Inside the service queue "+trade.Price);

            StrategyRouter(trade);
            var strategy = _strategyList.Last();
            Stock s = GetRealTimeData(trade.Stock);
            DataAggregate d = new DataAggregate();
            d.stock = s;
            d.trade = trade;
            d.strategy = strategy;

            Thread t = new Thread(new ParameterizedThreadStart(InvokeExecute));
            
            t.Start(d);

        }

        public void PopulateQueue(Trade trade) {
            _orderQueue.Enqueue(trade);
           
             ServiceQueue();
        }

        public void StrategyRouter(Trade trade) {

            var strategyId = trade.StrategyID;

            _strategyList.Enqueue(new MovingAvgStrategy(trade.Stock, trade));

            if (strategyId.Contains(SystemConstants.StrategyType.MOVING_AVERAGE.ToString()))
            {
                //Moving Averages
               
            }
            else if (strategyId.Contains(SystemConstants.StrategyType.BOLLINGER_BAND.ToString()))
            {
                //Bollinger Bands    
            }
            else if (strategyId.Contains(SystemConstants.StrategyType.PRICE_BREAKOUT.ToString()))
            {
                //Price Breakout
            }
        }

        public List<Strategy> GetAllStratConfig()
        {
            var dal = StrategyDAL.Instance;
            return dal.GetAllStrategies();
        }

        public List<Strategy> GetStratByType(string type)
        {
            var dal = StrategyDAL.Instance;
            return dal.GetStrategiesByType(type);
        }

        public Strategy GetStratConfig(int strategyId) {

            var dal = StrategyDAL.Instance;
            return dal.GetStrategyById(strategyId);
        }

        public SystemConstants.Status SaveStratConfig(Strategy strategy) {

            var dal = StrategyDAL.Instance;

           
            var success = -1;
            if (strategy.Id == -1)
                success = dal.InsertStrategy(strategy);
            else
            {
                dal.UpdateStrategy(strategy);
            }
            if(success == 1)
                return SystemConstants.Status.SUCCESS;

            return SystemConstants.Status.ERROR; 

        }

     

        void routeToExchange(Trade trade)
        {
            QueueManager.Instance.sendMessage(trade);
        }

        void hearFromExchange(Trade t)
        {

            //return trade but to where??
           // Logger.Out(String.Format("Stock: {0}, Quantity: {1}, Price: {2}, PnL: {3}",t.Stock, t.Size, t.Price, t.PnL));

        }
 









    }
}
