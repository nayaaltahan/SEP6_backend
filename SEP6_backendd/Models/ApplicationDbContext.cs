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
                    var monthlyFlight = new MonthlyFlights
                    {
                        month = rdr.GetInt16(0),
                        count = rdr.GetInt16(1)
                    };
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

        public List<MonthlyFlightsOrigin> GetMonthlyFlightsOrigin()
        {
            List<MonthlyFlightsOrigin> monthlyFlightsOrigins = new List<MonthlyFlightsOrigin>();
            try
            {
                var conn = ConnectToDB();

                var rdr = ExecuteQuery("SELECT origin , MONTH , COUNT(origin) FROM `flights` GROUP BY origin , MONTH ORDER BY origin , month ASC", conn);
                int i = 0;
                int j = 0;
                while (rdr.Read() && j < 3)
                {
                    var monthlyFlightOrigin = new MonthlyFlightsOrigin();
                    monthlyFlightOrigin.origin = rdr.GetString(0);
                    var monthlyFlights = new List<MonthlyFlights>();
                    do
                    {
                        var monthlyFlight = new MonthlyFlights
                        {
                            month = rdr.GetInt16(1),
                            count = rdr.GetInt16(2)
                        };
                        monthlyFlights.Add(monthlyFlight);
                        i++;
                    } while (rdr.Read() && i < 11);

                    var monthlyFlight1 = new MonthlyFlights
                    {
                        month = rdr.GetInt16(1),
                        count = rdr.GetInt16(2)
                    };
                    monthlyFlights.Add(monthlyFlight1);
                    i = 0;
                    monthlyFlightOrigin.monthlyFlights = monthlyFlights;
                    monthlyFlightsOrigins.Add(monthlyFlightOrigin);
                    j++;
                }

                CloseConnections(rdr, conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return monthlyFlightsOrigins;
        }

        public List<DestinationFlights> GetDestinationFlights()
        {
            List<DestinationFlights> destinationFlights = new List<DestinationFlights>();
            try
            {
                var conn = ConnectToDB();

                var rdr = ExecuteQuery("SELECT dest, COUNT(dest) FROM `flights` GROUP BY dest ORDER BY COUNT(dest) DESC LIMIT 10", conn);

                while (rdr.Read())
                {
                    var destinationFlight = new DestinationFlights
                    {
                        Destination = rdr.GetString(0),
                        count = rdr.GetInt16(1)
                    };
                    destinationFlights.Add(destinationFlight);
                }

                CloseConnections(rdr, conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return destinationFlights;
        }

        public List<Airtime> GetAirtime()
        {
            List<Airtime> airtimes = new List<Airtime>();
            try
            {
                var conn = ConnectToDB();

                var rdr = ExecuteQuery("SELECT origin,  AVG(air_time) FROM flights GROUP BY origin", conn);

                while (rdr.Read())
                {
                    var airtime = new Airtime
                    {
                        Origin = rdr.GetString(0),
                        MeanAirtime = rdr.GetDouble(1)
                    };
                    airtimes.Add(airtime);
                }

                CloseConnections(rdr, conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return airtimes;
        }

        public List<Delay> GetDelays()
        {
            List<Delay> delays = new List<Delay>();
            try
            {
                var conn = ConnectToDB();

                var rdr = ExecuteQuery("SELECT origin,  AVG(dep_delay) , AVG(arr_delay) FROM flights GROUP BY origin", conn);

                while (rdr.Read())
                {
                    var delay = new Delay
                    {
                        Origin = rdr.GetString(0),
                        DepartureDelay = rdr.GetDouble(1),
                        ArrivalDelay = rdr.GetDouble(2)
                    };
                    delays.Add(delay);
                }

                CloseConnections(rdr, conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return delays;
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
