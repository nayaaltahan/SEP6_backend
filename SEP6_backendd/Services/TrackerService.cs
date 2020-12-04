using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEP6_backendd.Models;

namespace SEP6_backendd.Services
{
    public class TrackerService: ITrackerService
    {
        private List<Airtime> _airtimes;
        private List<Delay> _delays;


        private readonly ApplicationDbContext _applicationDbContext;

        public TrackerService()
        {
            _applicationDbContext = new ApplicationDbContext();
        }

        public List<Airtime> GetMeanAirtime()
        {
            if (_airtimes == null || _airtimes.Count == 0)
            {
                _airtimes = _applicationDbContext.GetAirtime();
            }
            return _airtimes;
        }

        public List<Delay> GetMeanDelay()
        {
            if (_delays == null || _delays.Count == 0)
            {
                _delays = _applicationDbContext.GetDelays();
            }
            return _delays;
        }
    }
}
