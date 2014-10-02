using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREX.Entities;
using TREX.Utilities;
using TREX.Common;
using System.ComponentModel;

namespace TREX
{
    internal class StrategyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Parameters for querying yahoo chart
        public string stockString;
        public YahooChart.ChartTimeSpan timeSpan;
        public YahooChart.ChartType chartType;
        public List<YahooChart.MovingAverageIndicator> movingAvgIntervalList;

        #region Constructor and Properties

        public StrategyViewModel()
        {
            StrategyList = getAllStratConfig();
            MarketDataManager.Instance.LiveDataArrived += new Action<List<Stock>>(getStockChart);
        }

        private List<Strategy> _strategyList;
        public List<Strategy> StrategyList
        {
            get { return _strategyList; }
            set
            {
                _strategyList = value;
                OnPropertyChanged("StrategyList");
            }
        }

        private Strategy _selectedStrategy;
        public Strategy SelectedStrategy
        {
            get { return _selectedStrategy; }
            set
            {
                _selectedStrategy = value;
                OnPropertyChanged("SelectedStrategy");
            }
        }

        private Stock _selectedStock;
        public Stock SelectedStock
        {
            get { return _selectedStock; }
            set
            {
                _selectedStock = value;
                OnPropertyChanged("SelectedStock");
            }
        }

        private byte[] _stockImage;
        public byte[] StockImage
        {
            get { return _stockImage; }
            set
            {
                _stockImage = value;
                OnPropertyChanged("StockImage");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        // Query StrategyManager for all strategy configurations
        internal List<Strategy> getAllStratConfig()
        {
            StrategyList = StrategyManager.Instance.GetAllStratConfig();
            return StrategyList;
        }

        // Query StrategyManager for strategy configurations by strategy type
        internal List<Strategy> getStratConfigByType(String type)
        {
            StrategyList = StrategyManager.Instance.GetStratByType(type);
            return StrategyList;
        }

        // Query MarketDataManager for stock
        internal Stock getStock(String symbol)
        {
            SelectedStock = MarketDataManager.Instance.getStock(symbol);
            return SelectedStock;
        }

        // Query PortfolioManager for stock
        internal int getPortfolioAvailableSize(String symbol)
        {
            if (PortfolioManager.Instance.GetAvailableSizeByStock(symbol) == -1)
                return 0;

            return PortfolioManager.Instance.GetAvailableSizeByStock(symbol);
        }

        // Save strategy configuration on "Save" click
        internal void saveStratConfig(int strategyId, string strategyType, bool activated, string stock, bool isBuy, bool isShort, string name, string size)
        {
            // Create strategyconfig object
            Strategy strategyConfig = new Strategy() { Id = strategyId, Type = strategyType.ToString(), Activated = activated, Stock = stock, Buy = isBuy, Short = isShort, Name = name, Size = Convert.ToInt32(size) };

            // Save the strategy config object into DB
            if (StrategyManager.Instance.SaveStratConfig(strategyConfig) == SystemConstants.Status.ERROR)
                Logger.Out("Error in saving strategy configuration");
        }

        // Add trade to queue on "Start" click
        internal void addTrade(string strategyID, string stock, decimal price, long size, bool isBuy, bool _isLong, string when)
        {
            Trade trade = new Trade() { Id = TradeManager.Instance.NextTradeId, StrategyID = strategyID, Stock = stock, Price = Convert.ToDecimal(price), Size = Convert.ToInt32(size), Buy = isBuy, Short = !_isLong, WhenAsDate = when, Auto = true };
            StrategyManager.Instance.PopulateQueue(trade);
        }

        // Get stock chart from Yahoo
        internal void getStockChart(List<Stock> list)
        {
            if (SelectedStock != null)
            {
                StockImage = YahooChart.getStockChart(SelectedStock.Symbol, timeSpan, chartType, movingAvgIntervalList, YahooChart.ChartImageSize.Large_l, true);
            }            
        }
        
    }
}
