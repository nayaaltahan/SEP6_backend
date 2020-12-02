using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEP6_backendd.Services;

namespace SEP6_backendd.Controllers
{
    [Route("v1/frequency/{id}")]
    public class FrequencyController : Controller
    {
        private readonly IFrequencyService _frequencyService;

        public FrequencyController(IFrequencyService frequencyService)
        {
            _frequencyService = frequencyService;
        }

        [HttpGet]
        public IActionResult GetMonthlyFlights(string id)
        {
            if (id == "origin")
            {
                var result = _frequencyService.GetMonthlyFlightsOrigin();
                return Ok(result);
            }
            else if(id == "monthly")
            {
                var result = _frequencyService.GetMonthlyFlights();
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
