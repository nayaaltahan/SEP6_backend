using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace SEP6_backendd.Models
{
    public class ApplicationDbContext
    {
        public IConfiguration Configuration;

        public ApplicationDbContext()
        {
        }

        public List<MonthlyFlights> GetMonthlyFlights(string query)
        {
            List<MonthlyFlights> monthlyFlights = new List<MonthlyFlights>();
            try
            {
                var conn = ConnectToDB();

                var rdr = ExecuteQuery(query, conn);
                
                while (rdr.Read())
                {
                    Console.WriteLine(rdr[0] + " -- " + rdr[1]);
                    var monthlyFlight = new MonthlyFlights();
                    monthlyFlight.month = Int16.Parse(rdr[0].ToString());
                    monthlyFlight.count = Int32.Parse(rdr[1].ToString());
                    monthlyFlights.Add(monthlyFlight);
                }

                CloseConnections(rdr,conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return monthlyFlights;
        }

        public List<MonthlyFlightsOrigin> GetMonthlyFlightsOrigin(string query)
        {
            List<MonthlyFlightsOrigin> monthlyFlightsOrigins = new List<MonthlyFlightsOrigin>();
            try
            {
                var conn = ConnectToDB();

                var rdr = ExecuteQuery(query, conn);
                //SELECT origin , MONTH , COUNT(origin) FROM `flights` GROUP BY origin , MONTH ORDER BY origin , month ASC
                while (rdr.Read())
                {
                    var monthlyFlightOrigin = new MonthlyFlightsOrigin();
                    monthlyFlightOrigin.origin = rdr[0].ToString();
                    var monthlyFlights = new List<MonthlyFlights>();
                    for (int i = 0; i < 12 ;i++)
                    {
                        var monthlyFlight = new MonthlyFlights();
                        monthlyFlight.month = Int16.Parse(rdr[1].ToString());
                        monthlyFlight.count = Int32.Parse(rdr[2].ToString());
                        monthlyFlights.Add(monthlyFlight);
                    }
                    monthlyFlightOrigin.monthlyFlights = monthlyFlights;
                    monthlyFlightsOrigins.Add(monthlyFlightOrigin);
                }

                CloseConnections(rdr, conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return monthlyFlightsOrigins;
        }

        public MySqlConnection ConnectToDB()
        {
            MySqlConnection conn = new MySqlConnection("server=sep6.cr8rrqpu4nwe.eu-west-1.rds.amazonaws.com;user=admin;password=Admin123;database=sep6_db");
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
            return conn;
        }

        public MySqlDataReader ExecuteQuery(string query, MySqlConnection conn)
        {
            string sql = query;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            return rdr;
        }

        public void CloseConnections(MySqlDataReader rdr, MySqlConnection conn)
        {
            rdr?.Close();
            conn?.Close();
        }
    }
}
