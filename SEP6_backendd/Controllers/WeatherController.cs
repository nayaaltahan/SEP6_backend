using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEP6_backendd.Controllers
{
    public class WeatherController : Controller
    {
        public IActionResult Index()
        {
            return NoContent();
        }
    }
}
