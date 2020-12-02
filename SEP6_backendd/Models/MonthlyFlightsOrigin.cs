using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEP6_backendd.Models
{
    public class MonthlyFlightsOrigin
    {
        public string origin { get; set; }

        public List<MonthlyFlights> monthlyFlights
        {
            get;
            set;
        }
    }

}