using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEP6_backendd.Models
{
    public class TemperaturesOrigin
    {
        public string Origin { get; set; }
        public List<TemperatureAttributes> TemperatureAtts { get; set; }
    }
}
