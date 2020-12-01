using System;
using System.Collections.Generic;

namespace SEP6_backendd.Models
{
    public partial class Flights
    {
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public int? DepTime { get; set; }
        public int? DepDelay { get; set; }
        public int? ArrTime { get; set; }
        public int? ArrDelay { get; set; }
        public string Carrier { get; set; }
        public string Tailnum { get; set; }
        public int? Flight { get; set; }
        public string Origin { get; set; }
        public string Dest { get; set; }
        public int? AirTime { get; set; }
        public int? Distance { get; set; }
        public int? Hour { get; set; }
        public int? Minute { get; set; }
    }
}
