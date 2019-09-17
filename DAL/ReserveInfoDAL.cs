using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ReserveInfoDAL
    {
        string selectSql = "select * from ReserveInfo";
        /// <summary>
        /// 插入新数据
        /// </summary>
        /// <param name="entity">要插入的数据对象</param>
        /// <returns></returns>
        public bool Insert(ReserveInfo entity)
        {
            string sql = "insert into ReserveInfo(reserveno,reservename,reserveperson,reservephone,reservenum,starttime,endtime,reservecost) " +
                "values(@reserveno,@reservename,@reserveperson,@reservephone,@reservenum,@starttime,@endtime,@reservecost)";
            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@reserveno",entity.ReserveNo),
                new SqlParameter("@reservename",entity.ReserveName),
                new SqlParameter("@reserveperson",entity.ReservePerson),
                new SqlParameter("@reservephone",entity.ReservePhone),
                new SqlParameter("@reservenum",entity.ReserveNum),
                new SqlParameter("@starttime",entity.StartTime),
                new SqlParameter("@endtime",entity.EndTime),
                new SqlParameter("@reservecost",entity.ReserveCost)
            };
            int r = SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
            string sql2 = @"update RoomInfo set roomsurplus=roomsurplus-1 where roomname=@roomname ";
            SqlParameter pms2 = new SqlParameter("@roomname", entity.ReserveName);
            int r2 = SqlHelper.ExecuteNonQuery(sql2, CommandType.Text, pms2);
            if (r > 0&&r2>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity">要更新的数据对象</param>
        /// <returns></returns>
        public bool Update(ReserveInfo entity)
        {
            string sql = @"update RoomInfo set reserveno=@reserveno,reservename=@reservename,reserveperson=@reserveperson,reservephone=@reservephone,reservenum=@reservenum,starttime=@starttime,endtime=@endtime,reservecost=@reservecost where reserveno=@reserveno";
            SqlParameter[] pms = new SqlParameter[]
           {
                new SqlParameter("@reserveno",entity.ReserveNo),
                new SqlParameter("@reservename",entity.ReserveName),
                new SqlParameter("@reserveperson",entity.ReservePerson),
                new SqlParameter("@reservephone",entity.ReservePhone),
                new SqlParameter("@reservenum",entity.ReserveNum),
                new SqlParameter("@starttime",entity.StartTime),
                new SqlParameter("@endtime",entity.EndTime),
                new SqlParameter("@reservecost",entity.ReserveCost)
           };
            int r = SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
            if (r > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="reserveNo">要删除的对象的编号</param>
        /// <returns></returns>
        public bool Delete(string reserveNo)
        {
            string sql1 = "delete from ReserveInfo" +
                " where reserveno = @reserveno";
            SqlParameter pms1 = new SqlParameter("@reserveno", reserveNo);
            int r = SqlHelper.ExecuteNonQuery(sql1, CommandType.Text, pms1);
            if (r > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 查询单个实体对象
        /// </summary>
        /// <param name="roomId">要查询的对象的编号</param>
        /// <returns></returns>
        public ReserveInfo GetEntity(string reserveNo)
        {
            string sql = selectSql + " where reserveno=@reserveno";
            SqlParameter pms = new SqlParameter("@reserveno", reserveNo);
            SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.Text, pms);
            ReserveInfo reserveInfo = new ReserveInfo();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    reserveInfo.ReserveNo = reader.GetString(0);
                    reserveInfo.ReserveName = reader.GetString(1);
                    reserveInfo.ReservePerson = reader.GetString(2);
                    reserveInfo. ReservePhone= reader.GetString(3);
                    reserveInfo.ReserveNum = reader.GetInt32(4);
                    reserveInfo.StartTime = reader.GetDateTime(5);
                    reserveInfo.EndTime = reader.GetDateTime(6);
                    reserveInfo.ReserveCost = reader.GetInt32(7);
                }
            }
            reader.Close();
            return reserveInfo;
        }
        /// <summary>
        /// 查询分页的预订信息列表
        /// </summary>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页多少条</param>
        /// <returns></returns>
        public List<ReserveInfo> GetReserveInfoList(int pageIndex, int pageSize, string sortField, string sortOrder)
        {
            string sql = null;
            if (string.IsNullOrEmpty(sortField) && string.IsNullOrEmpty(sortOrder))
            {
                sql = selectSql + " order by reserveno offset(@pageIndex)*@pageSize rows fetch next @pageSize rows only";
            }else if(!string.IsNullOrEmpty(sortField) && string.IsNullOrEmpty(sortOrder))
            {
                sql = selectSql + " order by "+sortField+" offset(@pageIndex)*@pageSize rows fetch next @pageSize rows only";

            }
            else if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
            {
                sql = selectSql + " order by " + sortField + " " + sortOrder + " offset(@pageIndex)*@pageSize rows fetch next @pageSize rows only";

            }



            List<ReserveInfo> reserveInfoList = new List<ReserveInfo>();
            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@pageIndex", pageIndex),
                new SqlParameter("@pageSize", pageSize)
            };
            SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.Text, pms);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ReserveInfo reserveInfo = new ReserveInfo();
                    reserveInfo.ReserveNo = reader.GetString(0);
                    reserveInfo.ReserveName = reader.GetString(1);
                    reserveInfo.ReservePerson = reader.GetString(2);
                    reserveInfo.ReservePhone = reader.GetString(3);
                    reserveInfo.ReserveNum = reader.GetInt32(4);
                    reserveInfo.StartTime = reader.GetDateTime(5);
                    reserveInfo.EndTime = reader.GetDateTime(6);
                    reserveInfo.ReserveCost = reader.GetInt32(7);
                    reserveInfoList.Add(reserveInfo);
                }
            }
            reader.Close();
            return reserveInfoList;
        }
        /// <summary>
        /// 查询预约信息表一共多少条数据
        /// </summary>
        /// <returns></returns>
        public int GetReserveInfoCount()
        {
            string sql = @"select count(*) from ReserveInfo";
            int r = (int)SqlHelper.ExecuteScalar(sql, CommandType.Text);
            return r;
        }
        /// <summary>
        /// 查询所有预订编号
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllReserveNo()
        {
            string sql = @"select reserveno from ReserveInfo ";
            List<string> sList = new List<string>();
            SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.Text);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    sList.Add(reader.GetString(0));
                }
            }
            return sList;
        }
    }
}
