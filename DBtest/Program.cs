using System.Data.SqlClient;
namespace DBtest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EmployeeManager manager = new EmployeeManager();
            int selected = 0;
            if(manager.Login().IsManager)
            {
                do
                {
                    Console.WriteLine("***EMPLOYEE MANAGER***");
                    Console.WriteLine("\t1.Search Employee by Name or EmpNo");
                    Console.WriteLine("\t2.Add New Employee");
                    Console.WriteLine("\t3.Update Employee");
                    Console.WriteLine("\t4.Remove Employee");
                    Console.WriteLine("\t5.Export Data");
                    Console.WriteLine("\t6.Import Data");
                    Console.WriteLine("\t7.Exit");
                    Console.Write("Select (1-7): ");
                    selected = Convert.ToInt16(Console.ReadLine());


                    switch (selected)
                    {
                        case 1:
                            manager.Find();
                            break;
                        case 2:
                            manager.AddNew();
                            break;
                        case 3:
                            manager.Update();
                            break;
                        case 4:
                            manager.Remove();
                            break;
                        case 5:
                            manager.Export();
                            break;
                        case 6:
                            manager.Import();
                            break;
                        case 7:
                            Console.WriteLine("------------END----------");
                            break;
                        default:
                            Console.WriteLine("Invalid");
                            break;
                    }
                } while (selected != 7);
                Console.WriteLine("----END----");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("***EMPLOYEE SEARCHING***");
                manager.Find();
                Console.WriteLine("----END----");
                Console.ReadLine();
            }
        }
    }
}