using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEP6_backendd.Services
{
    public class AirplanesService : IAirplanesService
    {
        private readonly Dictionary<string, int> airplanes;

        public Dictionary<string, int> GetAirplanes()
        {
            return airplanes;
        }
    }
}
