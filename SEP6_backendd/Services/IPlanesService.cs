using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEP6_backendd.Models;

namespace SEP6_backendd.Services
{
    public interface IPlanesService
    {
        List<Manufacturer> GetManufacturers200();
        List<Manufacturer> GetManufacturersFlights();
        List<Airbus> GetAirbuses();
    }
}
