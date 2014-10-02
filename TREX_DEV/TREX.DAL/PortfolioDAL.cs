using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using TREX.Entities;

namespace TREX.DAL
{
    public class PortfolioDAL
    {
        private static SqlConnection _connection;
        private static PortfolioDAL _instance;

        private PortfolioDAL() { }

        public static PortfolioDAL Instance
        {
            get
            {
                if (_connection == null)               
                    _connection = DALHelper.GetSQLConnection();
                

                if(_instance == null)
                    _instance = new PortfolioDAL();

                return _instance;
            }

        }

        public decimal? GetIncrementalBalanceByStock(string stock) {

            var query = string.Format("SELECT incrementalBalance FROM Portfolio WHERE stock = '{0}' ", stock);
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            return (decimal) dataTable.Rows[0]["incrementalBalance"];
        }

        public int GetAvailableSizeByStock(string stock)
        {
            var query = string.Format("SELECT availableSize FROM Portfolio WHERE stock = '{0}' ", stock);
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return -1;

            return (int) dataTable.Rows[0]["availableSize"];
        }

        public int GetTotalSizeByStock(string stock)
        {
            var query = string.Format("SELECT totalSize FROM Portfolio WHERE stock = '{0}' ", stock);
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return -1;

            return (int)dataTable.Rows[0]["totalSize"];
        }
       
        public List<string> GetAllStocksName()
        {
            var query = "SELECT DISTINCT stock FROM Portfolio  ";
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            List<string> stockList = new List<string>();
            foreach(System.Data.DataRow row in dataTable.Rows){
                stockList.Add(row[0].ToString());
            }

            return stockList;
        }

        public List<PortfolioEntry> GetAllPortfolioEntries()
        {
            var query = "SELECT * FROM Portfolio";
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            var portfolioList = DALHelper.GetPortfolioEntryList(dataTable);

            return portfolioList;
        }

        public Stock GetStockBySymbol(string stock)
        {
            var query = string.Format("SELECT * FROM Stock WHERE symbol IN (SELECT stock FROM Portfolio WHERE stock = '{0}')", stock);
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            var stockList = DALHelper.GetStockList(dataTable);

            return stockList[0];
        }

        public int AddPortfolioEntry(PortfolioEntry portfolio)
        {

            var query = string.Format("INSERT INTO Portfolio" +
                "(availableSize, incrementalBalance, stock, totalSize) VALUES " +
                "('{0}', '{1}', '{2}', '{3}')",                
                portfolio.AvailableSize,
                portfolio.IncrementalBalance,
                portfolio.Stock,          
                portfolio.TotalSize
                );

            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return -1;

            return 1;
        }

        public PortfolioEntry GetPortfolioEntryByStock(string stock)
        {
            var query = string.Format("SELECT * FROM Portfolio where stock = '{0}'", stock);
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            var portfolioEntryList = DALHelper.GetPortfolioEntryList(dataTable);

            return portfolioEntryList[0];
        }

        public bool CheckPortfolioEntryByStock(string stock) {
            var query = string.Format("SELECT * FROM Portfolio where stock = '{0}'", stock);
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return false;

            var portfolioEntryList = DALHelper.GetPortfolioEntryList(dataTable);

            return (portfolioEntryList.Count == 1) ? true : false;

        }

        public int UpdatePortfolio(PortfolioEntry portfolio)
        {

            var query = string.Format("UPDATE Portfolio  " +
                "SET availableSize = '{0}', incrementalBalance = '{1}', totalSize = '{2}' WHERE stock = '{3}' ",
                portfolio.AvailableSize,
                portfolio.IncrementalBalance,
                portfolio.TotalSize,
                portfolio.Stock
                );

            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return -1;

            return 1;
        }
     
        public int DeleteStock(Stock stock)
        {
            var query = string.Format("DELETE FROM Portfolio WHERE stock = '{0}'", stock.Symbol);
              
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return -1;

            return 1;
        }

        public int DeletePortfolioEntryByStock(string stock)
        {
            var query = string.Format("DELETE FROM Portfolio WHERE stock = '{0}'", stock);

            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return -1;

            return 1;
        }





    }
}
