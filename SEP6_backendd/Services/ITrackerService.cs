using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEP6_backendd.Models;

namespace SEP6_backendd.Services
{
    public interface ITrackerService
    {
        List<Airtime> GetMeanAirtime();
        List<Delay> GetMeanDelay();
    }
}
