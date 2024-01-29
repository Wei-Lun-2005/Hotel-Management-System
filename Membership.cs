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
    class Membership
    {
        private string status;
        private int points;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public int Points
        {
            get { return points; }
            set { points = value; }
        }
        public Membership() { }
        public Membership(string status, int points)
        {
            Status = status;
            Points = points;
        }
        public void EarnPoints(double payment)
        {
            double total = payment / 10;
            Points += (int)total;
        }
        public bool RedeemPoints(int p)
        {
            if (p <= Points)
            {
                Points -= p;
                return true;
            }
            else
            {
                return false;
            }
        }
        public override string ToString()
        {
            return "Status: " + Status + " Points: " + Points;
        }
    }
}
