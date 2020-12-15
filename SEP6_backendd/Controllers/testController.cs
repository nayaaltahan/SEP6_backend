using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEP6_backendd.Controllers
{
    [Route("v1")]
    public class testController : Controller
    {
        public testController()
        {
            
        }

        [HttpGet]
        public IActionResult GetMonthlyFlights()
        {
            return Ok("Hi");
        }
    }
}
