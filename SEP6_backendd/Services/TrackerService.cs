using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEP6_backendd.Models;
using SEP6_backendd.Repositories;

namespace SEP6_backendd.Services
{
    public class TrackerService: ITrackerService
    {
        private List<Airtime> _airtimes;
        private List<Delay> _delays;


        private readonly TrackerRepository _trackerRepository;

        public TrackerService()
        {
            _trackerRepository = new TrackerRepository();
        }

        public List<Airtime> GetMeanAirtime()
        {
            if (_airtimes == null || _airtimes.Count == 0)
            {
                _airtimes = _trackerRepository.GetAirtime();
            }
            return _airtimes;
        }

        public List<Delay> GetMeanDelay()
        {
            if (_delays == null || _delays.Count == 0)
            {
                _delays = _trackerRepository.GetDelays();
            }
            return _delays;
        }
    }
}
