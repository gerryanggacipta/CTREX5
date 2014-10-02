using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREX.Entities;
using TREX.DAL;

namespace TREX.Utilities
{
    public class TradeManager
    {
        private TradeManager() { }
        private static TradeManager _instance = null;
        private static TradeDAL _tradeDAL;
        private static PortfolioDAL _portfolioDAL;
        private static QueueManager _queueManager;

        public static TradeManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TradeManager();
                    _tradeDAL = TradeDAL.Instance;
                    _portfolioDAL = PortfolioDAL.Instance;

                    // Handle trade to/from exchange
                    _queueManager = QueueManager.Instance;
                    _queueManager.TradeReplied += new Action<Trade>(processTradeFromExchange);
                    _queueManager.TradeSending += new Action<Trade>(processTradeToExchange);
                }

                return _instance;
            }
        }

        private int _currentTradeId = 1;
        public int NextTradeId
        {
            get
            {
                // *** Query _currentTradeId from database
                _currentTradeId += 1;
                return _currentTradeId;
            }
        }

        public Trade getAllTradesByTradeId(int tradeId)
        {
            return _tradeDAL.GetTradeById(tradeId);
        }
        public List<Trade> getAllTrades()
        {
            return _tradeDAL.GetAllTrades();
        }
        public List<Trade> getAllTradesByStrategyId(string strategyId)
        {
            return _tradeDAL.GetTradeByStrategyId(strategyId);
        }
        public List<Trade> getAllTradesByDate(string startDateTime, string endDateTime)
        {
            return _tradeDAL.GetTradesByDate(startDateTime, endDateTime);
        }
        public List<Trade> getAllTradesByType(bool isAuto)
        {
            return _tradeDAL.GetTradesByType(isAuto);
        }
        public List<Trade> getAllTradesBySymbol(string symbol)
        {
            return _tradeDAL.GetTradesBySymbol(symbol);
        }
        public List<Trade> getAllOngoingTrades()
        {
            return _tradeDAL.GetOngoingTrades();
        }

        private static void processTradeToExchange(Trade trade)
        {
            // Ignore Short Sell trade
            if (trade.Short == true) return;

            // Get all info to update portfolio
            string symbol = trade.Stock;
            int size = trade.Size;
            bool isBuy = trade.Buy;

            // Otherwise, add the trade
            _tradeDAL.InsertTrade(trade); 

            // Monitor AvailableSize of PortfolioEntry
            PortfolioEntry entry = _portfolioDAL.GetPortfolioEntryByStock(symbol);
            if (entry == null)
            {
                // Handle new entry
                _portfolioDAL.AddPortfolioEntry(
                    new PortfolioEntry()
                    {
                        Stock = symbol,
                        TotalSize = 0,
                        AvailableSize = (isBuy ? size : -size), // Monitor available size
                        IncrementalBalance = 0
                    });
            }
            else
            {
                // Update available size
                if (isBuy)
                {
                    entry.AvailableSize += size;
                }
                else
                {
                    entry.AvailableSize -= size;
                }
                _portfolioDAL.UpdatePortfolio(entry);
            }
        }

        private static void processTradeFromExchange(Trade trade) 
        {
            // Ignore Short Sell trade
            if (trade.Short == true) return;

            // Get all info to update portfolio
            string symbol = trade.Stock;
            int size = trade.Size;
            decimal pnl = trade.PnL;
            bool isBuy = trade.Buy;

            // Otherwise, process the trade
            _tradeDAL.UpdateTrade(trade); 

            // Monitor TotalSize of PortfolioEntry
            PortfolioEntry entry = _portfolioDAL.GetPortfolioEntryByStock(symbol);
            if (entry == null)
            {
                // Handle new entry, assume Trade has been resolved
                // Note: It will not enter here!!!
                _portfolioDAL.AddPortfolioEntry(
                    new PortfolioEntry()
                    {
                        Stock = symbol,
                        TotalSize = (isBuy ? -size : size),
                        AvailableSize = (isBuy ? -size : size), // Monitor available size
                        IncrementalBalance = pnl
                    });
            }
            else
            {
                // Update total size
                if (isBuy)
                {                    
                    entry.TotalSize += size;
                    entry.IncrementalBalance += pnl;
                }
                else
                {
                    entry.TotalSize -= size;
                    entry.IncrementalBalance += pnl;
                }
                _portfolioDAL.UpdatePortfolio(entry);
            }

        }


    }
}
