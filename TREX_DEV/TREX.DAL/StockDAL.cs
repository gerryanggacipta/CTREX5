using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREX.Entities;

namespace TREX.DAL
{
    public class StockDAL
    {
        private static SqlConnection _connection;
        private static StockDAL _instance;

        private StockDAL() { }

        public static StockDAL Instance
        {
            get
            {
                if (_connection == null)               
                    _connection = DALHelper.GetSQLConnection();
                

                if(_instance == null)
                    _instance = new StockDAL();

                return _instance;
            }

        }

        public List<Stock> GetAllStocks()
        {
            var query = "SELECT * FROM Stock";
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            var stockList = DALHelper.GetStockList(dataTable);

            return stockList;
        }

        public List<Stock> GetStocksBySymbol(string symbol)
        {
            var query = string.Format("SELECT * FROM Stock WHERE symbol = '{0}'", symbol);
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            var stockList = DALHelper.GetStockList(dataTable);

            return stockList;
        }

        public List<Stock> GetStocksByDate(string startDateTime, string endDateTime)
        {

            var query = string.Format("SELECT * FROM Stock WHERE [when] BETWEEN {0} AND {1}", startDateTime, startDateTime);
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            var stockList = DALHelper.GetStockList(dataTable);

            return stockList;
        }

        public decimal? GetHighestBidBySymbol(string symbol) { 
            var query = string.Format("SELECT max(bid) FROM Stock WHERE symbol = '{0}'", symbol);
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;


            return (decimal)dataTable.Rows[0][0];
        }

        public decimal? GetLowestBidBySymbol(string symbol)
        {
            var query = string.Format("SELECT min(bid) FROM Stock WHERE symbol = '{0}'", symbol);
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            return (decimal)dataTable.Rows[0][0];
        }

        public decimal? GetHighestAskBySymbol(string symbol)
        {
            var query = string.Format("SELECT max(ask) FROM Stock WHERE symbol = '{0}'", symbol);
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            return (decimal)dataTable.Rows[0][0];
        }

        public decimal? GetLowestAskBySymbol(string symbol)
        {
            var query = string.Format("SELECT min(ask) FROM Stock WHERE symbol = '{0}'", symbol);
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            return (decimal)dataTable.Rows[0][0];
        }

        public int AddStock(Stock stock)
        {

            var query = string.Format("INSERT INTO Stock" +
                "(ask, bid, country, currency, [when], exchange, name, symbol, vol, [open], closeEOD, change) VALUES " +
                "('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}')",
                stock.Ask,
                stock.Bid,
                stock.Country,
                stock.Currency,
                stock.When,
                stock.Exchange,
                stock.Name,
                stock.Symbol,
                stock.Vol,
                stock.Open,
                stock.PrevClose,
                stock.Change
                );

            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return -1;

            return 1;
        }
        public int DeleteStocksBySymbol(string symbol)
        {

            var query = string.Format("DELETE FROM Stock WHERE symbol = '{0}'", symbol);

            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return -1;

            return dataTable.Rows.Count;
        }
        
    }
}
