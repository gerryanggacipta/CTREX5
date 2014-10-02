using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TREX.Entities
{
    public class Stock : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public decimal BidSize { get; set; }
        public decimal AskSize { get; set; }
        public Int64 Vol { get; set; }
        public string Currency { get; set; }
        public string Exchange { get; set; }
        public string MarketCap { get; set; }

        private string _when;
        public string When
        {
            get { return _when; }
            set
            {
                _when = value;
                OnPropertyChanged("When");
            }
        }
        public decimal Open { get; set; }
        public decimal PrevClose { get; set; }
        public decimal Change { get; set; }
        public decimal ChangePercentage { get; set; }
        public string ChangeString
        {
            get { return Change + " (" + Math.Round(ChangePercentage,4) + "%)"; }
        }
        public string NameSymbol { 
            get { return Name + " (" + Symbol + ")"; }  
        }
        public string ChangeImage
        {
            get {
                if (Change > 0) return "Images/greenArrow.gif";
                else return "Images/redArrow.png";
            } 
        }
        public string ChangeForeground
        {
            get
            {
                if (Change > 0) return "GreenYellow";
                else return "Red";
            }
        }
        public string SummaryName { get; set; }
        public string SummaryValue { get; set; }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
