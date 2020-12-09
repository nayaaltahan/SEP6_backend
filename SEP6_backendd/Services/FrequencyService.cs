using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEP6_backendd.Models;
using SEP6_backendd.Repositories;

namespace SEP6_backendd.Services
{
    public class FrequencyService : IFrequencyService
    {
        private List<MonthlyFlights> _monthlyFlights;
        private List<MonthlyFlightsOrigin> _monthlyFlightsOrigin;


        private readonly FrequencyRepository _frequencyRepository;

        public FrequencyService()
        {
            _frequencyRepository = new FrequencyRepository();
        }

        public List<MonthlyFlights> GetMonthlyFlights()
        {
            if (_monthlyFlights == null || _monthlyFlights.Count == 0)
            {
                var monthlyFlights =
                    _frequencyRepository.GetMonthlyFlights("SELECT MONTH , COUNT(MONTH)\r\nFROM `flights`\r\nGROUP BY MONTH ORDER BY MONTH ASC;");
                _monthlyFlights = monthlyFlights;
            }

            return _monthlyFlights;
        }

        public List<MonthlyFlightsOrigin> GetMonthlyFlightsOrigin()
        {
            if (_monthlyFlightsOrigin == null || _monthlyFlightsOrigin.Count == 0)
            {
                var monthlyFlightsOrigin =
                    _frequencyRepository.GetMonthlyFlightsOrigin();
                _monthlyFlightsOrigin = monthlyFlightsOrigin;
            }

            return _monthlyFlightsOrigin;
        }
    }
}
