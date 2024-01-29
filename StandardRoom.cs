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
    class StandardRoom : Room
    {
        private bool requireWifi;
        private bool requireBreakfast;
        public bool RequireWifi
        {
            get { return requireWifi; }
            set { requireWifi = value; }
        }
        public bool RequireBreakfast
        {
            get { return requireBreakfast; }
            set { requireBreakfast = value; }
        }
        public StandardRoom() : base() { }
        public StandardRoom(int r, string b, double d, bool i) : base(r, b, d, i)
        {
            RequireWifi = false;
            RequireBreakfast = false;
        }
        public override double CalculateCharges()
        {
            double charges = DailyRate;
            if (RequireWifi is true)
            {
                charges += 10;
            }
            if (RequireBreakfast is true)
            {
                charges += 20;
            }
            return charges;
        }
        public override string ToString()
        {
            return base.ToString() + " Require Wifi: " + RequireWifi + " Require Breakfast: " +  RequireBreakfast;
        }
    }
}
