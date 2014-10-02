using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREX.Entities;

namespace TREX.Utilities
{
    public static class CSVManager
    {
        public static List<Stock> parseRealtimeDataToMultipleStocks(string csvData)
        {
            List<Stock> dataOutput = new List<Stock>();

            string[] rows = csvData.Replace("\r", "").Split('\n');

            foreach (string row in rows)
            {                
                Stock stock = parseRealtimeDataToSingleStock(row);

                // Check if parse is ok, by checking stock name
                if (stock != null)
                {
                    dataOutput.Add(stock);
                }
            }

            return dataOutput;
        }

        public static Stock parseRealtimeDataToSingleStock(string csvData)
        {
            Stock stock = null;

            string[] rows = csvData.Replace("\r", "").Split('\n');

            string row = rows[0];

            // Check if this entry exists                
            if (!string.IsNullOrEmpty(row))
            {
                string[] delimiter = new string[2] { ",\"", "\"," };
                string[] cols = row.Split(delimiter, StringSplitOptions.None);

                // Proceed only when it has enough fields
                if (cols.Length == YahooConstants.RequestPropertyCount)
                {
                    /*
                     * Parse to Stock object
                     * Refer to YahooConstants to match property name and request order
                     */

                    stock = ParseStockFromString(cols);
                }
            }

            return stock;
        }

        private static Stock ParseStockFromString(string[] cols)
        {
            Stock stock = new Stock();

            // process strings first
            string symbol = cols[YahooConstants.RequestOrder["Symbol"]];
            string name = cols[YahooConstants.RequestOrder["Name"]];
            string exchange = cols[YahooConstants.RequestOrder["StockExchange"]];
            string currency = cols[YahooConstants.RequestOrder["Currency"]];
            string change = cols[YahooConstants.RequestOrder["RtChange"]];
            string changePercent = cols[YahooConstants.RequestOrder["RtChangeInPercent"]];
            string marketCap = cols[YahooConstants.RequestOrder["MarketCap"]];

            stock.Symbol = escapeDoubleQuoteFromString(symbol);
            stock.Name = escapeDoubleQuoteFromString(name);
            stock.Exchange = escapeDoubleQuoteFromString(exchange);
            stock.Currency = escapeDoubleQuoteFromString(currency);
                        
            stock.Change = change == "N/A" ? 0 : Convert.ToDecimal(change);
            stock.ChangePercentage = changePercent == "N/A" ? 0 : 
                Convert.ToDecimal(changePercent.Substring(6, changePercent.Length - 8));                        
            stock.MarketCap = marketCap == "N/A" ? "0" : marketCap;


            // process number later
            string ask = escapeCommaFromNumber(cols[YahooConstants.RequestOrder["AskPrice"]]);
            string bid = escapeCommaFromNumber(cols[YahooConstants.RequestOrder["BidPrice"]]);
            string askSize = escapeCommaFromNumber(cols[YahooConstants.RequestOrder["AskSize"]]);
            string bidSize = escapeCommaFromNumber(cols[YahooConstants.RequestOrder["BidSize"]]);
            string vol = escapeCommaFromNumber(cols[YahooConstants.RequestOrder["Volume"]]);
            string prevClose = escapeCommaFromNumber(cols[YahooConstants.RequestOrder["PrevClose"]]);
            string open = escapeCommaFromNumber(cols[YahooConstants.RequestOrder["Open"]]);

            stock.Ask = ask == "N/A" ? 0 : Convert.ToDecimal(ask);
            stock.Bid = bid == "N/A" ? 0 : Convert.ToDecimal(bid);
            stock.AskSize = askSize == "N/A" ? 0 : Convert.ToDecimal(askSize);
            stock.BidSize = bidSize == "N/A" ? 0 : Convert.ToDecimal(bidSize);
            stock.Vol = vol == "N/A" ? 0 : Convert.ToInt64(vol);
            stock.PrevClose = prevClose == "N/A" ? 0 : Convert.ToDecimal(prevClose);
            stock.Open = open == "N/A" ? 0 : Convert.ToDecimal(open);

            // Lastly, update it with DateTime.Now

            stock.When = DateTimeManager.getDateTimeNowToSGT();

            // Debug only
            //string output = string.Join(" , ",
            //    stock.Symbol,
            //    stock.Name,
            //    stock.Exchange,
            //    stock.Currency,
            //    stock.Country,
            //    stock.Change,
            //    stock.ChangePercentage,
            //    stock.MarketCap,
            //    stock.Ask,
            //    stock.Bid,
            //    stock.AskSize,
            //    stock.BidSize,
            //    stock.Vol,
            //    stock.PrevClose,
            //    stock.Open);
            //Console.WriteLine(output);
            return stock;
        }

