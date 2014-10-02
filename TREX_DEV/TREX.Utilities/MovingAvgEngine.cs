

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREX.Entities;
using TREX.Utilities;
using TREX.Common;

namespace TREX.Utilities
{
    interface Indicator
    {
        decimal Value { get; }
    }

    public class MovingAvgStrategy: IStrategy
    {

        public SystemConstants.Signals pSell{get;set;}
        public SystemConstants.Signals pBuy{get;set;}
        public SystemConstants.Signals pSsell{get;set;}

        decimal support, resistance;
        public SystemConstants.Signals exit { get; set; }

     
        public SystemConstants.Signals terSell { get; set; }
        public SystemConstants.Signals terBuy { get; set; }
        public SystemConstants.Signals terSsell { get; set; }

        public SystemConstants.Signals quartSell { get; set; }
        public SystemConstants.Signals quartBuy { get; set; }
        public SystemConstants.Signals quartSsell { get; set; }

        private MovingAverage mvBid,mvAsk, MA10, MA15,MAHourlyBid,MAHourlyAsk,MAMinutesBid,MAMinutesAsk;
        
        private List<decimal> avgPoints { get; set; }
        private List<decimal> avgPointsAsk { get; set; } 
        private List<decimal> avgPointTen { get; set; }
        private List<decimal> avgPointFifteen { get; set; }
        private List<decimal> avgPointHours { get; set; }
        private List<decimal> avgPointMinutes { get; set; }
        private List<decimal> avgPointHoursAsk { get; set; }
        private List<decimal> avgPointMinutesAsk { get; set; }

        Trade trade;
        public void GenerateTertiarySignal(bool shortSell=false){
           if (avgPointTen.Last() == avgPointFifteen.Last() || ((avgPointTen.ElementAt(avgPointTen.Count - 2) - avgPointFifteen.ElementAt(avgPointFifteen.Count - 2)) * (avgPointTen.Last()-avgPointFifteen.Last()) <0))
                {
                    if (avgPointTen.ElementAt(avgPointTen.Count - 2) > avgPointFifteen.ElementAt(avgPointFifteen.Count - 2))
                    {
                        if (shortSell)
                        {
                            terSsell = SystemConstants.Signals.SHORTSELL;
                        }
                        else
                        {
                            terSell = SystemConstants.Signals.SELL;
                        }
                        
                        //generate secondary short sell signal
                    }
                    else if (avgPointTen.ElementAt(avgPointTen.Count - 2) < avgPointFifteen.ElementAt(avgPointFifteen.Count - 2))
                    {
                        terBuy = SystemConstants.Signals.BUY;
                        //generate secondary long buy signal
                    }
                }
            Console.WriteLine("Inside Tertiary Signal " + terBuy + " " + terSell + " " + terSsell);
            
        }
    
        void getQuaternary(decimal bidVal,decimal askVal,bool shortSell=false)
        {
             if (askVal - support <= 0.1M * (askVal)){
                    quartBuy = SystemConstants.Signals.BUY;

                }
            else if((resistance - askVal <= 0.1M*(askVal))&&shortSell){

                quartSsell = SystemConstants.Signals.SHORTSELL;
            }
            else if (resistance - bidVal <= 0.1M * (bidVal)){

                quartSell = SystemConstants.Signals.SELL;
                    
            }
    
        }
        public MovingAvgStrategy(String inputStock,Trade t){


            MovingAvgInitializor(inputStock,t);

            ListInitializor();

            SignalInitializor();

            DateTime d= DateTime.Now.Date;
            DateTime earlier = DateTime.Now.Date;
            earlier = earlier.AddDays((-1)*SystemConstants.HISTORY_LENGTH);


            List<HistStock> stockList = MarketDataManager.Instance.getHistoricalStocks(inputStock, earlier.ToString("yyyyMMdd"), d.ToString("yyyyMMdd"));

            for (int i = 0; i <30; i++)
            {
                longTermSetup(stockList.ElementAt(i).Close);
            }

 }

