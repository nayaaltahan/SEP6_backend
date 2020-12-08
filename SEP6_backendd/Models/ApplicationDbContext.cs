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

        public List<Weather> GetWeathersInOrigin()
        {
            List<Weather> weathers = new List<Weather>();
            try
            {
                var conn = ConnectToDB();

                var rdr = ExecuteQuery("SELECT origin,  count(year) FROM weather GROUP BY origin", conn);

                while (rdr.Read())
                {
                    var weather = new Weather
                    {
                        Origin = rdr.GetString(0),
                        Count = rdr.GetInt16(1)
                    };
                    weathers.Add(weather);
                }

                CloseConnections(rdr, conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return weathers;
        }

        public List<TemperaturesOrigin> GetTemperatureAttributes()
        {
            List<TemperaturesOrigin> temperaturesOrigins = new List<TemperaturesOrigin>();
            try
            {
                var conn = ConnectToDB();

                var rdr = ExecuteQuery("SELECT ROUND((dewp - 32) * 5.0 / 9 , 3) AS dewpp , ROUND((temp - 32) * 5.0 / 9 ,3) AS tempp, origin, time_hour FROM weather;", conn);

                string origin = "";
                int j = 0;
                while (rdr.Read() && j < 3)
                {
                    var temperatureOrigin = new TemperaturesOrigin();
                    temperatureOrigin.Origin = rdr.GetString(2);
                    origin = temperatureOrigin.Origin;
                    var temperatureAtts = new List<TemperatureAttributes>();
                    do
                    {
                        var temperatureAtt = new TemperatureAttributes()
                        {
                            Dewp = rdr.GetDouble(0),
                            Temp = rdr.GetDouble(1),
                            Time = rdr.GetDateTime(3)
                        };
                        temperatureAtts.Add(temperatureAtt);
                        origin = temperatureOrigin.Origin;
                    } while (rdr.Read() && rdr.GetString(2) == origin);

                    temperatureOrigin.TemperatureAtts = temperatureAtts;
                    temperaturesOrigins.Add(temperatureOrigin);
                    j++;
                }

                CloseConnections(rdr, conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return temperaturesOrigins;
        }

        public List<Temperature> GetTemperatureJfk()
        {
            List<Temperature> temperatures = new List<Temperature>();
            try
            {
                var conn = ConnectToDB();

                var rdr = ExecuteQuery("SELECT ROUND((temp - 32) * 5.0 / 9 ,3) AS tempp,time_hour FROM weather WHERE origin =\"JFK\" ", conn);

                while (rdr.Read())
                {
                    var temperature = new Temperature()
                    {
                        Temp = rdr.GetDouble(0),
                        Time = rdr.GetDateTime(1)
                    };
                    temperatures.Add(temperature);
                }

                CloseConnections(rdr, conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return temperatures;
        }

        public List<Temperature> GetMeanTemperatureJfk()
        {
            List<Temperature> temperatures = new List<Temperature>();
            try
            {
                var conn = ConnectToDB();

                var rdr = ExecuteQuery("SELECT ROUND(AVG((temp - 32) * 5.0 / 9 ),1) AS tempp, DATE(time_hour) AS datee FROM weather WHERE origin =\"JFK\" GROUP BY datee", conn);

                while (rdr.Read())
                {
                    var temperature = new Temperature()
                    {
                        Temp = rdr.GetDouble(0),
                        Time = rdr.GetDateTime(1)
                    };
                    temperatures.Add(temperature);
                }

                CloseConnections(rdr, conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return temperatures;
        }

        public List<TemperaturesOrigin> GetMeanTemperatureOrigins()
        {
            List<TemperaturesOrigin> temperaturesOrigins = new List<TemperaturesOrigin>();
            try
            {
                var conn = ConnectToDB();

                var rdr = ExecuteQuery("SELECT ROUND(AVG((temp - 32) * 5.0 / 9 ),1) AS tempp, DATE(time_hour) AS datee,origin FROM weather GROUP BY datee, origin;", conn);

                string origin = "";
                int j = 0;
                while (rdr.Read() && j < 3)
                {
                    var temperatureOrigin = new TemperaturesOrigin();
                    temperatureOrigin.Origin = rdr.GetString(2);
                    origin = temperatureOrigin.Origin;
                    var temperatureAtts = new List<TemperatureAttributes>();
                    do
                    {
                        var temperatureAtt = new TemperatureAttributes()
                        {
                            Temp = rdr.GetDouble(0),
                            Time = rdr.GetDateTime(1)
                        };
                        temperatureAtts.Add(temperatureAtt);
                        origin = temperatureOrigin.Origin;
                    } while (rdr.Read() && rdr.GetString(2) == origin);

                    temperatureOrigin.TemperatureAtts = temperatureAtts;
                    temperaturesOrigins.Add(temperatureOrigin);
                    j++;
                }

                CloseConnections(rdr, conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return temperaturesOrigins;
        }

        public List<Manufacturer> GetManufacturers200()
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            try
            {
                var conn = ConnectToDB();

                var rdr = ExecuteQuery("SELECT count(*), manufacturer FROM planes group by manufacturer HAVING count(*) > 200 ;", conn);

                while (rdr.Read())
                {
                    var manufacturer = new Manufacturer()
                    {
                        Count = rdr.GetInt16(0),
                        Name = rdr.GetString(1)
                    };
                    manufacturers.Add(manufacturer);
                }

                CloseConnections(rdr, conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return manufacturers;
        }

        public List<Manufacturer> GetManufacturersFlights()
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            try
            {
                var conn = ConnectToDB();

                var rdr = ExecuteQuery("SELECT planes.manufacturer, COUNT(flights.origin) FROM planes join flights ON planes.tailnum = flights.tailnum WHERE planes.manufacturer IN (SELECT manufacturer FROM planes group by manufacturer HAVING count(*) > 200 ) GROUP BY planes.manufacturer ;", conn);

                while (rdr.Read())
                {
                    var manufacturer = new Manufacturer()
                    {
                        Name = rdr.GetString(0),
                        Count = rdr.GetInt32(1)
                    };
                    manufacturers.Add(manufacturer);
                }

                CloseConnections(rdr, conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return manufacturers;
        }

        public List<Airbus> GetAirbuses()
        {
            List<Airbus> airbuses = new List<Airbus>();
            try
            {
                var conn = ConnectToDB();

                var rdr = ExecuteQuery("SELECT count(*), model FROM planes group by model ; ", conn);

                while (rdr.Read())
                {
                    var airbus = new Airbus
                    {
                        Count = rdr.GetInt16(0),
                        Model = rdr.GetString(1)
                    };
                    airbuses.Add(airbus);
                }

                CloseConnections(rdr, conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return airbuses;
        }

        public List<DestinationOrigin> GetDestinationOrigins()
        {
            List<DestinationOrigin> originFlights = new List<DestinationOrigin>();
            try
            {
                var conn = ConnectToDB();

                var rdr = ExecuteQuery("SELECT dest , COUNT(dest), origin FROM flights WHERE dest IN (select * FROM (Select dest FROM flights GROUP BY dest ORDER BY COUNT(dest) DESC LIMIT 0,10) AS t1) GROUP BY dest, origin ORDER BY origin, dest;", conn);

                string origin = "";
                int j = 0;
                while (rdr.Read() && j < 3)
                {
                    var originFlight = new DestinationOrigin();
                    originFlight.Origin= rdr.GetString(2);
                    origin = originFlight.Origin;
                    var flights = new List<DestinationFlights>();
                    do
                    {
                        var flight = new DestinationFlights
                        {
                            Destination = rdr.GetString(0),
                            count = rdr.GetInt16(1)
                        };
                        flights.Add(flight);
                        origin = rdr.GetString(2);
                    } while (rdr.Read() && rdr.GetString(2) == origin);

                    originFlight.Flights = flights;
                    originFlights.Add(originFlight);
                    
                    j++;
                }

                CloseConnections(rdr, conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return originFlights;
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
