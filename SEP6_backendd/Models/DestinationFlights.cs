using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SEP6_backendd.Models
{ 
    public class DestinationFlights: Flights
    {
        public string Destination { get; set; }
    }
}
