using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBtest
{
    public class Employee
    {
        public String No { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public Boolean IsManager { get; set; }

        public Employee()
        {
        }

        public Employee(string no, string name, string email, string password, bool isManager)
        {
            this.No = no;
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
