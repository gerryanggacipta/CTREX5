using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREX.Entities;

namespace TREX.DAL
{
    public class StrategyDAL
    {
        private static SqlConnection _connection;
        private static StrategyDAL _instance;

        private StrategyDAL() { }
       
        public static StrategyDAL Instance
        {
            get
            {
                if (_connection == null)               
                    _connection = DALHelper.GetSQLConnection();
                

                if(_instance == null)
                    _instance = new StrategyDAL();

                return _instance;
            }

        }

        public Strategy GetStrategyById(int id){

            var query = string.Format("SELECT * FROM Strategy WHERE id = '{0}'", id);
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            var strategyList = DALHelper.GetStrategyList(dataTable);

            return strategyList[0];
        }

        public List<Strategy> GetAllStrategies() {
            var query = "SELECT * FROM Strategy";
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            var strategyList = DALHelper.GetStrategyList(dataTable);

            return strategyList;
        }

        public List<Strategy> GetStrategiesByType(string type)
        {
            var query = string.Format("SELECT * FROM Strategy WHERE type = '{0}'", type);
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            var strategyList = DALHelper.GetStrategyList(dataTable);

            return strategyList;
        }

        public int InsertStrategy(Strategy strategy)
        {
            var query = string.Format("INSERT INTO Strategy" +
                "(id, xmlConfig, type, activated, stock, buy, short, size ) VALUES " +
                "('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')",
                strategy.Id,
                strategy.XmlConfig,
                strategy.Type,
                strategy.Activated,
                strategy.Stock,
                strategy.Buy,
                strategy.Short,
                strategy.Size
                );
            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return -1;

            return 1;
        }

        public int InsertStrategy(List<Strategy> strategyList)
        {

            foreach (var strategy in strategyList)
            {
                var query = string.Format("INSERT INTO Strategy" +
                "(id, xmlConfig, type, activated, stock, buy, short, size) VALUES " +
                "('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')",
                strategy.Id,
                strategy.XmlConfig,
                strategy.Type,
                strategy.Activated,
                strategy.Stock,
                strategy.Buy,
                strategy.Short,
                strategy.Size
                );

                var dataTable = DALHelper.GetQueryResult(query);

                if (dataTable == null || dataTable.Rows.Count == 0)
                    return -1;

            }
            return strategyList.Count;
        }

        public int DeleteStrategy(Strategy strategy){

            var query = string.Format("DELETE FROM Strategy WHERE id = '{0}'", strategy.Id);

            var dataTable = DALHelper.GetQueryResult(query);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return -1;

            return 1;

        }

        public int DeleteStrategy(List<Strategy> strategyList)
        {

            foreach (var strategy in strategyList)
            {
                var query = string.Format("DELETE FROM Strategy WHERE id = '{0}'", strategy.Id);

                var dataTable = DALHelper.GetQueryResult(query);
                
                if (dataTable == null || dataTable.Rows.Count == 0)
                    return -1;

            }

            return 1;
        }

        public int UpdateStrategy(Strategy strategy) {

            var query = string.Format(String.Format("UPDATE Strategy SET " +
            "xmlConfig = '{1}', type = '{2}', activated = '{3}', stock = '{4}', buy = '{5}', short = '{6}', size = '{7}' WHERE id = '{0}' " 

                ,strategy.Id,
                strategy.XmlConfig,
                strategy.Type,
                strategy.Activated,
                strategy.Stock,
                strategy.Buy,
                strategy.Short,
                strategy.Size
                ), strategy.Id);
            var dataTable = DALHelper.GetNonQueryResult(query);

            if (dataTable == 0)
                return -1;

            return 1;
        }

    }
}
