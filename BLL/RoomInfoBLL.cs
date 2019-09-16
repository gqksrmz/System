using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RoomInfoBLL
    {
        RoomInfoDAL roomInfoDAL = new RoomInfoDAL();
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="entity">要插入的数据数据对象</param>
        /// <returns></returns>
        public bool Insert(RoomInfo entity)
        {
            return roomInfoDAL.Insert(entity);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">要更新的数据对象</param>
        /// <returns></returns>
        public bool Update(RoomInfo entity)
        {
            return roomInfoDAL.Update(entity);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="roomId">要删除的对象的编号</param>
        /// <returns></returns>
        public bool Delete(string roomId)
        {
            return roomInfoDAL.Delete(roomId);
        }
        /// <summary>
        /// 根据tableNo获取单个实体对象
        /// </summary>
        /// <param name="tableNo"></param>
        /// <returns></returns>
        public RoomInfo GetEntity(string roomId)
        {
            return roomInfoDAL.GetEntity(roomId);
        }
        /// <summary>
        /// 分页获取客房信息列表
        /// </summary>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页几条数据</param>
        /// <returns></returns>
        public List<RoomInfo> GetRoomInfoList(int pageIndex, int pageSize, string searchRoomName, string searchRoomType)
        {
            return roomInfoDAL.GetRoomInfoList(pageIndex, pageSize,searchRoomName,searchRoomType);
        }
        /// <summary>
        /// 获取客房信息表总共多少条数据
        /// </summary>
        /// <returns></returns>
        public int GetRoomInfoCount()
        {
            return roomInfoDAL.GetRoomInfoCount();
        }
        /// <summary>
        /// 更改后的信息保存
        /// </summary>
        /// <param name="obj">要保存的对象</param>
        /// <param name="status">是增加还是修改还是删除</param>
        /// <returns></returns>
        public bool SaveRoomInfo(Object obj, string status)
        {
            RoomInfo roomInfo = (RoomInfo)obj;
            try
            {
                if (status == "added")
                {
                    roomInfoDAL.Insert(roomInfo);
                }
                else if (status == "edit")
                {
                    roomInfoDAL.Update(roomInfo);
                }
                else
                {
                    roomInfoDAL.Delete(roomInfo.RoomId);
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 根据预订编号查询每天多少钱
        /// </summary>
        /// <param name="reserveNo">预订编号</param>
        /// <returns></returns>
        public int GetReserveCostByReserveName(string reserveName)
        {
            int r = roomInfoDAL.GetReserveCostByReserveName(reserveName);
            return r;
        }
        /// <summary>
        /// 根据类型获取编号
        /// </summary>
        /// <param name="type">客房类型</param>
        /// <returns></returns>
        public List<string> GetAllRoomId(int type)
        {
            List<string> sList = roomInfoDAL.GetAllRoomId(type);
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

            List<RoomInfo> roomInfoList = roomInfoDAL.GetRoomInfoSurplusList();
            return roomInfoList;
        }

    }
}