        private void ListInitializor()
        {
            avgPoints = new List<decimal>();
            avgPointTen = new List<decimal>();
            avgPointFifteen = new List<decimal>();
            
            avgPointHours = new List<decimal>();
            avgPointMinutes = new List<decimal>();
            avgPointHoursAsk = new List<decimal>();
            avgPointMinutesAsk = new List<decimal>();
            avgPointsAsk = new List<decimal>();
     
        }

        private void MovingAvgInitializor(String inputStock,Trade t)
        {
            SetupTrade(t);
            mvBid = new MovingAverage(40, inputStock);
            MA10 = new MovingAverage(10, inputStock, true);
            MA15 = new MovingAverage(15, inputStock, true);
            MAHourlyBid = new MovingAverage(3600, inputStock);
            MAMinutesBid = new MovingAverage(60, inputStock);
            MAHourlyAsk = new MovingAverage(3600, inputStock);
            MAMinutesAsk = new MovingAverage(60, inputStock);
            mvAsk = new MovingAverage(40, inputStock);


            Stock s = MarketDataManager.Instance.getStock(inputStock);



            for (int i = 0; i < MAHourlyAsk._length; i++)
            {
                MAHourlyAsk.Add(s.Ask);
                MAHourlyBid.Add(s.Bid);
            }
            for (int i = 0; i < MAMinutesAsk._length; i++)
            {
                MAMinutesAsk.Add(s.Ask);
                MAMinutesBid.Add(s.Bid);
            }
            for (int i = 0; i < mvAsk._length; i++)
            {
                mvBid.Add(s.Bid);
                mvAsk.Add(s.Ask);
            }

        }

        private void SetupTrade(Trade t)
        {
            trade = new Trade();
            trade.Buy = t.Buy;
            trade.Auto = t.Auto;
            trade.Id = t.Id;
            trade.PnL = t.PnL;
            trade.Position = t.Position;
            trade.Price = t.Price;
            trade.Short = t.Short;
            trade.Size = t.Size;
            trade.Stock = t.Stock;
            trade.StrategyID = t.StrategyID;
            trade.WhenAsDate = t.WhenAsDate;
        }

        private void SignalInitializor()
        {
            pSell = SystemConstants.Signals.DEFAULT;
            pBuy = SystemConstants.Signals.DEFAULT;
            terSell = SystemConstants.Signals.DEFAULT;
            terSsell = SystemConstants.Signals.DEFAULT;
            terBuy = SystemConstants.Signals.DEFAULT;
            pSsell = SystemConstants.Signals.DEFAULT;
            exit = SystemConstants.Signals.DEFAULT;
            quartBuy = SystemConstants.Signals.DEFAULT;
            quartSell = SystemConstants.Signals.DEFAULT;
            quartSsell = SystemConstants.Signals.DEFAULT;
        }
        public void longTermSetup(decimal closePrice)
        {
            
                MA10.Add(closePrice);
                MA15.Add(closePrice);
                if (MA10._candles.Count == 10) { avgPointTen.Add(MA10.Value); }
                if (MA15._candles.Count == 15) { avgPointFifteen.Add(MA15.Value); }
                
            
        }
        public void movingUpdate(Object obj){

            AlgoDescriptor desc = obj as AlgoDescriptor;
            
            decimal bidVal = desc.bidValue;
            decimal askVal = desc.askValue;
            String duration = desc.duration;
            bool dayBased = desc.shortLong;
            String bidAsk = desc.bidAsk;
            bool shortSell = desc.shortSell;

            
            if (duration.Equals("MINUTES"))
            {
            
                MAMinutesBid.Add(bidVal);
                MAMinutesAsk.Add(askVal);
                avgPointMinutes.Add(MAMinutesBid.Value);
                avgPointMinutesAsk.Add(MAMinutesAsk.Value);
            
                mvBid.Add(bidVal);
                mvAsk.Add(askVal);
                avgPoints.Add(mvBid.Value);
                avgPointsAsk.Add(mvAsk.Value);
            }
            else
            {
                MAHourlyBid.Add(bidVal);
                MAHourlyAsk.Add(askVal);
                avgPointHours.Add(MAHourlyBid.Value);
                avgPointHoursAsk.Add(MAHourlyAsk.Value);
                mvBid.Add(bidVal);
                mvAsk.Add(askVal);
                avgPoints.Add(mvBid.Value);
                avgPointsAsk.Add(mvAsk.Value);
            }
                  
            
          if (avgPoints.Count >= 2)
            {
                GenerateTertiarySignal(shortSell);
                genSignal(duration, bidAsk,shortSell);
                getQuaternary(bidVal,askVal, shortSell);
            }

           }

