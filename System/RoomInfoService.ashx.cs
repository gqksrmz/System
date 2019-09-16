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
    /// RoomInfoService 的摘要说明
    /// </summary>
    public class RoomInfoService : IHttpHandler
    {
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
        /// 查询所有客房信息
        /// </summary>
        /// <param name="context"></param>
        public void SearchAllRoomInfo(HttpContext context)
        {
            string searchRoomName = context.Request["SearchRoomName"];
            string searchRoomType = context.Request["SearchRoomType"];
            if (searchRoomType == "-1")
            {
                searchRoomType = null;
            }
            int pageIndex = Convert.ToInt32(context.Request["pageIndex"]);
            int pageSize = Convert.ToInt32(context.Request["pageSize"]);//string searchRoomName,string searchRoomType
            List<RoomInfo> roomInfoList = roomInfoBLL.GetRoomInfoList(pageIndex, pageSize,searchRoomName,searchRoomType);
            Hashtable result = new Hashtable
            {
                ["data"] = roomInfoList,
                ["total"] = roomInfoBLL.GetRoomInfoCount()
            };
            String json = JSON.Encode(result);
            context.Response.Write(json);
        }
        /// <summary>
        /// 保存客房信息
        /// </summary>
        /// <param name="context"></param>
        public void SaveTableInfo(HttpContext context)
        {
            var data = JSON.Decode(context.Request["data"]);
            if (data is Hashtable)
            {
                Hashtable i = (Hashtable)data;
                RoomInfo roomInfo = new RoomInfo();
                string status = null;
                roomInfo.RoomId = (string)i["RoomId"];
                roomInfo.RoomName = (string)i["RoomName"];
                roomInfo.RoomRecommend = Convert.ToInt32(i["RoomRecommend"]);
                roomInfo.RoomType = Convert.ToInt32(i["RoomType"])-1;
                roomInfo.RoomCost = Convert.ToInt32(i["RoomCost"]);
                roomInfo.RoomCount = Convert.ToInt32(i["RoomCount"]);
                roomInfo.RoomSurplus = 0;
                status = (string)i["Status"];
                bool r = roomInfoBLL.SaveRoomInfo(roomInfo, status);
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
            else if (data is ArrayList)
            {
                ArrayList arryayList = (ArrayList)data;
                RoomInfo roomInfo = new RoomInfo();
                string status = null;
                foreach (var item in arryayList)
                {
                    Hashtable hs = (Hashtable)item;
                    roomInfo.RoomId = (string)hs["RoomId"];
                    roomInfo.RoomName = (string)hs["RoomName"];
                    roomInfo.RoomRecommend = Convert.ToInt32(hs["RoomRecommend"]);
                    roomInfo.RoomType = Convert.ToInt32(hs["RoomType"]);
                    roomInfo.RoomCost = Convert.ToInt32(hs["RoomCost"]);
                    roomInfo.RoomCount = Convert.ToInt32(hs["RoomCount"]);
                    roomInfo.RoomSurplus = Convert.ToInt32(hs["RoomSurplus"]);
                    status = (string)hs["Status"];
                }
                bool r = roomInfoBLL.SaveRoomInfo(roomInfo, status);
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

        }
        /// <summary>
        /// 根据roomId删除餐桌信息
        /// </summary>
        /// <param name="context"></param>
        public void RemoveRoomInfo(HttpContext context)
        {
            string roomId = context.Request["RoomId"];
            string[] roomIdList = roomId.Split(',');
            for (int i = 0; i < roomIdList.Length; i++)
            {
                bool r = roomInfoBLL.Delete(roomIdList[i]);
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
        /// 根据TableNo获取单个餐桌信息
        /// </summary>
        /// <param name="context"></param>
        public void GetRoomInfo(HttpContext context)
        {
            string roomId = context.Request["RoomId"];
            RoomInfo roomInfo = roomInfoBLL.GetEntity(roomId);
            Hashtable hs = new Hashtable();
            hs["RoomId"] = roomInfo.RoomId;
            hs["RoomName"] = roomInfo.RoomName;
            hs["RoomRecommend"] = roomInfo.RoomRecommend;
            hs["RoomType"] = roomInfo.RoomType;
            hs["RoomCost"] = roomInfo.RoomCost;
            hs["RoomCount"] = roomInfo.RoomCount;
            hs["RoomSurplus"] = roomInfo.RoomSurplus;
            String json = JSON.Encode(hs);
            context.Response.Write(json);
        }
        /// <summary>
        /// 展示编号
        /// </summary>
        /// <param name="context"></param>
        public void ShowNo(HttpContext context)
        {
            int type = Convert.ToInt32(context.Request["data"]);
            List<string> sList = roomInfoBLL.GetAllRoomId(type);
            List<int> newSList = new List<int>();
            string result = null;
            string r = null;
            foreach (var item in sList)
            {
                newSList.Add(Convert.ToInt32(item.Substring(2, 2)));
            }
            newSList.Sort();
            if (newSList.Count <= 0)
            {
                if (type == 0)
                {
                    r = "01";
                    result = "A1" + r;
                }else if (type == 1)
                {
                    r = "01";
                    result = "B2" + r;
                }
                else if (type == 2)
                {
                    r = "01";
                    result = "C3" + r;
                }
            }
            else
            {
                 r = (newSList.Last() + 1).ToString();
                if (sList.First().Substring(0, 1) == "A")
                {
                    result = "A1" + r;
                }
                else if (sList.First().Substring(0, 1) == "B")
                {
                    result = "B2" + r;
                }
                else if (sList.First().Substring(0, 1) == "B")
                {
                    result = "C3" + r;
                }
            }
            
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