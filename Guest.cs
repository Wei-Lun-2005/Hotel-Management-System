using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number : S10242883
// Student Name : Ho Wei Lun 
//==========================================================

namespace Assignment
{
    class Guest
    {
        private string name;
        private string passportNum;
        private Stay hotelStay;
        private Membership member;
        private bool isCheckedin;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string PassportNum
        {
            get { return passportNum; }
            set { passportNum = value; }
        }
        public Stay HotelStay
        {
            get { return hotelStay; }
            set { hotelStay = value; }
        }
        public Membership Member
        {
            get { return member; }
            set { member = value; }
        }
        public bool IsCheckedin
        {
            get { return isCheckedin; }
            set { isCheckedin = value; }
        }
        public Guest() { }
        public Guest(string name, string passportNum, Stay hotelStay, Membership member, bool isCheckedin)
        {
            Name = name;
            PassportNum = passportNum;
            HotelStay = hotelStay;
            Member = member;
            IsCheckedin = isCheckedin;
        }
        public override string ToString()
        {
            return "Name: " + Name + " PassportNum: " + PassportNum + " " + HotelStay.ToString() + " " + Member.ToString() + " IsCheckedin: " + IsCheckedin;
        }
    }
}
