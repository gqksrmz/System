using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ReserveInfoBLL
    {
        ReserveInfoDAL reserveInfoDAL = new ReserveInfoDAL();
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="entity">要插入的数据数据对象</param>
        /// <returns></returns>
        public bool Insert(ReserveInfo entity)
        {
            return reserveInfoDAL.Insert(entity);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">要更新的数据对象</param>
        /// <returns></returns>
        public bool Update(ReserveInfo entity)
        {
            return reserveInfoDAL.Update(entity);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="reserveNo">要删除的对象的编号</param>
        /// <returns></returns>
        public bool Delete(string reserveNo)
        {
            return reserveInfoDAL.Delete(reserveNo);
        }
        /// <summary>
        /// 根据tableNo获取单个实体对象
        /// </summary>
        /// <param name="tableNo"></param>
        /// <returns></returns>
        public ReserveInfo GetEntity(string reserveNo)
        {
            return reserveInfoDAL.GetEntity(reserveNo);
        }
        /// <summary>
        /// 分页获取预订信息列表
        /// </summary>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页几条数据</param>
        /// <returns></returns>
        public List<ReserveInfo> GetReserveInfoList(int pageIndex, int pageSize, string sortField, string sortOrder)
        {
            return reserveInfoDAL.GetReserveInfoList(pageIndex, pageSize,sortField,sortOrder);
        }
        /// <summary>
        /// 获取预订信息表总共多少条数据
        /// </summary>
        /// <returns></returns>
        public int GetReserveInfoCount()
        {
            return reserveInfoDAL.GetReserveInfoCount();
        }
        /// <summary>
        /// 更改后的信息保存
        /// </summary>
        /// <param name="obj">要保存的对象</param>
        /// <param name="status">是增加还是修改还是删除</param>
        /// <returns></returns>
        public bool SaveReserveInfo(Object obj, string status)
        {
            ReserveInfo reserveInfo = (ReserveInfo)obj;
            try
            {
                if (status == "added")
                {
                    reserveInfoDAL.Insert(reserveInfo);
                }
                else if (status == "edit")
                {
                    reserveInfoDAL.Update(reserveInfo);
                }
                else
                {
                    reserveInfoDAL.Delete(reserveInfo.ReserveNo);
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 查询所有预订编号
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllReserveNo()
        {
            List<string> sList = reserveInfoDAL.GetAllReserveNo();
            return sList;
        }
    }
}
