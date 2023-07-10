using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBtest
{
    public class Employee
    {
        public int No { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsManager { get; set; }

        public Employee()
        {
        }

        public Employee(string name, string email, string password, bool isManager)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.IsManager = isManager;
        }
        public override String ToString()
        {
            return No + "," + Name + "," + Email + "," + IsManager + "," + Password;
        }
    }
}
