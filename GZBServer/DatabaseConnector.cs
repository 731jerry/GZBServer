using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace GZBServer
{
    class DatabaseConnector
    {

        static MySqlConnection onlineSqlConnection;

        private static DatabaseConnector _onlineConnection = null;

        public static DatabaseConnector OnlineConnector()
        {
            if (_onlineConnection == null)
            {
                _onlineConnection = new DatabaseConnector();
            }

            if (onlineSqlConnection == null)
            {
                onlineSqlConnection = new MySqlConnection(@"server=121.42.154.95; user id=vivid; password=vivid; database=vivid;Charset=utf8");
                onlineSqlConnection.Open();
            }
            if (onlineSqlConnection.State == ConnectionState.Closed)
            {
                onlineSqlConnection.Open();
            }
            if (onlineSqlConnection.State == ConnectionState.Broken)
            {
                onlineSqlConnection.Close();
                onlineSqlConnection.Open();
            }
            return DatabaseConnector._onlineConnection;
        }

        // 修改数据
        public int OnlineUpdateData(String table, List<String> query, List<String> value, String condition)
        {
            int affectedRows = -1;
            String innerSQL = String.Join(",", query);
            if (!condition.Equals(""))
            {
                condition = " WHERE " + condition;
            }
            String SQLforGeneral = "UPDATE " + table + " SET " + innerSQL + " " + condition;
            MySqlCommand cmdInsert = new MySqlCommand(SQLforGeneral, onlineSqlConnection);
            cmdInsert.CommandTimeout = 0;
            affectedRows = cmdInsert.ExecuteNonQuery();
            onlineSqlConnection.Close();
            return affectedRows;
        }
    }
}
