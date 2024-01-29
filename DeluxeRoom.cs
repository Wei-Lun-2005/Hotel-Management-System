using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number : S10242883
// Student Name : Ho Wei Lun 
//==========================================================

namespace Assignment
{
    class DeluxeRoom: Room
    {
        private bool additionalBed;
        public bool AdditionalBed
        {
            get { return additionalBed; }
            set { additionalBed = value; }
        }
        public DeluxeRoom() : base() { }
        public DeluxeRoom(int r, string b, double d, bool i) : base(r, b, d, i)
        {
            additionalBed = false;
        }
        public override double CalculateCharges()
        {
            double charges = DailyRate;
            if (AdditionalBed is true)
            {
                charges += 25;
            }
            return charges;
        }
        public override string ToString()
        {
            return base.ToString() + " Additional Bed: " + AdditionalBed;
        }
    }
}
