using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number : S10242883
// Student Name : Ho Wei Lun
//
// Made by: Wei Lun, Javier
// Purpose: For bonus feature
//==========================================================
namespace Assignment
{
    class Staff
    {
        private string name;
        private int id;
        private string password;
        private string email;
        private DateTime datejoined;
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public DateTime Datejoined
        {
            get { return datejoined; }
            set { datejoined = value; }
        }
        public Staff() { }
        public Staff(string n, int i, string p, string e, DateTime d)
        {
            Name = n;
            Id = i;
            Password = p;
            Email = e;
            Datejoined = d;
        }


    }
}
