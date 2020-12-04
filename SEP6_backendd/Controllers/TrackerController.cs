using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEP6_backendd.Services;

namespace SEP6_backendd.Controllers
{
    [Route("v1/tracker/{id}")]
    public class TrackerController : Controller
    {
        private readonly ITrackerService _trackerService;

        public TrackerController(ITrackerService trackerService)
        {
            _trackerService = trackerService;
        }

        [HttpGet]
        public IActionResult GetMonthlyFlights(string id)
        {
            if (id == "airtime")
            {
                var result = _trackerService.GetMeanAirtime();
                return Ok(result);
            }
            else if (id == "delay")
            {
                var result = _trackerService.GetMeanDelay();
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
