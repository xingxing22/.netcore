using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public static class DBHelper
    {
        private static string _connection = ConfigurationManager.ConnectionStrings["MySQLlocal"].ToString();

        /// <summary>
        /// 从mysql数据库中获取数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parames"></param>
        /// <returns></returns>
        public static DataTable GetDataBySQL(string sql, MySqlParameter[] parames = null)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                if (parames != null && parames.Length > 0)
                {
                    foreach (var param in parames)
                    {
                        cmd.Parameters.Add(param);
                    }
                }
                MySqlDataAdapter mySqlData = new MySqlDataAdapter(cmd);
                mySqlData.Fill(dt);
                mySqlData.Dispose();
            }
            return dt;
        }
    }
}
