using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEP6_backendd.Services;

namespace SEP6_backendd.Controllers
{
    [Route("v1/weather/{id}")]
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public IActionResult GetWeathersInOrigins(string id)
        {
            if (id == "origin")
            {
                var result = _weatherService.getWeathersInOrigins();
                return Ok(result);
            }
            else if (id == "temperatureAtt")
            {
                var result = _weatherService.GetTemperatureAttributes();
                return Ok(result);
            }
            else if (id == "temperature")
            {
                var result = _weatherService.GetTemperaturesJfk();
                return Ok(result);
            }
            else if (id == "meanTemperature")
            {
                var result = _weatherService.GetMeanTemperatureJfk();
                return Ok(result);
            }
            else if (id == "meanTemperatureOrigin")
            {
                var result = _weatherService.GetMeanTemperatureOrigins();
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
