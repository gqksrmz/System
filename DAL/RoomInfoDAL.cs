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
    public class RoomInfoDAL
    {
        string selectSql = "select * from RoomInfo";
        /// <summary>
        /// 插入新数据
        /// </summary>
        /// <param name="entity">要插入的数据对象</param>
        /// <returns></returns>
        public bool Insert(RoomInfo entity)
        {
            string sql = "insert into RoomInfo(roomid,roomname,roomrecommend,roomtype,roomcost,roomcount,roomsurplus) " +
                "values(@roomid,@roomname,@roomrecommend,@roomtype,@roomcost,@roomcount,@roomsurplus)";
            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@roomid",entity.RoomId),
                new SqlParameter("@roomname",entity.RoomName),
                new SqlParameter("@roomrecommend",entity.RoomRecommend),
                new SqlParameter("@roomtype",entity.RoomType),
                new SqlParameter("@roomcost",entity.RoomCost),
                new SqlParameter("@roomcount",entity.RoomCount),
                new SqlParameter("@roomsurplus",entity.RoomSurplus)
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
        /// 更新数据
        /// </summary>
        /// <param name="entity">要更新的数据对象</param>
        /// <returns></returns>
        public bool Update(RoomInfo entity)
        {
            string sql = @"update RoomInfo set roomid=@roomid,roomname=@roomname,roomrecommend=@roomrecommend,roomtype=@roomtype,roomcost=@roomcost,roomcount=@roomcount,roomsurplus=@roomsurplus where roomid=@roomid";
            SqlParameter[] pms = new SqlParameter[]
           {
                new SqlParameter("@roomid",entity.RoomId),
                new SqlParameter("@roomname",entity.RoomName),
                new SqlParameter("@roomrecommend",entity.RoomRecommend),
                new SqlParameter("@roomtype",entity.RoomType),
                new SqlParameter("@roomcost",entity.RoomCost),
                new SqlParameter("@roomcount",entity.RoomCount),
                new SqlParameter("@roomsurplus",entity.RoomSurplus)
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
        /// <param name="roomId">要删除的对象的编号</param>
        /// <returns></returns>
        public bool Delete(string roomId)
        {
            string sql1 = "delete from RoomInfo" +
                " where roomid = @roomid";
            SqlParameter pms1 = new SqlParameter("@roomid", roomId);
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
        public RoomInfo GetEntity(string roomId)
        {
            string sql = selectSql + " where roomid=@roomid";
            SqlParameter pms = new SqlParameter("@roomid", roomId);
            SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.Text, pms);
            RoomInfo roomInfo = new RoomInfo();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    roomInfo.RoomId = reader.GetString(0);
                    roomInfo.RoomName = reader.GetString(1);
                    roomInfo.RoomRecommend = reader.GetInt32(2);
                    roomInfo.RoomType = reader.GetInt32(3);
                    roomInfo.RoomCost = reader.GetInt32(4);
                    roomInfo.RoomCount = reader.GetInt32(5);
                    roomInfo.RoomSurplus = reader.GetInt32(6);
                }
            }
            reader.Close();
            return roomInfo;
        }
        /// <summary>
        /// 查询分页的客房信息列表
        /// </summary>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页多少条</param>
        /// <returns></returns>
        public List<RoomInfo> GetRoomInfoList(int pageIndex, int pageSize, string searchRoomName, string searchRoomType)
        {
            string sql = null;
            if (string.IsNullOrEmpty(searchRoomName) && string.IsNullOrEmpty(searchRoomType))
            {
                sql = selectSql + " order by roomid offset(@pageIndex)*@pageSize rows fetch next @pageSize rows only";
            }
            else if (!string.IsNullOrEmpty(searchRoomName) && string.IsNullOrEmpty(searchRoomType))
            {
                sql = selectSql + " where roomname like '%" + searchRoomName + "%' order by roomid offset(@pageIndex)*@pageSize rows fetch next @pageSize rows only";
            }
            else if (!string.IsNullOrEmpty(searchRoomName) && !string.IsNullOrEmpty(searchRoomType))
            {
                sql = selectSql + " where roomname like '%" + searchRoomName + "%' and roomtype =" + searchRoomType + " order by roomid offset(@pageIndex)*@pageSize rows fetch next @pageSize rows only";
            }
            else if (string.IsNullOrEmpty(searchRoomName) && !string.IsNullOrEmpty(searchRoomType))
            {
                sql = selectSql + " where  roomtype =" + searchRoomType + " order by roomid offset(@pageIndex)*@pageSize rows fetch next @pageSize rows only";
            }
            List<RoomInfo> roomInfoList = new List<RoomInfo>();
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
                    RoomInfo roomInfo = new RoomInfo();
                    roomInfo.RoomId = reader.GetString(0);
                    roomInfo.RoomName = reader.GetString(1);
                    roomInfo.RoomRecommend = reader.GetInt32(2);
                    roomInfo.RoomType = reader.GetInt32(3);
                    roomInfo.RoomCost = reader.GetInt32(4);
                    roomInfo.RoomCount = reader.GetInt32(5);
                    roomInfo.RoomSurplus = reader.GetInt32(6);
                    roomInfoList.Add(roomInfo);
                }
            }
            reader.Close();
            return roomInfoList;
        }
        /// <summary>
        /// 查询客房信息表一共多少条数据
        /// </summary>
        /// <returns></returns>
        public int GetRoomInfoCount()
        {
            string sql = @"select count(*) from RoomInfo";
            int r = (int)SqlHelper.ExecuteScalar(sql, CommandType.Text);
            return r;
        }
        /// <summary>
        /// 根据预订编号查询每天多少钱
        /// </summary>
        /// <param name="reserveNo">预订编号</param>
        /// <returns></returns>
        public int GetReserveCostByReserveName(string reserveName)
        {
            string sql = @"select roomcost from RoomInfo where roomname=@reservename";
            SqlParameter pms = new SqlParameter("@reservename", reserveName);
            int r = (int)SqlHelper.ExecuteScalar(sql, CommandType.Text, pms);
            return r;
        }
        /// <summary>
        /// 根据类型获取编号
        /// </summary>
        /// <param name="type">客房类型</param>
        /// <returns></returns>
        public List<string> GetAllRoomId(int type)
        {
            List<string> sList = new List<string>();
            if (type == 0)
            {
                string sql = @"select roomid from RoomInfo where  roomid like 'A%'";
                SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.Text);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sList.Add(reader.GetString(0));
                    }
                }
            }
            else if (type == 1)
            {
                string sql = @"select roomid from RoomInfo where  roomid like 'B%'";
                SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.Text);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sList.Add(reader.GetString(0));
                    }
                }
            }
            else if (type == 1)
            {
                string sql = @"select roomid from RoomInfo where  roomid like 'C%'";
                SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.Text);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sList.Add(reader.GetString(0));
                    }
                }
            }
            else if (type == 1)
            {
                string sql = @"select roomid from RoomInfo where  roomid like 'B%'";
                SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.Text);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sList.Add(reader.GetString(0));
                    }
                }

            }
            return sList;
        }
        /// <summary>
        /// 获取剩余数量大于0的客房
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<RoomInfo> GetRoomInfoSurplusList()
        {

            string sql = selectSql + " where roomsurplus>0 order by roomid ";
            List<RoomInfo> roomInfoList = new List<RoomInfo>();
            SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.Text);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    RoomInfo roomInfo = new RoomInfo();
                    roomInfo.RoomId = reader.GetString(0);
                    roomInfo.RoomName = reader.GetString(1);
                    roomInfo.RoomRecommend = reader.GetInt32(2);
                    roomInfo.RoomType = reader.GetInt32(3);
                    roomInfo.RoomCost = reader.GetInt32(4);
                    roomInfo.RoomCount = reader.GetInt32(5);
                    roomInfo.RoomSurplus = reader.GetInt32(6);
                    roomInfoList.Add(roomInfo);
                }
            }
            reader.Close();
            return roomInfoList;
        }

    }
}
