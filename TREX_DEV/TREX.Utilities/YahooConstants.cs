using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TREX.Utilities
{
    public static class YahooConstants
    {    
        public const string Symbol = "s0";
        public const string Name = "n0";
        public const string StockExchange = "x0";        
        public const string Currency = "c4";
        public const string DayHigh = "h0";
        public const string DayLow = "g0";
        public const string Open = "o0";
        public const string AskPrice = "a0";
        public const string BidPrice = "b0";
        public const string AskSize = "a5";
        public const string BidSize = "b6";
        public const string MarketCap = "j1";
        public const string Volume = "v0";        
        public const string RtAskPrice = "b2";
        public const string RtBidPrice = "b30";
        public const string RtChangeInPercent = "k2";
        public const string RtChange = "c6";
        //public const string TradeDate = "d2";
        public const string PrevClose = "p0";
        public const string RtDayRange = "m2";

        /**
         * Mapping property name with value
         */
        public static readonly IDictionary<string, string> PropertyMap = new Dictionary<string, string>()
        {
            {"Symbol",          YahooConstants.Symbol},
            {"Name",            YahooConstants.Name},
            {"StockExchange",   YahooConstants.StockExchange},
            {"Currency",        YahooConstants.Currency},
            {"DayHigh",         YahooConstants.DayHigh},
            {"DayLow",          YahooConstants.DayLow},
            {"Open",            YahooConstants.Open},
            {"AskPrice",        YahooConstants.AskPrice},
            {"BidPrice",        YahooConstants.BidPrice},
            {"AskSize",         YahooConstants.AskSize},
            {"BidSize",         YahooConstants.BidSize},
            {"MarketCap",       YahooConstants.MarketCap},
            {"Volume",          YahooConstants.Volume},
            {"RtAskPrice",      YahooConstants.RtAskPrice},
            {"RtBidPrice",      YahooConstants.RtBidPrice},
            {"RtChangeInPercent", YahooConstants.RtChangeInPercent},
            {"RtChange",        YahooConstants.RtChange},
            //{"TradeDate",       YahooConstants.TradeDate},
            {"PrevClose",       YahooConstants.PrevClose},
            {"RtDayRange",      YahooConstants.RtDayRange}
        };


        /**
         * Define a fix order for retrieving and parsing real time data
         * 
         * IMPORTANT: TO FIX THE COMMA PROBLEM (3,000)
         * ORDER OF PROPERTIES MUST BE IN THIS FORMAT : string | num | string | num | ..
         * 
         */ 
        public static readonly IDictionary<string, int> RequestOrder = new Dictionary<string, int>()
        {
            // string and number should be alternating

            // string first
            {"Symbol",          1},
            {"Name",            3},
            {"StockExchange",   5},
            {"Currency",        7},                        
            {"RtChangeInPercent", 9},
            {"RtChange",        11}, 
            {"RtDayRange",      13},

            // then, number
            {"Open",            0},
            {"AskPrice",        2},
            {"BidPrice",        4},
            {"AskSize",         6},
            {"BidSize",         8},            
            {"Volume",          10},
            {"PrevClose",       12},
            {"MarketCap",       14},

            // future use, remember to keep alternating format of (string | num | string)                        
            //{"DayHigh",         -},
            //{"DayLow",          -},
        };        

        /**
         * Number of properties requested in real-time data
         */ 
        public static readonly int RequestPropertyCount = RequestOrder.Count;

        /**
         * Concatenate code of properties for URL to request for real-time data 
         * 
         * Hint: Use reflection to get all fields, which have names that appears in the RequestOrder
         * 
         */
        private static string _requestPropertyString = string.Empty;
        public static string RequestPropertyString
        {
            get
            {
                if (string.IsNullOrEmpty(_requestPropertyString))
                {
                    string[] propertyString = new string[RequestPropertyCount];
                    foreach (var pair in RequestOrder)
                    {
                        propertyString[pair.Value] = PropertyMap[pair.Key];
                    }

                    _requestPropertyString = string.Join("", propertyString);
                }
                

                return _requestPropertyString;
            }
        }
            //typeof(YahooConstants).GetFields().Where(field => YahooConstants.RequestOrder.ContainsKey(field.Name)).
            //Aggregate (new StringBuilder(), (strBuilder, field) => 
            //    strBuilder.Append((string)field.GetValue(null)), strBuilder => strBuilder.ToString());

        
        /**
         * Note:    The order of properties in historical query is pre-defined in Yahoo, 
         *          then it is hard-coded here for CSV parser
         */
        public static readonly IDictionary<string, int> HistoricalOrder = new Dictionary<string, int>()
        {
            {"Date",        0},
            {"Open",        1},
            {"High",        2},
            {"Low",         3},
            {"Close",       4},
            {"Volume",      5},
            {"AdjClose",    6}
        };
    }
}
