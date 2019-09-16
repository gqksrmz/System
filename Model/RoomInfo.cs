using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RoomInfo
    {
        private string _roomId;
        private string _roomName;
        private int _roomRecommend;
        private int _roomType;
        private int _roomCost;
        private int _roomCount;
        private int _roomSurplus;
        private string _roomremark;
        public RoomInfo()
        {

        }
        public RoomInfo(string roomId, string roomName, int roomRecommend, int roomType, int roomCost, int roomCount, int roomSurplus, string roomremark)
        {
            this.RoomId = roomId;
            this.RoomName = roomName;
            this.RoomRecommend = roomRecommend;
            this.RoomType = roomType;
            this.RoomCost = roomCost;
            this.RoomCount = roomCount;
            this.RoomSurplus = roomSurplus;
            this.Roomremark = roomremark;
        }

        public string RoomId
        {
            get
            {
                return _roomId;
            }

            set
            {
                _roomId = value;
            }
        }

        public string RoomName
        {
            get
            {
                return _roomName;
            }

            set
            {
                _roomName = value;
            }
        }

        public int RoomRecommend
        {
            get
            {
                return _roomRecommend;
            }

            set
            {
                _roomRecommend = value;
            }
        }

        public int RoomType
        {
            get
            {
                return _roomType;
            }

            set
            {
                _roomType = value;
            }
        }

        public int RoomCost
        {
            get
            {
                return _roomCost;
            }

            set
            {
                _roomCost = value;
            }
        }

        public int RoomCount
        {
            get
            {
                return _roomCount;
            }

            set
            {
                _roomCount = value;
            }
        }

        public int RoomSurplus
        {
            get
            {
                return _roomSurplus;
            }

            set
            {
                _roomSurplus = value;
            }
        }

        public string Roomremark
        {
            get
            {
                return _roomremark;
            }

            set
            {
                _roomremark = value;
            }
        }
    }
}
