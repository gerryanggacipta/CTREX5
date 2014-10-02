using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using TREX.Entities;
using TREX.Utilities;

namespace TREX
{
    internal class TradeViewModel
    {        
        private static TradeViewModel _instance = null;

        public static TradeViewModel Instance
        {
            get
            {
                if (_instance == null) _instance = new TradeViewModel();

                return _instance;
            }
        }

        // An observable collection of stocks (the stocks come to us from the model).
        // The view binds its listview to this collection.
        public ObservableCollection<Trade> MyTrades { get; set; }

        private Dictionary<int, Trade> tradeMonitorDict; 
        public QueueManager queueManager;
        private readonly Dispatcher currentDispatcher;
        private TradeManager _tradeManager;

        private TradeViewModel()
        {
            Console.WriteLine(">> Enter constructor of MainWindowWiewModel");

            // Create an empty collection of quotes.
            this.MyTrades = new ObservableCollection<Trade>();
            tradeMonitorDict = new Dictionary<int, Trade>();

            // Query all trades from DB
            _tradeManager = TradeManager.Instance;
            List<Trade> trades = _tradeManager.getAllTrades();
            foreach (Trade t in trades)
            {
                MyTrades.Add(t);
            }
            
            // Init 
            queueManager = QueueManager.Instance;

            // Handle event when response arrived            
            QueueManager.Instance.TradeSending += new Action<Trade>(notifyUI);   
            QueueManager.Instance.TradeReplied += new Action<Trade>(notifyUI);            

            // Init Dispatcher for cross-thread update
            currentDispatcher = Dispatcher.CurrentDispatcher;

            // Start async consumer
            queueManager.startAsyncReceiveMessage();
        }

        private void notifyUI(Trade trade)
        {
            Action dispatchData = () =>
            {
                Console.WriteLine(">> [ViewModel] XML Data arrived to UI thread");
                
                //Set the trade position to Closed
                if (tradeMonitorDict.ContainsKey(trade.Id))
                {
                    int index = MyTrades.IndexOf(tradeMonitorDict[trade.Id]);                    
                    tradeMonitorDict[trade.Id].Position = "Close";
                    MyTrades.RemoveAt(index);
                    MyTrades.Insert(index,tradeMonitorDict[trade.Id]);
                }
                else
                {
                    trade.Position = "Open";
                    this.MyTrades.Add(trade);
                    this.tradeMonitorDict.Add(trade.Id, trade);
                }
            };

            currentDispatcher.BeginInvoke(DispatcherPriority.DataBind, dispatchData);
        }

        public void addHighTouchTrade(Trade trade) {
            notifyUI(trade);
        }
    }
}
