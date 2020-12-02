using System.Collections.Generic;
using SEP6_backendd.Models;

namespace SEP6_backendd.Services
{
    public interface IFrequencyService
    {
        List<MonthlyFlights> GetMonthlyFlights();
        List<MonthlyFlightsOrigin> GetMonthlyFlightsOrigin();
    }
}