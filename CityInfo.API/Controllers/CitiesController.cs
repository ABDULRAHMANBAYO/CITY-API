using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        [HttpGet()]
        public IActionResult GetCities()
        { 
            return Ok(CitiesDataStore.Current.Cities);
        }
        [HttpGet("{id}")]
        public IActionResult GetCities(int id)
        {
            //find city by Id
            var cityToReturn=CitiesDataStore.Current.Cities.FirstOrDefault(p => p.Id == id);
            if (cityToReturn==null)
            {
                return NotFound();
            }
            return Ok(cityToReturn);

        }
    }
}


