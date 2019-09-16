using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ReserveInfo
    {
        private string _reserveNo;
        private string _reserveName;
        private string _reservePerson;
        private string _reservePhone;
        private int _reserveNum;
        private DateTime _startTime;
        private DateTime _endTime;
        private int _reserveCost;
        public ReserveInfo()
        {

        }
        public ReserveInfo(string reserveNo, string reserveName, string reservePerson, string reservePhone, int reserveNum,DateTime startTime,
            DateTime endTime, int reserveCost)
        {
            this._reserveNo = reserveNo;
            this._reserveName = reserveName;
            this._reservePerson = reservePerson;
            this._reservePhone = reservePhone;
            this._reserveNum = reserveNum;
            this._startTime = startTime;
            this._endTime = endTime;
            this._reserveCost = reserveCost;
        }
        public string ReserveNo
        {
            get
            {
                return _reserveNo;
            }

            set
            {
                _reserveNo = value;
            }
        }

        public string ReserveName
        {
            get
            {
                return _reserveName;
            }

            set
            {
                _reserveName = value;
            }
        }

        public string ReservePerson
        {
            get
            {
                return _reservePerson;
            }

            set
            {
                _reservePerson = value;
            }
        }

        public string ReservePhone
        {
            get
            {
                return _reservePhone;
            }

            set
            {
                _reservePhone = value;
            }
        }

        public int ReserveNum
        {
            get
            {
                return _reserveNum;
            }

            set
            {
                _reserveNum = value;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return _startTime;
            }

            set
            {
                _startTime = value;
            }
        }

        public DateTime EndTime
        {
            get
            {
                return _endTime;
            }

            set
            {
                _endTime = value;
            }
        }

        public int ReserveCost
        {
            get
            {
                return _reserveCost;
            }

            set
            {
                _reserveCost = value;
            }
        }
    }
}
