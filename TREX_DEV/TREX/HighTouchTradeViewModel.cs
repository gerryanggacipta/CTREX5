using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREX.Entities;
using TREX.Utilities;

namespace TREX
{
    internal class HighTouchTradeViewModel
    {
        public HighTouchTradeViewModel(bool isBuy, Stock selectedStock) 
        {
            Buy = isBuy;
            Sell = !isBuy;
            SelectedStock = selectedStock;
        }

        public Stock SelectedStock { get; set; }
        public bool Buy { get; set; }
        public bool Sell { get; set; }

        internal void addTrade(string stock, string price, string size, bool isBuy, bool _isLong, String when)
        {
            Trade trade = new Trade() { Id = TradeManager.Instance.NextTradeId, StrategyID = "-" , Stock = stock, Price = Convert.ToDecimal(price), Size = Convert.ToInt32(size), Buy = isBuy, Short = !_isLong, WhenAsDate = when, Auto = false};
            QueueManager.Instance.sendMessage(trade);
            TradeViewModel.Instance.addHighTouchTrade(trade);
        }

    }
}
