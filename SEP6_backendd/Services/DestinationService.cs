using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEP6_backendd.Models;

namespace SEP6_backendd.Services
{
    public class DestinationService: IDestinationService
    {
        public List<DestinationFlights> DestinationFlights { get; set; }
        public List<DestinationOrigin> DestinationOrigins { get; set; }

        private readonly ApplicationDbContext _applicationDbContext;

        public DestinationService()
        {
            _applicationDbContext = new ApplicationDbContext();
        }

        public List<DestinationFlights> GetDestinationFlights()
        {
            if (DestinationFlights == null || DestinationFlights.Count == 0)
            {
                DestinationFlights = _applicationDbContext.GetDestinationFlights();
            }
            return DestinationFlights;
        }

        public List<DestinationOrigin> GetDestinationOrigins()
        {
            if (DestinationOrigins == null || DestinationOrigins.Count == 0)
            {
                DestinationOrigins = _applicationDbContext.GetDestinationOrigins();
            }
            return DestinationOrigins;
        }
    }
}
