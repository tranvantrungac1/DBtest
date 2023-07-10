using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBtest
{
    internal class UserManager
    {
        SqlConnection conn;
        public UserManager()
        {
            DbConnector dbConnector = new DbConnector();
            this.conn = dbConnector.GetConnection();
            this.Connect();
        }
        private void Connect()
        {
            this.conn.Open();
            if(this.conn.State == System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Connected");
            }
        }
        public User Login (string username, string password)
        {
            User u = null;
            string sql = "SELECT * FROM dbo.users WHERE username = @username AND password = @password";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("username", username);
            cmd.Parameters.AddWithValue("password", Utils.Hash(password));
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                u = new User((int)r[0], (string)r[1], (string)r[2]);
            }
            return u;
        }
        public int Register(string username, string password)
        {
            string sql = $"INSERT INTO dbo.users (username, password) VALUES (@username, @password)";
            SqlCommand cmd = new SqlCommand(sql, this.conn);
            cmd.Parameters.AddWithValue("username", username);
            cmd.Parameters.AddWithValue("password", Utils.Hash(password));
            int rows = cmd.ExecuteNonQuery();
            return rows;
        }
    }
}
