using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREX.DAL;
using TREX.Entities;

namespace TREX.Utilities
{
    public class PortfolioManager
    {
        private static PortfolioManager _instance;
        private static PortfolioDAL _dal;

        private PortfolioManager() { 
        
        }
        public static PortfolioManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PortfolioManager();

                if (_dal == null)
                    _dal = PortfolioDAL.Instance;


                return _instance;
            }

        }

        public decimal? GetIncrementalBalanceByStock(string stock)
        {
            return _dal.GetIncrementalBalanceByStock(stock);
        }

        public int GetAvailableSizeByStock(string stock)
        {
            return _dal.GetAvailableSizeByStock(stock);
        }

        public int GetTotalSizeByStock(string stock)
        {
            return _dal.GetTotalSizeByStock(stock);
        }

        public List<string> GetAllStocksName()
        {
            return _dal.GetAllStocksName();
        }

        public List<PortfolioEntry> GetAllPortfolioEntries()
        {
            return _dal.GetAllPortfolioEntries();
        }

        public Stock GetStockBySymbol(string stock)
        {
            return _dal.GetStockBySymbol(stock);
        }

        public int AddStock(PortfolioEntry portfolio)
        {
            return _dal.AddPortfolioEntry(portfolio);
        }

        public int UpdatePortfolio(PortfolioEntry portfolio)
        {
            return _dal.UpdatePortfolio(portfolio);
        }

        public int DeleteStock(Stock stock)
        {
            return _dal.DeleteStock(stock);
        }

        public int DeleteStock(string stock)
        {
            return _dal.DeletePortfolioEntryByStock(stock);
        }


    }
}
