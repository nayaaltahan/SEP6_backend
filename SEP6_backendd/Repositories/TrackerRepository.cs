using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEP6_backendd.Models;

namespace SEP6_backendd.Repositories
{
    public class TrackerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TrackerRepository()
        {
            _dbContext = ApplicationDbContext.GetInstance();
        }

        public List<Airtime> GetAirtime()
        {
            List<Airtime> airtimes = new List<Airtime>();
            try
            {
                var conn = _dbContext.ConnectToDB();

                var rdr = _dbContext.ExecuteQuery("SELECT origin,  AVG(air_time) FROM flights GROUP BY origin", conn);

                while (rdr.Read())
                {
                    var airtime = new Airtime
                    {
                        Origin = rdr.GetString(0),
                        MeanAirtime = rdr.GetDouble(1)
                    };
                    airtimes.Add(airtime);
                }

                _dbContext.CloseConnections(rdr, conn);
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
                var conn = _dbContext.ConnectToDB();

                var rdr = _dbContext.ExecuteQuery("SELECT origin,  AVG(dep_delay) , AVG(arr_delay) FROM flights GROUP BY origin", conn);

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

                _dbContext.CloseConnections(rdr, conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return delays;
        }
    }
}
