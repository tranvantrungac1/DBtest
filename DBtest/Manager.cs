using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace DBtest
{
    public class Manager
    {
        public String name;
        public Manager() { }
        public Manager(string name)
        {
            this.name = name;
        }
        virtual public void AddNew()
        {
            Console.WriteLine("");
        }
        virtual public void Remove()
        {
            Console.WriteLine("");
        }
        virtual public void Update()
        {
            Console.WriteLine("");
        }
        virtual public void Find()
        {
            Console.WriteLine("");
        }
        virtual public void Import()
        {
            Console.WriteLine("");
        }
        virtual public void Export()
        {
            Console.WriteLine("");
        }
        virtual public Employee Login()
        {
            return new Employee();
        }
    }
}
