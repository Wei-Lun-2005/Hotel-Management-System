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
    abstract class Room : ICloneable
    {
        private int roomNumber;
        private string bedConfiguration;
        private double dailyRate;
        private bool isAvail;
        
        public int RoomNumber
        {
            get { return roomNumber; }
            set { roomNumber = value; }
        }
        public string BedConfiguration
        {
            get { return bedConfiguration; }
            set { bedConfiguration = value; }
        }
        public double DailyRate
        {
            get { return dailyRate; }
            set { dailyRate = value; }
        }
        public bool IsAvail
        {
            get { return isAvail; }
            set { isAvail = value; }
        }
        public Room() { }
        public Room(int r, string b, double d, bool i)
        {
            RoomNumber = r;
            BedConfiguration = b;
            DailyRate = d;
            IsAvail = i; 
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        abstract public double CalculateCharges();
        public override string ToString()
        {
            return "RoomNumber: " + RoomNumber + " Bed Configuration: "+BedConfiguration+" Daily Rate: " + DailyRate
                + " Available: " + IsAvail;
        }
    }
}
