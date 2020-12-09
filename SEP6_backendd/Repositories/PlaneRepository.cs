using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEP6_backendd.Models;

namespace SEP6_backendd.Repositories
{
    public class PlaneRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PlaneRepository()
        {
            _dbContext = ApplicationDbContext.GetInstance();
        }

        public List<Manufacturer> GetManufacturers200()
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            try
            {
                var conn = _dbContext.ConnectToDB();

                var rdr = _dbContext.ExecuteQuery("SELECT count(*), manufacturer FROM planes group by manufacturer HAVING count(*) > 200 ;", conn);

                while (rdr.Read())
                {
                    var manufacturer = new Manufacturer()
                    {
                        Count = rdr.GetInt16(0),
                        Name = rdr.GetString(1)
                    };
                    manufacturers.Add(manufacturer);
                }

                _dbContext.CloseConnections(rdr, conn);
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
                var conn = _dbContext.ConnectToDB();

                var rdr = _dbContext.ExecuteQuery("SELECT planes.manufacturer, COUNT(flights.origin) FROM planes join flights ON planes.tailnum = flights.tailnum WHERE planes.manufacturer IN (SELECT manufacturer FROM planes group by manufacturer HAVING count(*) > 200 ) GROUP BY planes.manufacturer ;", conn);

                while (rdr.Read())
                {
                    var manufacturer = new Manufacturer()
                    {
                        Name = rdr.GetString(0),
                        Count = rdr.GetInt32(1)
                    };
                    manufacturers.Add(manufacturer);
                }

                _dbContext.CloseConnections(rdr, conn);
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
                var conn = _dbContext.ConnectToDB();

                var rdr = _dbContext.ExecuteQuery("SELECT count(*), model FROM planes group by model ; ", conn);

                while (rdr.Read())
                {
                    var airbus = new Airbus
                    {
                        Count = rdr.GetInt16(0),
                        Model = rdr.GetString(1)
                    };
                    airbuses.Add(airbus);
                }

                _dbContext.CloseConnections(rdr, conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return airbuses;
        }
    }
}