        private static string escapeCommaFromNumber(string str) { return str.Replace(",", ""); }
        private static string escapeDoubleQuoteFromString(string str) { return str.Replace("\"", ""); }
        
        /**
         * Remember this csv has 1 line of header
         * 
         * The order of properties is pre-defined in Yahoo API
         * [Date,Open,High,Low,Close,Volume,Adj Close]
         * 
         */
        public static List<HistStock> parseHistoricalDataToMultipleStocks(string csvData)
        {
            List<HistStock> dataOutput = new List<HistStock>();

            string[] rows = csvData.Replace("\r", "").Split('\n');

            bool isHeader = true;
            foreach (string row in rows)
            {
                if (isHeader)
                {
                    isHeader = false;
                    continue;
                }

                // Check if this entry exists
                if (string.IsNullOrEmpty(row)) continue;

                string[] cols = row.Split(',');

                // Ignore this entry because it does not have enough fields
                // Always has 7 fields: [Date,Open,High,Low,Close,Volume,Adj Close]  
                if (cols.Length != 7) continue;

                /*
                 * Parse to Stock object
                 * Ensure the order of properties is as pre-defined
                 * [Date,Open,High,Low,Close,Volume,Adj Close]                
                 * 
                 */
                HistStock stock = new HistStock();

                stock.Open = cols[YahooConstants.HistoricalOrder["Open"]] == "N/A" ? 0 :
                    Convert.ToDecimal(cols[YahooConstants.HistoricalOrder["Open"]]);
                stock.High = cols[YahooConstants.HistoricalOrder["High"]] == "N/A" ? 0 :
                    Convert.ToDecimal(cols[YahooConstants.HistoricalOrder["High"]]);
                stock.Low = cols[YahooConstants.HistoricalOrder["Low"]] == "N/A" ? 0 :
                    Convert.ToDecimal(cols[YahooConstants.HistoricalOrder["Low"]]);
                stock.Close = cols[YahooConstants.HistoricalOrder["Close"]] == "N/A" ? 0 :
                    Convert.ToDecimal(cols[YahooConstants.HistoricalOrder["Close"]]);
                stock.Vol = cols[YahooConstants.HistoricalOrder["Volume"]] == "N/A" ? 0 :
                    Convert.ToDecimal(cols[YahooConstants.HistoricalOrder["Volume"]]);
                stock.AdjClose = cols[YahooConstants.HistoricalOrder["AdjClose"]] == "N/A" ? 0 :
                    Convert.ToDecimal(cols[YahooConstants.HistoricalOrder["AdjClose"]]);

                stock.When = cols[YahooConstants.HistoricalOrder["Date"]];

                dataOutput.Add(stock);
            }

            return dataOutput;
        }

        public static Stock ParseLocalCsvDataForSingleStock(string csvData, string symbol)
        {
            Stock result = null;

            string[] rows = csvData.Replace("\r", "").Split('\n');

            foreach (string row in rows)
            {
                // Check if this entry exists                
                if (!string.IsNullOrEmpty(row))
                {
                    string[] delimiter = new string[2] { ",\"", "\"," };
                    string[] cols = row.Split(delimiter, StringSplitOptions.None);

                    // Proceed only when it has enough fields
                    if (cols.Length == YahooConstants.RequestPropertyCount)
                    {
                        string currentSymbol = escapeDoubleQuoteFromString(cols[YahooConstants.RequestOrder["Symbol"]]);
                        if (currentSymbol == symbol)
                        {
                            result = ParseStockFromString(cols);

                            // stop and return here immediately
                            Console.WriteLine(">> Found it! Symbol = " + symbol);

                            return result;
                        }
                    }
                }
            }

            return result;
        }
    }
}
