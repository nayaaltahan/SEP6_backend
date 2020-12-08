using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEP6_backendd.Services;

namespace SEP6_backendd.Controllers
{
    [Route("v1/planes/{id}")]
    public class PlanesController : ControllerBase
    {
        private readonly IPlanesService _planesService;

        public PlanesController(IPlanesService planesService)
        {
            _planesService = planesService;
        }

        [HttpGet]
        public IActionResult GetPlanes(string id)
        {
            if (id == "200")
            {
                var result = _planesService.GetManufacturers200();
                return Ok(result);
            }
            else if (id == "flights")
            {
                var result = _planesService.GetManufacturersFlights();
                return Ok(result);
            }
            else if (id == "airbuses")
            {
                var result = _planesService.GetAirbuses();
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
