using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEP6_backendd.Models;

namespace SEP6_backendd.Services
{
    public class PlaneService: IPlanesService
    {
        private List<Manufacturer> _manufacturers;
        private List<Manufacturer> _manufacturersFlights;
        private List<Airbus> _airbuses;
        private readonly ApplicationDbContext _applicationDbContext;

        public PlaneService()
        {
            _applicationDbContext = new ApplicationDbContext();
        }

        public List<Manufacturer> GetManufacturers200()
        {
            if (_manufacturers == null || _manufacturers.Count == 0)
            {
                _manufacturers = _applicationDbContext.GetManufacturers200();
            }

            return _manufacturers;
        }

        public List<Manufacturer> GetManufacturersFlights()
        {
            if (_manufacturersFlights == null || _manufacturersFlights.Count == 0)
            {
                _manufacturersFlights = _applicationDbContext.GetManufacturersFlights();
            }

            return _manufacturersFlights;
        }

        public List<Airbus> GetAirbuses()
        {
            if (_airbuses == null || _airbuses.Count == 0)
            {
                _airbuses = _applicationDbContext.GetAirbuses();
            }

            return _airbuses;
        }
    }
}