        public decimal exitCondition(Trade t,Stock s)
        {
            Console.WriteLine("Prices are " + t.Price + " " + s.Bid + " " + s.Ask);
            decimal profitOrLoss = 0.0M;
            if(t.Buy){
                profitOrLoss = (t.Price - s.Ask)/t.Price;
                profitOrLoss *= 100M;

            }
            else{

                profitOrLoss = (t.Price - s.Bid)/t.Price;
                profitOrLoss *= 100M;
            }
            if(profitOrLoss>=1.0M || profitOrLoss<=-1.0M){

               exit = SystemConstants.Signals.EXIT;
               
            }
            return profitOrLoss;
           
        }
        public void setSupport(decimal val)
        {
            if (support > val)
            {
                support = val;
            }
        }
        public void setResistance(decimal val)
        {
            if (resistance < val)
            {
                resistance = val;
            }
        }
        public Stock GetRealTimeData(string symbol)
        {

            var marketDataManager = MarketDataManager.Instance;
            //return marketDataManager.getStock(symbol);
            return marketDataManager.getStockFromLocalCsv(symbol);
        }
        public decimal MakeTrade(Trade t,IStrategy strategy,Stock s)
        {

            Console.WriteLine("The entry quantity is " + t.Size);
            decimal profitOrLoss;
            s = GetRealTimeData(t.Stock);
            do 
            {
                
                StrategyManager.Instance.UpdateParams(t, s, strategy);
                profitOrLoss = exitCondition(t, s);
                if (pBuy == SystemConstants.Signals.BUY || (terBuy == SystemConstants.Signals.BUY||quartBuy == SystemConstants.Signals.BUY))
                    {
                        if (s.AskSize > int.Parse(trade.Size.ToString()))
                            trade.Size = 0;
                        else
                            trade.Size -= int.Parse(s.AskSize.ToString());
                        pBuy = SystemConstants.Signals.DEFAULT;
                        
                        quartBuy = SystemConstants.Signals.DEFAULT;
                        Console.WriteLine("The sizes of the stock" + s.AskSize + " " + s.BidSize);


                    }
                else if (pSell == SystemConstants.Signals.SELL || (terSell == SystemConstants.Signals.SELL || quartSell == SystemConstants.Signals.SELL))
                    {

                        Console.WriteLine("Entered the SALE but the size is "+s.BidSize);
                     // Logger.Out(String.Format("Stock: {0}, Quantity: {1}, Price: {2}, PnL: {3}", trade.Stock, trade.Size, trade.Price, trade.PnL));
                        if (t.Short)
                        {
                            if (s.BidSize > int.Parse(trade.Size.ToString()))
                                trade.Size = 0;
                            else
                                trade.Size -= int.Parse(s.BidSize.ToString());
                            pBuy = SystemConstants.Signals.BUY;
                           
                            quartBuy  = SystemConstants.Signals.BUY;
                        }
                        else
                        {
                            if (s.BidSize > int.Parse(trade.Size.ToString()))
                                trade.Size = 0;
                            else
                                trade.Size -= int.Parse(s.BidSize.ToString());
                           
                        }
                         pSell = SystemConstants.Signals.DEFAULT;
                        
                         quartSell = SystemConstants.Signals.DEFAULT;

                    }
              else if (pSsell==SystemConstants.Signals.SHORTSELL || (terSsell == SystemConstants.Signals.SHORTSELL || quartSsell == SystemConstants.Signals.SHORTSELL))
                    {
                        
                      //  Logger.Out(String.Format("Stock: {0}, Quantity: {1}, Price: {2}, PnL: {3}", trade.Stock, trade.Size, trade.Price, trade.PnL));
                        trade.Size += int.Parse(s.AskSize.ToString());
                        pSell = SystemConstants.Signals.SELL;
                       
                        quartSell = SystemConstants.Signals.SELL;


                    }
                
            }while(!(trade.Size == 0 || exit == SystemConstants.Signals.EXIT));

            trade.PnL = profitOrLoss;
            trade.Position = "CLOSED";
            Console.WriteLine("Trade Placed "+trade.PnL + " "+trade.Size);
            return profitOrLoss;
                
        }
        
        
        public void genSignal(String duration,String bidAsk,bool shortSell=false)
        {
            
            List<decimal> arrPrice = null;
            List<decimal> arrPriceAsk = null;
            if (duration.Equals("HOURLY"))
            {
                arrPrice = avgPointHours;
                arrPriceAsk = avgPointHoursAsk;
            }
            else
            {
                arrPrice = avgPointMinutes;
                arrPriceAsk = avgPointMinutesAsk;
            }

            Console.WriteLine("Primary Indicator " + avgPoints.Last() + " " + arrPrice.Last() + " " + avgPointsAsk.Last() + " " + arrPriceAsk.Last());

            if (avgPoints.Last() == arrPrice.Last() || ((avgPoints.ElementAt(avgPoints.Count - 2) - arrPrice.ElementAt(arrPrice.Count - 2)) * (avgPoints.Last() - arrPrice.Last())) < 0)
            {

                if (avgPoints.ElementAt(avgPoints.Count - 2) > arrPrice.ElementAt(arrPrice.Count - 2) && bidAsk.Equals("BID"))
                { 
                        pSell = SystemConstants.Signals.SELL;
                   //generate secondary short sell signal
                }
            }
            else if (avgPointsAsk.Last() == arrPriceAsk.Last() || ((avgPointsAsk.ElementAt(avgPointsAsk.Count - 2) - arrPriceAsk.ElementAt(arrPriceAsk.Count - 2)) * (avgPointsAsk.Last() - arrPriceAsk.Last())) < 0)
            {
                if (avgPointsAsk.ElementAt(avgPointsAsk.Count - 2) < arrPriceAsk.ElementAt(arrPriceAsk.Count - 2) && bidAsk.Equals("ASK") )
                {
                    pBuy = SystemConstants.Signals.BUY;
                    //generate secondary long buy signal
                }
                else if(avgPointsAsk.ElementAt(avgPointsAsk.Count - 2) > arrPriceAsk.ElementAt(arrPrice.Count - 2) && bidAsk.Equals("ASK"))
                {
                    pSsell = SystemConstants.Signals.SHORTSELL;
                }
            }
           
        }
  
