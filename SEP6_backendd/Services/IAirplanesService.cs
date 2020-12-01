using System.Collections.Generic;

namespace SEP6_backendd.Services
{
    public interface IAirplanesService
    {
        Dictionary<string, int> GetAirplanes();
    }
}