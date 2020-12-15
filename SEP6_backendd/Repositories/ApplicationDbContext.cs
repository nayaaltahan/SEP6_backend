using System;
using MySql.Data.MySqlClient;

namespace SEP6_backendd.Repositories
{
    public class ApplicationDbContext
    {
        private static ApplicationDbContext _instance;
        private ApplicationDbContext()
        {
        }

        public static ApplicationDbContext GetInstance()
        {
            return _instance ??= new ApplicationDbContext();
        }

        public MySqlConnection ConnectToDB()
        {
            var conn = new MySqlConnection("server=sep6.cr8rrqpu4nwe.eu-west-1.rds.amazonaws.com;user=admin;password=Admin123;database=sep6_db");
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
            Console.WriteLine("Connected to MySQL...");
            return conn;
        }

        public MySqlDataReader ExecuteQuery(string query, MySqlConnection conn)
        {
            var sql = query;
            var cmd = new MySqlCommand(sql, conn);
            var rdr = cmd.ExecuteReader();
            return rdr;
        }

        public void CloseConnections(MySqlDataReader rdr, MySqlConnection conn)
        {
            rdr?.Close();
            conn?.Close();
        }


    }
}
