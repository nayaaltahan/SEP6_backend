using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEP6_backendd.Models
{
    public class DestinationOrigin
    {
        public string Origin { get; set; }

        public List<DestinationFlights> Flights { get; set; }
    }
}
