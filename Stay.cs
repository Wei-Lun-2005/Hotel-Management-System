using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number : S10242883
// Student Name : Ho Wei Lun
//==========================================================

namespace Assignment
{
    class Stay
    {
        private DateTime checkinDate;
        private DateTime checkoutDate;
        private List<Room> roomList;

        public DateTime CheckinDate 
        {
            get { return checkinDate; }
            set { checkinDate = value; }
        }
        public DateTime CheckoutDate
        {
            get { return checkoutDate; }
            set { checkoutDate = value; }
        }
        public List<Room> RoomList
        {
            get { return roomList; }
            set { roomList = value; }
        }
        public Stay() { }
        public Stay(DateTime checkinDate, DateTime checkoutDate)
        {
            CheckinDate = checkinDate;
            CheckoutDate = checkoutDate;
            RoomList = new List<Room>();
        }
        public void AddRoom(Room room)
        {
            RoomList.Add(room);
        }
        public double CalculateTotal()
        {
            double Total = 0;
            int Daystay = checkoutDate.Subtract(checkinDate).Days;
            foreach(Room i in RoomList)
            {
                Total += Daystay * i.CalculateCharges();
            }
            return Total;
        }
        public override string ToString()
        {
            return "CheckinDate: " + CheckinDate + " CheckoutDate: " + CheckoutDate;
        }
        
    }
}
