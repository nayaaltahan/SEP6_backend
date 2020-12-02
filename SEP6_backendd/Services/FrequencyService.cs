using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEP6_backendd.Models;

namespace SEP6_backendd.Services
{
    public class FrequencyService : IFrequencyService
    {
        private List<MonthlyFlights> _monthlyFlights;
        private List<MonthlyFlightsOrigin> _monthlyFlightsOrigin;


        private readonly ApplicationDbContext _applicationDbContext;

        public FrequencyService()
        {
            _applicationDbContext = new ApplicationDbContext();
        }

        public List<MonthlyFlights> GetMonthlyFlights()
        {
            if (_monthlyFlights == null || _monthlyFlights.Count == 0)
            {
                var monthlyFlights =
                    _applicationDbContext.GetMonthlyFlights("SELECT MONTH , COUNT(MONTH)\r\nFROM `flights`\r\nGROUP BY MONTH ORDER BY MONTH ASC;");
                _monthlyFlights = monthlyFlights;
            }

            return _monthlyFlights;
        }

        public List<MonthlyFlightsOrigin> GetMonthlyFlightsOrigin()
        {
            if (_monthlyFlightsOrigin == null || _monthlyFlightsOrigin.Count == 0)
            {
                var monthlyFlightsOrigin =
                    _applicationDbContext.GetMonthlyFlightsOrigin("SELECT origin , MONTH , COUNT(origin) FROM `flights` GROUP BY origin , MONTH ORDER BY origin , month ASC");
                _monthlyFlightsOrigin = monthlyFlightsOrigin;
            }

            return _monthlyFlightsOrigin;
        }
    }
}
