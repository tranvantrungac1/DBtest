using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace DBtest
{
    public class EmployeeManager : Manager
    {
        List<Employee> emps;


        public EmployeeManager()
        {
            emps = new List<Employee>();
        }
        //public void LoadData()
        //{
        //    try
        //    {
        //        SqlConnection conn = new DbConnector().GetConnection();
        //        conn.Open();
        //        string sql = "SELECT * FROM EMPLOYEES";
        //        SqlCommand cmd = new SqlCommand(sql,conn);
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            Employee emp = new Employee();
        //            emp.No = (string) reader[0];
        //            emp.Name = (string) reader[1];
        //            emp.Email = (string) reader[2];
        //            emp.Password = (string) reader[3];
        //            emp.IsManager = (bool) reader[4];
        //            emps.Add(emp);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        public override Employee Login()
        {
            SqlConnection conn = new DbConnector().GetConnection();
            conn.Open();
            while (true)
            {
                Console.Write("Nhap Name: ");
                String name = Console.ReadLine();
                Console.Write("Nhap Password: ");
                String password = Console.ReadLine();
                string sql = "SELECT * FROM dbo.employees WHERE name=@name AND password = @password";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@password", password);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Console.WriteLine("Dang nhap thanh cong");
                    Employee emp = new Employee((string)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (bool)reader[4]);
                    conn.Close();
                    return emp;
                }
                Console.WriteLine("Name hoac Password khong dung");
                reader.Close();
            }
        }
        public override void Find()
        {
            String searchInfo;
            do
            {
                Console.Write("Nhap User hoac Id: ");
                searchInfo = Console.ReadLine();
            } while (string.IsNullOrEmpty(searchInfo));

            SqlConnection conn = new DbConnector().GetConnection();
            conn.Open();
            string sql = "SELECT * FROM dbo.employees WHERE no=@no OR name = @name";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", searchInfo);
            cmd.Parameters.AddWithValue("@no", searchInfo);
            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.HasRows)
            {
                Console.WriteLine("Tim thay danh sach sau: ");
                while (reader.Read())
                {
                    Employee emp = new Employee();
                    emp.No = (string)reader[0];
                    emp.Name = (string)reader[1];
                    emp.Email = (string)reader[2];
                    emp.Password = (string)reader[3];
                    emp.IsManager = (bool)reader[4];
                    Console.WriteLine(emp);
                }
                reader.Close();
            }
            else
            {
                Console.WriteLine("Khong tim thay");
            }
            conn.Close();
        }
        public override void AddNew()
        {
            Console.Write("Nhap Number: ");
            String no = Console.ReadLine();
            Console.Write("Nhap Ten: ");
            String name = Console.ReadLine();
            Console.Write("Nhap Email: ");
            String email = Console.ReadLine();
            Console.Write("La quan ly y/n:");
            Boolean isManager = false;
            if (Console.ReadLine().ToUpper() == "Y") { isManager = true; }
            Console.Write("Nhap Password:");
            String password = Console.ReadLine();
            
            SqlConnection conn = new DbConnector().GetConnection();
            conn.Open();

            string sql = "INSERT INTO dbo.employees (no, name, email, password, isManager)" +
                "VALUES (@no, @name, @email, @password, @isManager)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@no", no);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@isManager", isManager);
            cmd.ExecuteNonQuery();

            conn.Close();
        }
        public override void Update() 
        {
            Find();
            Console.Write("Lua chon ID tai khoan can Update: ");
            String idUpdate = Console.ReadLine();

            Console.WriteLine("Chon truong thong tin ban muon Update");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Email");
            Console.WriteLine("3. Manager");
            Console.Write("Select (1-3): ");
            int fieldSelect = Convert.ToInt32(Console.ReadLine());
            switch (fieldSelect)
            {
                case 1:
                    Console.Write("Nhap Name moi: ");
                    emps[indexUpdate].Name = Console.ReadLine();
                    break;
                case 2:
                    Console.Write("Nhap Email moi: ");
                    emps[indexUpdate].Email = Console.ReadLine();
                    break;
                case 3:
                    Console.WriteLine("La quan ly hay khong (y/n): ");
                    Boolean isManager = false;
                    if (Console.ReadLine().ToUpper() == "Y") { isManager = true; }
                    emps[indexUpdate].IsManager = isManager;
                    break;
            }

            SqlConnection conn = new DbConnector().GetConnection();
            conn.Open();

            string sql = "INSERT INTO dbo.employees (no, name, email, password, isManager)" +
                "VALUES (@no, @name, @email, @password, @isManager)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@no", no);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@isManager", isManager);
            conn.Close();

            int indexUpdate = -1;
            foreach (Employee emp in this.emps)
            {
                if(emp.No == idUpdate)
                {
                    indexUpdate = this.emps.IndexOf(emp);
                }
            }     
            
        }
        public override void Remove()
        {
            Find();
            Console.Write("Lua chon ID tai khoan can xoa: ");
            String idRemove = Console.ReadLine();
            int indexRemove = -1;
            foreach (Employee emp in this.emps)
            {
                if (emp.No == idRemove)
                {
                    indexRemove = this.emps.IndexOf(emp);
                }
            }
            this.emps.RemoveAt(indexRemove);
        }
        public override void Export()
        {
            StreamWriter streamWriter = new StreamWriter("C:\\Users\\TRUNGTV\\T8.csv", false, System.Text.Encoding.UTF8);
            try
            {
                foreach (Employee e in emps)
                {
                    streamWriter.WriteLine(e);
                    streamWriter.Flush();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                streamWriter.Close();
            }
        }
        public override void Import()
        {
            String path = "C:\\Users\\TRUNGTV\\T8import.csv";
            StreamReader reader = new StreamReader(path) ;
            try
            {
                string line;
                while ((line = reader.ReadLine()) !=null)
                {
                    //tách chuỗi gán vào mảng 
                    string[] strings = line.Split(',');

                    //
                    String no = strings[0];
                    String name = strings[1];
                    String email = strings[2];
                    String password = strings[3];
                    Boolean isManager = false;
                    if (strings[4].ToUpper() == "TRUE")
                    {
                        isManager = true;
                    }
                    emps.Add(new Employee(no, name, email, password, isManager));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                reader.Close();
            }
        }

    }
}
