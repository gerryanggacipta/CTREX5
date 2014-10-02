using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using TREX.Entities;

namespace TREX.Utilities
{
    public class YahooManager : IProviderManager
    {
        /**
         * Note     {0} : stock symbol(s)
         *          {1} : properties - refer to YahooConstants
         */          
        private static readonly string _realtimeURL = 
            @"http://finance.yahoo.com/d/quotes.csv?s={0}&f={1}";

        /**
         * Note     {0} : symbol(s)
         *          {1} : start month (start from 00 to 11) - Need minus 1
         *          {2} : start day
         *          {3} : start year (format yyyy)
         *          {4} : end month (start from 00 to 11)
         *          {5} : end day
         *          {6} : end year
         *          {7} : trading period ("d" for daily, "w" for weekly        
         */
        private static readonly string _historicalUrl = 
            @"http://ichart.finance.yahoo.com/table.csv?s={0}&a={1}&b={2}&c={3}&d={4}&e={5}&f={6}&g={7}&ignore=.csv";
        

        public YahooManager() { }

        //public void setupConnection() { }

        //public void disposeConnection() { }  

        private string getCsvFromUrl(string url)
        {
            //Console.WriteLine(">> Start getting Csv from Url:\n" + url);
            //Console.WriteLine(">> " + DateTime.Now.ToString());

            string csvData;

            try
            {

                /*
                // Create a request for the URL. 
                WebRequest request = WebRequest.Create(url);
                // If required by the server, set the credentials.
                request.Credentials = CredentialCache.DefaultCredentials;
                // Get the response.
                WebResponse response = request.GetResponse();
                // Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                csvData = reader.ReadToEnd();

                reader.Close();
                response.Close();
                */

                using (WebClient webClient = new WebClient())
                {
                    csvData = webClient.DownloadString(url);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                csvData = "";
            }

            //Console.WriteLine(">> Data received");
            //Console.WriteLine(">> " + DateTime.Now.ToString());

            return csvData;
        }


        #region Historical data, using _historicalUrl
        
        private string constructUrl(string symbol, string startDateTime, string endDateTime, HistoricalPeriod dayOrWeek = HistoricalPeriod.Day)
        {
            // Factorize format [yyyy, mm, dd]
            string[] startDate = DateTimeManager.factorizeDate(startDateTime);
            string[] endDate = DateTimeManager.factorizeDate(endDateTime);
            string period = dayOrWeek == HistoricalPeriod.Day ? "d" : "w";

            return string.Format(_historicalUrl,
                symbol,
                int.Parse(startDate[1]) - 1,    // Note: Month starts from 0 to 11   
                startDate[2],
                startDate[0],
                int.Parse(endDate[1]) - 1,
                endDate[2],
                endDate[0],
                period);
        }
        
        //private string constructUrl(List<string> symbolList, string startDateTime, string endDateTime, HistoricalPeriod dayOrWeek = HistoricalPeriod.Day)
        //{
        //    return constructUrl(string.Join(",", symbolList), startDateTime, endDateTime, dayOrWeek);
        //}
        

        /**
         * A strategy for getting a quote at exact day is
         * compute the next day (= dateTime + 1), and get the period.
         * So Yahoo will return only historical data for current dateTime
         */ 
        private string constructUrl(string symbol, string dateTime)
        {
            DateTime dateTimeObj = DateTimeManager.getDateTimeObj(dateTime);
            DateTime nextDateTimeObj = dateTimeObj.AddDays(1);
            string nextDateTime = nextDateTimeObj.ToString("yyyyMMdd");
            return constructUrl(symbol, dateTime, nextDateTime, HistoricalPeriod.Day);
        }
        
        //private string constructUrl(List<string> symbolList, string dateTime)
        //{
        //    return constructUrl(string.Join(",", symbolList), dateTime);
        //}        

        public List<HistStock> getHistoricalStocks(string symbol, string startDateTime, string endDateTime, HistoricalPeriod dayOrWeek)
        {
            string url = constructUrl(symbol, startDateTime, endDateTime, dayOrWeek);
            string csvData = getCsvFromUrl(url);
            List<HistStock> histStockList = CSVManager.parseHistoricalDataToMultipleStocks(csvData);
            foreach (HistStock histStock in histStockList)
            {
                // Need to set symbol into histStock
                // because data received does not have Symbol column
                histStock.Symbol = symbol;
            }
            return histStockList;
        }

        public List<HistStock> getHistoricalStocks(string symbol, string dateTime)
        {
            string url = constructUrl(symbol, dateTime);
            string csvData = getCsvFromUrl(url);
            List<HistStock> histStockList = CSVManager.parseHistoricalDataToMultipleStocks(csvData);
            foreach (HistStock histStock in histStockList)
            {
                // Need to set symbol into histStock
                // because data received does not have Symbol column
                histStock.Symbol = symbol;
            }
            return histStockList;
        }

        public List<HistStock> getHistoricalStocks(List<string> symbolList, string startDateTime, string endDateTime, HistoricalPeriod dayOrWeek)
        {
            List<HistStock> histStocks = new List<HistStock>();

            foreach (string symbol in symbolList)
            {
                string url = constructUrl(symbol, startDateTime, endDateTime, dayOrWeek);
                string csvData = getCsvFromUrl(url);
                List<HistStock> histStockList = CSVManager.parseHistoricalDataToMultipleStocks(csvData);
                foreach (HistStock histStock in histStockList)
                {
                    // Need to set symbol into histStock
                    // because data received does not have Symbol column
                    histStock.Symbol = symbol;
                }                
                histStocks.AddRange(histStockList);
            }

            return histStocks;
        }
       
        public List<HistStock> getHistoricalStocks(List<string> symbolList, string dateTime)
        {
            List<HistStock> histStocks = new List<HistStock>();

            foreach (string symbol in symbolList)
            {
                string url = constructUrl(symbol, dateTime);
                string csvData = getCsvFromUrl(url);
                List<HistStock> histStockList = CSVManager.parseHistoricalDataToMultipleStocks(csvData);
                foreach (HistStock histStock in histStockList)
                {
                    // Need to set symbol into histStock
                    // because data received does not have Symbol column
                    histStock.Symbol = symbol;
                }
                histStocks.AddRange(histStockList);
            }

            return histStocks;
        }

        #endregion


        #region Real-time data, using _realtimeUrl

        private string constructUrl(string symbol)
        {
            return string.Format(YahooManager._realtimeURL, symbol, YahooConstants.RequestPropertyString);
        }
        
        private string constructUrl(List<string> symbolList)
        {
            return constructUrl(string.Join(",", symbolList));
        }

        public Stock getRealtimeSingleStock(string symbol)
        {            
            string url = constructUrl(symbol);
            string csvData = getCsvFromUrl(url);
            return CSVManager.parseRealtimeDataToSingleStock(csvData);
        }

        public List<Stock> getRealtimeMultipleStocks(List<string> symbolList)
        {
            string url = constructUrl(symbolList);
            string csvData = getCsvFromUrl(url);
            //string csvData = File.ReadAllText(MarketDataManager.csvRootPath + "\\" + MarketDataManager.currentCsvNum + ".csv");
            return CSVManager.parseRealtimeDataToMultipleStocks(csvData);
        }

        #endregion

    }
}

