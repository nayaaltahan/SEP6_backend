using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEP6_backendd.Models;

namespace SEP6_backendd.Repositories
{
    public class DestinationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DestinationRepository()
        {
            _dbContext = ApplicationDbContext.GetInstance();
        }

        public List<DestinationFlights> GetDestinationFlights()
        {

            List<DestinationFlights> destinationFlights = new List<DestinationFlights>();
            try
            {
                var conn = _dbContext.ConnectToDB();

                var rdr = _dbContext.ExecuteQuery("SELECT dest, COUNT(dest) FROM `flights` GROUP BY dest ORDER BY COUNT(dest) DESC LIMIT 10", conn);

                while (rdr.Read())
                {
                    var destinationFlight = new DestinationFlights
                    {
                        Destination = rdr.GetString(0),
                        count = rdr.GetInt16(1)
                    };
                    destinationFlights.Add(destinationFlight);
                }

                _dbContext.CloseConnections(rdr, conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return destinationFlights;
        }

        public List<DestinationOrigin> GetDestinationOrigins()
        {
            List<DestinationOrigin> originFlights = new List<DestinationOrigin>();
            try
            {
                var conn = _dbContext.ConnectToDB();

                var rdr = _dbContext.ExecuteQuery("SELECT dest , COUNT(dest), origin FROM flights WHERE dest IN (select * FROM (Select dest FROM flights GROUP BY dest ORDER BY COUNT(dest) DESC LIMIT 0,10) AS t1) GROUP BY dest, origin ORDER BY origin, dest;", conn);

                var j = 0;
                while (rdr.Read() && j < 3)
                {
                    var originFlight = new DestinationOrigin {Origin = rdr.GetString(2)};
                    var origin = originFlight.Origin;
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

                _dbContext.CloseConnections(rdr, conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return originFlights;
        }

    }
}
