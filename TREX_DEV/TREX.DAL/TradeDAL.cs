using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREX.Entities;
using TREX.Common;

namespace TREX.DAL
{
    public class TradeDAL
    {
        private static SqlConnection _connection;
        private static TradeDAL _instance;

        private TradeDAL() { }

        public static TradeDAL Instance
        {
            get
            {
                if (_connection == null)               
                    _connection = DALHelper.GetSQLConnection();
                

                if(_instance == null)
                    _instance = new TradeDAL();

                return _instance;
            }

        }
    
        public List<Trade> GetAllTrades(){

            var query = "SELECT * FROM Trade";
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            var tradeList = DALHelper.GetTradeList(dataTable);

            return tradeList;
        }

        public Trade GetTradeById(int id)
        {

            var query = string.Format("SELECT * FROM Trade WHERE id = '{0}'", id);
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            var tradeList = DALHelper.GetTradeList(dataTable);

            return tradeList[0];
        }

        public List<Trade> GetTradeByStrategyId(string id)
        {

            var query = string.Format("SELECT * FROM Trade WHERE strategyId = '{0}'", id);
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            var tradeList = DALHelper.GetTradeList(dataTable);
            
            return tradeList;
        }

        public List<Trade> GetTradesByDate(string startDateTime, string endDateTime)
        {

            var query = string.Format("SELECT * FROM Trade WHERE [when] BETWEEN '{0}' AND '{1}'", startDateTime, startDateTime);
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            var tradeList = DALHelper.GetTradeList(dataTable);

            return tradeList;
        }

        public List<Trade> GetTradesByType(bool isAuto)
        {

            var query = string.Format("SELECT * FROM Trade WHERE auto = '{0}'", isAuto);
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            var tradeList = DALHelper.GetTradeList(dataTable);

            return tradeList;
        }

        public List<Trade> GetTradesBySymbol(string symbol)
        {

            var query = string.Format("SELECT * FROM Trade WHERE stock = '{0}'", symbol);
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            var tradeList = DALHelper.GetTradeList(dataTable);

            return tradeList;
        }

        public List<Trade> GetOngoingTrades()
        {

            var query = string.Format("SELECT * FROM Trade WHERE position = '{0}'", SystemConstants.TradePosition.OPEN);
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            var tradeList = DALHelper.GetTradeList(dataTable);

            return tradeList;
        }

        public void InsertTrade(Trade trade) {
            var query = string.Format("INSERT INTO Trade" +
                "(auto, buy, [when], position, size, strategyId, stock, price, pnl, short) VALUES " +
                "('{0}', '{1}', '{2}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}')", 
                trade.Auto,
                trade.Buy,
                trade.WhenAsDate,
                trade.Id,
                trade.Position,
                trade.Size,
                trade.StrategyID,
                trade.Stock,
                trade.Price,
                trade.PnL,
                trade.Short
                );
            var dataTable = DALHelper.GetQueryResult(query);
        }

        public void InsertTrade(List<Trade> tradeList){

            foreach (var trade in tradeList) {
                var query = string.Format("INSERT INTO Trade" +
                "(auto, buy, [when], position, size, strategyId, stock, price, pnl, short) VALUES " +
                "('{0}', '{1}', '{2}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}')",
                trade.Auto,
                trade.Buy,
                trade.WhenAsDate,
                trade.Id,
                trade.Position,
                trade.Size,
                trade.StrategyID,
                trade.Stock,
                trade.Price,
                trade.PnL,
                trade.Short
                );

                var dataTable = DALHelper.GetQueryResult(query);
            }           
        }

        public void UpdateTrade(Trade trade)
        {
            var query = string.Format("UPDATE Trade" +
                "SET auto = '{0}', buy = '{1}', [when] = '{2}', position = '{4}', size = '{5}', strategyId = '{6}', stock = '{7}', price = '{8}' WHERE ([when] = '{2}' AND stock = '{7}')",
                trade.Auto,
                trade.Buy,
                trade.WhenAsDate,
                trade.Id,
                trade.Position,
                trade.Size,
                trade.StrategyID,
                trade.Stock,
                trade.Price
                );
            var dataTable = DALHelper.GetQueryResult(query);
        }

        public void UpdateTrade(List<Trade> tradeList)
        {                     
            foreach (var trade in tradeList)
            {
                var query = string.Format("UPDATE Trade" +
                "SET auto = '{0}', buy = '{1}', [when] = '{2}', position = '{4}', size = '{5}', strategyId = '{6}', stock = '{7}', price = '{8}', pnl = '{9}', short = '{10}'",
                trade.Auto,
                trade.Buy,
                trade.WhenAsDate,
                trade.Id,
                trade.Position,
                trade.Size,
                trade.StrategyID,
                trade.Stock,
                trade.Price,
                trade.PnL,
                trade.Short
                );

                DALHelper.GetQueryResult(query);

            }
        }



    }
}