    public class MovingAverage : Indicator
    {
        public int _length{get;set;}
        public Queue<Candle> _candles { get; set; }
        private Candle _newestCandle;
        private decimal _sumExceptNewest;
        

        /**
         * only median supported now
         */

        public MovingAverage(int timeSpan,String inputStock,bool dayBased=false)
        {

            _sumExceptNewest = 0.0M;
            if (!dayBased)
            {
                _length = (int)((decimal)(timeSpan) / (decimal)SystemConstants.queryFrequency);
                _candles = new Queue<Candle>(_length);
            }
            else
            {
                _length = (int)((decimal)(timeSpan));
                _candles = new Queue<Candle>(_length);
            }
            _sumExceptNewest = 0.0M;

            
        }

        public void Add(decimal val)
        {
            if (_candles.Count == _length)
            {
                _sumExceptNewest -= _candles.Dequeue().Median;
            }
            if (_candles.Count > 0)
            {
                _sumExceptNewest += _newestCandle.Median;
            }
           
            // TODO: avoid new
            _newestCandle = new Candle(val);
            _candles.Enqueue(_newestCandle);
            RecalculateValue();
        }

        

        private void RecalculateValue()
        {
            Value = (_sumExceptNewest + _newestCandle.Median) / _candles.Count;
        }

        public decimal Value { get; private set; }

       
    }
}
}
