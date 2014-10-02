using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREX.Entities;
using System.Collections.ObjectModel;
using TREX.Common;
using System.Configuration;
using System.IO;

namespace TREX.Utilities
{
    public class MarketDataManager
    {
        private static MarketDataManager _instance = null;

        private IProviderManager provider = null;

        public List<string> stockList { get; private set; }
        public event Action<List<Stock>> LiveDataArrived;
        
        // Number to load csv file
        public static string csvRootPath;
        public static int currentCsvNum;

        private MarketDataManager()
        {
            // Choose Yahoo provider
            provider = new YahooManager();
        }
        public static MarketDataManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MarketDataManager();
                    csvRootPath = (string)ConfigurationManager.AppSettings["CSV_ROOT_PATH"];
                    currentCsvNum = 1;
                }
                return _instance;
            }
        }

        public void setSymbolList(List<string> symbolList)
        {
            this.stockList = symbolList;
        }

        public void Run()
        {
            while (true)
            {
                List<Stock> liveData = getStocks();                                
                LiveDataArrived(liveData);
                System.Threading.Thread.Sleep(SystemConstants.MARKET_FEED_SLEEP_INTERVAL);
            }            
        }

        public Stock getStock(string symbol)
        {
            return provider.getRealtimeSingleStock(symbol);
        }

        public List<Stock> getStocks()
        {
            return provider.getRealtimeMultipleStocks(stockList);
        }

        public List<Stock> getStocks(List<string> symbolList)
        {
            return provider.getRealtimeMultipleStocks(symbolList);
        }

        public List<HistStock> getHistoricalStocks(string startDateTime, string endDateTime, HistoricalPeriod dayOrWeek = HistoricalPeriod.Day)
        {
            return provider.getHistoricalStocks(stockList, startDateTime, endDateTime);
        }

        public List<HistStock> getHistoricalStocks(string symbol, string startDateTime, string endDateTime, HistoricalPeriod dayOrWeek = HistoricalPeriod.Day)
        {
            return provider.getHistoricalStocks(symbol, startDateTime, endDateTime);
        }

        public List<HistStock> getHistoricalStocks(List<string> symbolList, string startDateTime, string endDateTime, HistoricalPeriod dayOrWeek = HistoricalPeriod.Day)
        {
            return provider.getHistoricalStocks(stockList, startDateTime, endDateTime, dayOrWeek);
        }
        
        // Historical data, period = 1 day
        public Queue<Candle> getStockCandles(String symbol,string startDateTime, string endDateTime,bool dayBased=false)
        {
            //perform logic to query either csv or db
            return new Queue<Candle>();
        }

        public Stock getStockFromLocalCsv(string symbol)
        {
            string filePath = csvRootPath + "\\" + currentCsvNum + ".csv";
            
            if (!File.Exists(filePath))
            {
                currentCsvNum = 1;
                filePath = csvRootPath + "\\" + currentCsvNum + ".csv";
            } 
            else
            {
                // Point to next
                currentCsvNum += 1;
            }

            Console.WriteLine(">> Reading from file : " + filePath);

            // Read all csv data from the file
            string csvData = System.IO.File.ReadAllText(filePath);

            // Return stock, if cannot find, return null
            return CSVManager.ParseLocalCsvDataForSingleStock(csvData, symbol);
        }


    }
}
