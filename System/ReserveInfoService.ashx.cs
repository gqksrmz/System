using BLL;
using Common;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace System
{
    /// <summary>
    /// ReserveInfoService 的摘要说明
    /// </summary>
    public class ReserveInfoService : IHttpHandler
    {
        ReserveInfoBLL reserveInfoBLL = new ReserveInfoBLL();
        RoomInfoBLL roomInfoBLL = new RoomInfoBLL();
        /// <summary>
        /// 反射
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            String fn = context.Request["action"];
            MethodInfo method = this.GetType().GetMethod(fn);
            if (method != null)
            {
                method.Invoke(this, new object[] { context });
            }
        }
        /// <summary>
        /// 查询所有预订信息
        /// </summary>
        /// <param name="context"></param>
        public void SearchAllReserveInfo(HttpContext context)
        {
            string sortField = context.Request["sortField"];
            string sortOrder = context.Request["sortOrder"];
            int pageIndex = Convert.ToInt32(context.Request["pageIndex"]);
            int pageSize = Convert.ToInt32(context.Request["pageSize"]);//string sortField,string sortOrder
            List<ReserveInfo> roomInfoList = reserveInfoBLL.GetReserveInfoList(pageIndex, pageSize, sortField, sortOrder);
            Hashtable result = new Hashtable
            {
                ["data"] = roomInfoList,
                ["total"] = reserveInfoBLL.GetReserveInfoCount()
            };
            String json = JSON.Encode(result);
            context.Response.Write(json);
        }
        /// <summary>
        /// 保存预订信息
        /// </summary>
        /// <param name="context"></param>
        public void SaveReserveInfo(HttpContext context)
        {
            var data = JSON.Decode(context.Request["data"]);
            Hashtable i = (Hashtable)data;
            ReserveInfo reserveInfo = new ReserveInfo();
            string status = null;
            reserveInfo.ReserveNo = (string)i["ReserveNo"];
            reserveInfo.ReserveName = (string)i["ReserveName"];
            reserveInfo.ReservePerson = (string)i["ReservePerson"];
            reserveInfo.ReservePhone = (string)i["ReservePhone"];
            //reserveInfo.ReserveNum = Convert.ToInt32(i["RoomCost"]);.
            //reserveInfo.ReserveCost = Convert.ToInt32(i["RoomSurplus"]);
            reserveInfo.StartTime = (DateTime)i["StartTime"];
            reserveInfo.EndTime = (DateTime)i["EndTime"];
            TimeSpan sp = reserveInfo.EndTime.Subtract(reserveInfo.StartTime);
            reserveInfo.ReserveNum = sp.Days;
            reserveInfo.ReserveCost = roomInfoBLL.GetReserveCostByReserveName(reserveInfo.ReserveName) * reserveInfo.ReserveNum;
            status = (string)i["Status"];
            bool r = reserveInfoBLL.SaveReserveInfo(reserveInfo, status);
            if (r)
            {
                String json = JSON.Encode("保存成功！");
                context.Response.Write(json);
            }
            else
            {
                String json = JSON.Encode("保存失败！");
                context.Response.Write(json);
            }
        }
        /// <summary>
        /// 根据roomId删除餐桌信息
        /// </summary>
        /// <param name="context"></param>
        public void RemoveReserveInfo(HttpContext context)
        {
            string reserveNo = context.Request["ReserveNo"];
            string[] reserveNoList = reserveNo.Split(',');
            for (int i = 0; i < reserveNoList.Length; i++)
            {
                bool r = reserveInfoBLL.Delete(reserveNoList[i]);
                if (r)
                {
                    context.Response.Write("成功!");
                }
                else
                {
                    context.Response.Write("失败！");
                }
            }
        }
        /// <summary>
        /// 获取剩余数量大于0的客房,展示名称
        /// </summary>
        /// <param name="context"></param>
        public void ShowRoomName(HttpContext context)
        {
            List<RoomInfo> roomInfoList = roomInfoBLL.GetRoomInfoSurplusList();
            Hashtable result = new Hashtable
            {
                ["data"] = roomInfoList,
                ["total"] = roomInfoBLL.GetRoomInfoCount()
            };
            String json = JSON.Encode(result);
            context.Response.Write(json);
        }
        /// <summary>
        ///显示预约编号 
        /// </summary>
        /// <param name="context"></param>
        public void ShowReserveNo(HttpContext context)
        {
            List<string> sList = reserveInfoBLL.GetAllReserveNo();
            List<int> newSList = new List<int>();
            foreach (var item in sList)
            {
                newSList.Add(Convert.ToInt32(item.Substring(6, 3)));
            }
            newSList.Sort();
            string r = (newSList.Last() + 1).ToString("D3");
            string result = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("D2")+r;
            String json = JSON.Encode(result);
            context.Response.Write(json);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}