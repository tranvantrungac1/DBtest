using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DBtest
{
    internal class DbConnector
    {
        public string Database { get; set; }
        public string Server { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public DbConnector()
        {
            Server = "10.36.0.36";
            Database = "TRUNGTV_BEPAN";
            User = "trungtv";
            Password = "@Automation1";
        }
        public DbConnector(string server, string database, string user, string password)
        {
            Server = server;
            Database = database;
            User = user;
            Password = password;
        }
        public SqlConnection GetConnection()
        {
            string connStr = $"Server={Server};Database={Database};User ID={User};Password={Password}";
            SqlConnection sqlConnection = new SqlConnection(connStr);
            return sqlConnection;
        }
    }
}
