using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using TREX.Entities;
using TREX.Common;

namespace TREX.DAL
{
    internal class DALHelper
    {
        private static SqlConnection connection;

        public static SqlConnection GetSQLConnection()
        {
            if (connection != null)
                return connection;

            var connectionString = ConfigurationManager.AppSettings["CONNECTION_STRING"];
            
            try
            {
                connection = new SqlConnection(connectionString);
            }
            catch (SqlException ex)
            {
                Logger.Out(String.Format("An exception occurred: {0}", ex.Message));
                Console.WriteLine("An exception occurred: {0}", ex.Message);
            }

            Console.WriteLine("Connection has been established successfully");

            return connection;
            
        }

        public static DataTable GetQueryResult(string query) {

            try{
                var adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(query, connection);
                var dataSet = new DataSet();
                adapter.Fill(dataSet, "ResultTable");
                return dataSet.Tables["ResultTable"];
            }
            catch (Exception e) {
                Logger.Out("DATABASE ERROR: " + e.Message);
                Console.WriteLine("DATABASE ERROR: " + e.Message);
            }

            return null;
        }

        public static int GetNonQueryResult(string query)
        {
            try
            {
                var adapter = new SqlDataAdapter();
                adapter.UpdateCommand = connection.CreateCommand();
                adapter.UpdateCommand.CommandText = query;
                return adapter.UpdateCommand.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Logger.Out("DATABASE ERROR: " + e.Message);
                Console.WriteLine("DATABASE ERROR: " + e.Message);
            }

            return -1;
        }

        //Populate Trade objects
        public static List<Trade> GetTradeList(DataTable dataTable) {

            var tradeList = new List<Trade>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var trade = new Trade();

                trade.Id = (int)dataRow["id"];
                trade.Auto = (bool)dataRow["auto"];
                trade.Buy = (bool)dataRow["buy"];
                trade.WhenAsDate = (string)dataRow["when"];
                trade.Position = (string)dataRow["position"];
                trade.Size = (int)dataRow["size"];
                trade.StrategyID = (string)dataRow["strategyId"];
                trade.Stock = (string)dataRow["stock"];
                trade.Price = (decimal)dataRow["price"];
                trade.Short = (bool)dataRow["short"];
                trade.PnL = (decimal)dataRow["pnl"];

                tradeList.Add(trade);
            }
            return tradeList;
        }

        //Populate Strategy objects
        public static List<Strategy> GetStrategyList(DataTable dataTable)
        {

            var strategyList = new List<Strategy>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var strategy = new Strategy();

                strategy.Id = (int)dataRow["id"];
                strategy.Activated = (bool)dataRow["activated"];
                strategy.Type = (string)dataRow["type"];
                strategy.XmlConfig = (string)dataRow["xmlConfig"];
				strategy.Name = (string)dataRow["name"];
                strategy.Stock = (string)dataRow["stock"];
                strategy.Buy = (bool)dataRow["buy"];
                strategy.Short = (bool)dataRow["short"];
                strategy.Size = (int)dataRow["size"];


                strategyList.Add(strategy);
            }
            return strategyList;
        }

        //populate Portfolio objects
        public static List<PortfolioEntry> GetPortfolioEntryList(DataTable dataTable)
        {

            var portfolioList = new List<PortfolioEntry>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var portfolio = new PortfolioEntry();
                portfolio.Stock = (string)dataRow["stock"];
                portfolio.AvailableSize = (int)dataRow["availableSize"];
                portfolio.IncrementalBalance = (decimal)dataRow["incrementalBalance"];
                portfolio.TotalSize = (int)dataRow["totalSize"];

                portfolioList.Add(portfolio);
            }
            return portfolioList;
        }

        //populate Stock objects
        public static List<Stock> GetStockList(DataTable dataTable)
        {

            var stockList = new List<Stock>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var stock = new Stock();

                stock.Symbol = (string)dataRow["symbol"];
                stock.Ask = (decimal)dataRow["ask"];
                stock.Bid = (decimal)dataRow["bid"];
                stock.Country = (string)dataRow["country"];
                stock.Currency = (string)dataRow["currency"];
                stock.When = (string)dataRow["when"];
                stock.Name = (string)dataRow["name"];
                stock.Exchange = (string)dataRow["exchange"];
                stock.Vol = (int)dataRow["vol"];
                stock.Open = (decimal)dataRow["open"];
                stock.PrevClose = (decimal)dataRow["prevClose"];
                stock.Change = (decimal)dataRow["change"];
                stock.BidSize = (int)dataRow["bidSize"];
                stock.AskSize = (int)dataRow["askSize"];
                stock.Change = (decimal)dataRow["change"];
                stock.ChangePercentage = (decimal)dataRow["changePercentage"];
                stock.SummaryName = (string)dataRow["summaryName"];
                stock.SummaryValue = (string)dataRow["summaryValue"];

                stockList.Add(stock);
            }
            return stockList;
        }

    }
}
