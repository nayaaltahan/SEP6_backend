using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEP6_backendd.Models;
using SEP6_backendd.Repositories;

namespace SEP6_backendd.Services
{
    public class DestinationService: IDestinationService
    {
        public List<DestinationFlights> DestinationFlights { get; set; }
        public List<DestinationOrigin> DestinationOrigins { get; set; }

        private readonly DestinationRepository _destinationRepository;

        public DestinationService()
        {
            _destinationRepository = new DestinationRepository();
        }

        public List<DestinationFlights> GetDestinationFlights()
        {
            if (DestinationFlights == null || DestinationFlights.Count == 0)
            {
                DestinationFlights = _destinationRepository.GetDestinationFlights();
            }
            return DestinationFlights;
        }

        public List<DestinationOrigin> GetDestinationOrigins()
        {
            if (DestinationOrigins == null || DestinationOrigins.Count == 0)
            {
                DestinationOrigins = _destinationRepository.GetDestinationOrigins();
            }
            return DestinationOrigins;
        }
    }
}
