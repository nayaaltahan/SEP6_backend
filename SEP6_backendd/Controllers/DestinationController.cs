using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEP6_backendd.Services;

namespace SEP6_backendd.Controllers
{
    [Route("v1/destination/{id}")]
    public class DestinationController : Controller
    {
        private readonly IDestinationService _destinationService;

        public DestinationController(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        [HttpGet]
        public IActionResult GetDestinationFlights(string id)
        {
            if (id == "top10")
            {

                return Ok(_destinationService.GetDestinationFlights());
            }
            if (id == "top10Origin")
            {

                return Ok(_destinationService.GetDestinationOrigins());
            }
            else
            {
                return NoContent();
            }
        }
    }
}
