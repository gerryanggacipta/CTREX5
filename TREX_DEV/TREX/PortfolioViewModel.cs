using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREX.Entities;
using TREX.Utilities;

namespace TREX
{
    internal class PortfolioViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Constructor and Properties
        public PortfolioViewModel() {

            PortfolioEntries = PortfolioManager.Instance.GetAllPortfolioEntries();
            TradeListByStock = TradeManager.Instance.getAllTrades();

            QueueManager.Instance.TradeSending += new Action<Trade>(updatePortfolioView);
            QueueManager.Instance.TradeReplied += new Action<Trade>(updatePortfolioView);
        }

        private List<PortfolioEntry> _portfolioEntries;
        public List<PortfolioEntry> PortfolioEntries
        {
            get { return _portfolioEntries; }
            set
            {
                _portfolioEntries = value;
                OnPropertyChanged("PortfolioEntries");
            }
        }

        private List<Trade> _tradeListByStock;
        public List<Trade> TradeListByStock
        {
            get { return _tradeListByStock; }
            set
            {
                _tradeListByStock = value;
                OnPropertyChanged("TradeListByStock");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        internal void getAllTradesBySymbol(string selectedStock)
        {
            TradeListByStock = TradeManager.Instance.getAllTradesBySymbol(selectedStock);
        }
        
        internal void updatePortfolioView(Trade trade)
        {
            PortfolioEntries = PortfolioManager.Instance.GetAllPortfolioEntries();
            TradeListByStock = TradeManager.Instance.getAllTrades();
        }
    }
}
