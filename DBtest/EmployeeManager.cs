using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

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
                    Employee emp = new Employee((string)reader[1], (string)reader[2], (string)reader[3], (bool)reader[4]);
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
            String sql;
            do
            {
                Console.Write("Nhap User hoac Id: ");
                searchInfo = Console.ReadLine();
            } while (string.IsNullOrEmpty(searchInfo));

            SqlConnection conn = new DbConnector().GetConnection();
            conn.Open();
            if(int.TryParse(searchInfo, out int result))
            {
                sql = "SELECT * FROM dbo.employees WHERE no=@no";
            }
            else
            {
                sql = "SELECT * FROM dbo.employees WHERE name like @name";
            }
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@no", result);
            cmd.Parameters.AddWithValue("@name", "%" + searchInfo  + "%");
            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.HasRows)
            {
                Console.WriteLine("Tim thay danh sach sau: ");
                while (reader.Read())
                {
                    Employee emp = new Employee();
                    emp.No = (int)reader[0];
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
            
            SqlConnection conn = new DbConnector().GetConnection();
            conn.Open();
            bool checkName = true;
            String name="";
            while (checkName)
            {
                Console.Write("Nhap Ten: ");
                name = Console.ReadLine();
                string sqlStr = "SELECT * FROM dbo.employees WHERE name = @nameCheck";
                SqlCommand command = new SqlCommand(sqlStr, conn);
                command.Parameters.AddWithValue("@nameCheck", name);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    Console.WriteLine("Ten bi trung, de nghi nhap lai");
                }
                else
                {
                    checkName = false;
                }
                reader.Close();
            }
            Console.Write("Nhap Email: ");
            String email = Console.ReadLine();
            Console.Write("La quan ly y/n:");
            Boolean isManager = false;
            if (Console.ReadLine().ToUpper() == "Y") { isManager = true; }
            Console.Write("Nhap Password:");
            String password = Console.ReadLine();
            


            string sql = "INSERT INTO dbo.employees (name, email, password, isManager)" +
                "VALUES (@name, @email, @password, @isManager)";
            SqlCommand cmd = new SqlCommand(sql, conn);
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
            int idUpdate = Convert.ToInt16(Console.ReadLine());
            String sql;
            Console.WriteLine("Chon truong thong tin ban muon Update");
            Console.WriteLine("1. Email");
            Console.WriteLine("2. Manager");
            Console.Write("Select (1-2): ");
            int fieldSelect = Convert.ToInt32(Console.ReadLine());
            SqlConnection conn = new DbConnector().GetConnection();
            conn.Open();
           

            switch (fieldSelect)
            {
                case 1:
                    Console.Write("Nhap Email moi: ");
                    string email = Console.ReadLine();
                    sql = "UPDATE dbo.employees SET email = @email WHERE no=@no";
                    SqlCommand command = new SqlCommand(sql, conn);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@no", idUpdate);
                    command.ExecuteNonQuery();
                    break;
                case 2:
                    Console.WriteLine("La quan ly hay khong (y/n): ");
                    Boolean isManager = false;
                    if (Console.ReadLine().ToUpper() == "Y") { isManager = true; }
                    sql = "UPDATE dbo.employees SET isManager = @manager WHERE no = @no";
                    SqlCommand command1 = new SqlCommand(sql, conn);
                    command1.Parameters.AddWithValue("@manager", isManager);
                    command1.Parameters.AddWithValue("@no", idUpdate);
                    command1.ExecuteNonQuery();
                    break;
            }
            conn.Close();
        }
        public override void Remove()
        {
            Find();
            Console.Write("Lua chon ID tai khoan can xoa: ");
            String idRemove = Console.ReadLine();
            SqlConnection conn = new DbConnector().GetConnection();
            conn.Open();
            string sql = "DELETE FROM dbo.employees WHERE no = @no";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@no", idRemove);
            cmd.ExecuteNonQuery();
        }
        public override void Export()
        {
            StreamWriter streamWriter = new StreamWriter("C:\\Users\\ADMIN\\T8.csv", false, System.Text.Encoding.UTF8);
            try
            {
                SqlConnection conn = new DbConnector().GetConnection();
                conn.Open();
                string sql = "SELECT * FROM dbo.employees";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Employee emp = new Employee();
                    emp.No = (int)reader[0];
                    emp.Name = (string)reader[1];
                    emp.Email = (string)reader[2];
                    emp.Password = (string)reader[3];
                    emp.IsManager = (bool)reader[4];
                    streamWriter.WriteLine(emp);
                    streamWriter.Flush();
                }
                conn.Close();
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
            String path = "C:\\Users\\ADMIN\\T8import.csv";
            StreamReader reader = new StreamReader(path) ;
            try
            {
                string line;
                while ((line = reader.ReadLine()) !=null)
                {
                    //tách chuỗi gán vào mảng 
                    string[] strings = line.Split(',');

                    //
                    String name = strings[0];
                    String email = strings[1];
                    String password = strings[2];
                    Boolean isManager = false;
                    if (strings[3].ToUpper() == "TRUE")
                    {
                        isManager = true;
                    }
                    SqlConnection conn = new DbConnector().GetConnection();
                    conn.Open();
                    string sql = "INSERT INTO dbo.employees (name, email, password, isManager)" +
                "VALUES (@name, @email, @password, @isManager)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@isManager", isManager);
                    cmd.ExecuteNonQuery();
                    conn.Close();
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
