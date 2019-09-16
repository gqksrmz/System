using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
namespace Common
{
    public static class SqlHelper
    {
        //获取配置文件中的连接字符串
        //Data Source=DESKTOP-V4D9RI7;Initial Catalog=BookManager;User ID=sa;Password=123456
        //ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private static readonly string constr = "Data Source=DESKTOP-V4D9RI7;Initial Catalog = RoomManager; User ID = sa; Password=123456";
        public static SqlConnection GetSqlConnection()
        {

            SqlConnection con = new SqlConnection(constr);
            return con;
        }
        /// <summary>
        /// 执行insert，delete，update方法
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="pms">sql语句中的参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, CommandType type, params SqlParameter[] pms)
        {
            using (SqlConnection con = GetSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = type;
                    //如果paramameter为空直接执行回报错
                    if (pms != null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    con.Open();
                    int r = cmd.ExecuteNonQuery();
                    return r;


                }
            }
        }
        /// <summary>
        /// 执行sql语句，返回单个值
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="pms">sql语句中的参数</param>
        /// <returns></returns>
        public static Object ExecuteScalar(string sql, CommandType type, params SqlParameter[] pms)
        {
            using (SqlConnection con = GetSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = type;
                    //如果paramameter为空直接执行回报错
                    if (pms != null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    con.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
        /// <summary>
        /// 执行语句返回一个DataReader
        /// 当返回DataReader的时候,Connection和DataReader不能关闭
        /// Command对象执行ExecuteReaderr方法时需要传递一个参数CommandBehavior.CloseConnection
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="pms">sql语句中的参数</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql, CommandType type, params SqlParameter[] pms)
        {
            SqlConnection con = GetSqlConnection();
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.CommandType = type;
                if (pms != null)
                {
                    cmd.Parameters.AddRange(pms);
                }
                con.Open();
                //当调用ExecuteReader方法时，如果传递一个CommandBehivior.CloseConnection参数，则表示将来用户关闭
                //raader的时候，系统会自动将Connection也关闭掉.
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }

        }
        /// <summary>
        /// 封装一个返回DataTable的方法
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="pms">sql语句中的参数</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql, CommandType type, params SqlParameter[] pms)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, constr);
            if (pms != null)
            {
                sqlDataAdapter.SelectCommand.Parameters.AddRange(pms);
            }
            DataTable dt = new DataTable();
            sqlDataAdapter.SelectCommand.CommandType = type;
            sqlDataAdapter.Fill(dt);
            return dt;
        }

    }

}





