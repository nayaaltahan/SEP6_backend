using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEP6_backendd.Models;

namespace SEP6_backendd.Repositories
{
    public class WeatherRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public WeatherRepository()
        {
            _dbContext = ApplicationDbContext.GetInstance();
        }

        public List<Weather> GetWeathersInOrigin()
        {
            List<Weather> weathers = new List<Weather>();
            try
            {
                var conn = _dbContext.ConnectToDB();

                var rdr = _dbContext.ExecuteQuery("SELECT origin,  count(year) FROM weather GROUP BY origin", conn);

                while (rdr.Read())
                {
                    var weather = new Weather
                    {
                        Origin = rdr.GetString(0),
                        Count = rdr.GetInt16(1)
                    };
                    weathers.Add(weather);
                }

                _dbContext.CloseConnections(rdr, conn);
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
                var conn = _dbContext.ConnectToDB();

                var rdr = _dbContext.ExecuteQuery("SELECT ROUND((dewp - 32) * 5.0 / 9 , 3) AS dewpp , ROUND((temp - 32) * 5.0 / 9 ,3) AS tempp, origin, time_hour FROM weather;", conn);

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

                _dbContext.CloseConnections(rdr, conn);
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
                var conn = _dbContext.ConnectToDB();

                var rdr = _dbContext.ExecuteQuery("SELECT ROUND((temp - 32) * 5.0 / 9 ,3) AS tempp,time_hour FROM weather WHERE origin =\"JFK\" ", conn);

                while (rdr.Read())
                {
                    var temperature = new Temperature()
                    {
                        Temp = rdr.GetDouble(0),
                        Time = rdr.GetDateTime(1)
                    };
                    temperatures.Add(temperature);
                }

                _dbContext.CloseConnections(rdr, conn);
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
                var conn = _dbContext.ConnectToDB();

                var rdr = _dbContext.ExecuteQuery("SELECT ROUND(AVG((temp - 32) * 5.0 / 9 ),1) AS tempp, DATE(time_hour) AS datee FROM weather WHERE origin =\"JFK\" GROUP BY datee", conn);

                while (rdr.Read())
                {
                    var temperature = new Temperature()
                    {
                        Temp = rdr.GetDouble(0),
                        Time = rdr.GetDateTime(1)
                    };
                    temperatures.Add(temperature);
                }

                _dbContext.CloseConnections(rdr, conn);
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
                var conn = _dbContext.ConnectToDB();

                var rdr = _dbContext.ExecuteQuery("SELECT ROUND(AVG((temp - 32) * 5.0 / 9 ),1) AS tempp, DATE(time_hour) AS datee,origin FROM weather GROUP BY datee, origin;", conn);

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

                _dbContext.CloseConnections(rdr, conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return temperaturesOrigins;
        }
    }
}
