using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEP6_backendd.Services;

namespace SEP6_backendd.Controllers
{
    [Route("v1/airplanes")]
    public class AirplanesController : Controller
    {
        private readonly IAirplanesService _airplanesService;

        public AirplanesController(IAirplanesService airplanesService)
        {
            _airplanesService = airplanesService;
        }

        [HttpGet]
        public IActionResult GetAirplanes()
        {
            var result = _airplanesService.GetAirplanes();
            return Ok(result);
        }
    }
}
