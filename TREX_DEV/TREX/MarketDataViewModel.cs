using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREX.Entities;
using TREX.Utilities;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows;
using System.ComponentModel;

namespace TREX
{
    public class MarketDataViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private String[] _summaryList = { "Bid", "Ask", "Vol", "PrevClose", "Open", "MarketCap" };
        
        private readonly Dispatcher currentDispatcher;
        private MarketDataManager marketDataManager;
        private ListView _marketDataListView;

        // Parameters for querying yahoo chart
        public string stockString;
        public YahooChart.ChartTimeSpan timeSpan;
        public YahooChart.ChartType chartType;
        public List<YahooChart.MovingAverageIndicator> movingAvgIntervalList;


        public MarketDataViewModel() { }
        public MarketDataViewModel(ListView marketDataListView)
        {
            _marketDataListView = marketDataListView;
            // Create an empty collection of quotes.
            AllStocks = new ObservableCollection<Stock>();
            StockSummary = new ObservableCollection<Stock>();
            WatchList = new ObservableCollection<Stock>();

            // Init MarketDataManager
            // and set symbols of subscribed stocks
            marketDataManager = MarketDataManager.Instance;
            marketDataManager.setSymbolList(CompanyListManager.SymbolList);

            // Handle event when live data arrived
            marketDataManager.LiveDataArrived += new Action<List<Stock>>(updateWithLiveStockData);

            // Init Dispatcher for cross-thread update
            currentDispatcher = Dispatcher.CurrentDispatcher;

            // Start 
            getLiveStockData();
        }

        // An observable collection of stocks (the stocks come to us from the model).
        // The view binds its listview to this collection.
        public static ObservableCollection<Stock> AllStocks { get; set; }
        public List<string> subscribedStockSymbols { get; set; }
        public ObservableCollection<Stock> StockSummary { get; set; }
        public ObservableCollection<Stock> WatchList { get; set; }
        // Lock for safety & atomic access
        public object SyncWatchListLock = new object();
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
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }


        public void getLiveStockData()
        {
            Thread liveFeeder = new Thread(new ThreadStart(marketDataManager.Run));
            liveFeeder.IsBackground = true;
            liveFeeder.Priority = ThreadPriority.Normal;
            liveFeeder.Start();
        }

        private void updateWithLiveStockData(List<Stock> liveData)
        {
            Action dispatchData = () =>
            {
                int selectedIndex = _marketDataListView.SelectedIndex == -1 ? 0 : _marketDataListView.SelectedIndex;

                AllStocks.Clear();

                int count = 0;
                foreach (Stock stock in liveData)
                {
                    if (MarketDataView.filterOption != null && MarketDataView.filterString != null)
                    {
                        if (MarketDataView.filterOption.Equals("Symbol") && stock.Symbol.ToUpper().StartsWith(MarketDataView.filterString.ToUpper()))
                        {
                            AllStocks.Add(stock);
                            
                        }
                            
                        if (MarketDataView.filterOption.Equals("Name") && stock.Name.ToUpper().StartsWith(MarketDataView.filterString.ToUpper()))
                            AllStocks.Add(stock);
                        if (MarketDataView.filterOption.Equals("Exchange") && stock.Exchange.ToUpper().StartsWith(MarketDataView.filterString.ToUpper()))
                            AllStocks.Add(stock);
                    } 
                    else
                    {
                        AllStocks.Add(stock);

                        // Control safety access to WatchList
                        lock (SyncWatchListLock)
                        {
                            int watchListCount = WatchList.Count;
                           
                            if (count < watchListCount)
                            {                                
                                for (int i = 0; i < watchListCount; i++)
                                {
                                    if (stock.Symbol.Equals(WatchList[i].Symbol))
                                    {                                        
                                        WatchList.RemoveAt(i);
                                        WatchList.Insert(i, stock);
                                        count++;
                                        if (count == WatchList.Count) continue;
                                    }
                                }
                            }
                        }   // Release lock
                    }
                }
                _marketDataListView.SelectedIndex = selectedIndex;
                getStockChart(stockString, timeSpan, chartType, movingAvgIntervalList);
            };

            currentDispatcher.BeginInvoke(dispatchData);
        }

        internal void getStockSummary()
        {
            if (_marketDataListView == null || _marketDataListView.Items.Count <= 0) return;

            StockSummary.Clear();

            if (SelectedStock == null)
            {
                SelectedStock = (Stock)_marketDataListView.Items[0];
            }

            for (int i = 0; i < _summaryList.Length; i++)
            {
                string summaryValue = "";
                switch (_summaryList[i])
                {
                    case "Bid":
                        summaryValue = SelectedStock.Bid == 0 ? "-" : SelectedStock.Bid.ToString();
                        break;
                    case "Ask":
                        summaryValue = SelectedStock.Ask == 0 ? "-" : SelectedStock.Ask.ToString();
                        break;
                    case "Vol":
                        summaryValue = SelectedStock.Vol == 0 ? "-" : SelectedStock.Vol.ToString();
                        break;
                    case "PrevClose":
                        summaryValue = SelectedStock.PrevClose == 0 ? "-" : SelectedStock.PrevClose.ToString();
                        break;
                    case "Open":
                        summaryValue = SelectedStock.Open == 0 ? "-" : SelectedStock.Open.ToString();
                        break;
                    case "MarketCap": 
                        decimal num;
                        if (decimal.TryParse(SelectedStock.MarketCap, out num))
                        {
                            summaryValue = Convert.ToDecimal(SelectedStock.MarketCap) == 0 ? "-" : SelectedStock.MarketCap.ToString();
                        }
                        else
                            summaryValue = SelectedStock.MarketCap.ToString();
                        break;
                }
                StockSummary.Add(new Stock() { SummaryName = _summaryList[i], SummaryValue = summaryValue });
            }
        }

        // Get stock chart from Yahoo
        private void getStockChart(string stock, YahooChart.ChartTimeSpan timeSpan, YahooChart.ChartType chartType, List<YahooChart.MovingAverageIndicator> movingAvgIntervalList)
        {
            StockImage = YahooChart.getStockChart(stock, timeSpan, chartType, movingAvgIntervalList, YahooChart.ChartImageSize.Large_l, false);
        }
    }
}
