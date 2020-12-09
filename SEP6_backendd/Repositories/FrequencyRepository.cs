using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEP6_backendd.Models;

namespace SEP6_backendd.Repositories
{
    public class FrequencyRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FrequencyRepository()
        {
            _dbContext = ApplicationDbContext.GetInstance();
        }

        public List<MonthlyFlights> GetMonthlyFlights(string query)
        {
            List<MonthlyFlights> monthlyFlights = new List<MonthlyFlights>();
            try
            {
                var conn = _dbContext.ConnectToDB();

                var rdr = _dbContext.ExecuteQuery(query, conn);

                while (rdr.Read())
                {
                    var monthlyFlight = new MonthlyFlights
                    {
                        month = rdr.GetInt16(0),
                        count = rdr.GetInt16(1)
                    };
                    monthlyFlights.Add(monthlyFlight);
                }

                _dbContext.CloseConnections(rdr, conn);
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
                var conn = _dbContext.ConnectToDB();

                var rdr = _dbContext.ExecuteQuery("SELECT origin , MONTH , COUNT(origin) FROM `flights` GROUP BY origin , MONTH ORDER BY origin , month ASC", conn);
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

                _dbContext.CloseConnections(rdr, conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return monthlyFlightsOrigins;
        }
    }
}
