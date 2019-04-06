using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class CitiesController:Controller
    {
         [HttpGet() ]
        public JsonResult GetCities()
        {  
            return new JsonResult(new List<object>()
            {
                new{id=1,Name="Lagos"},
                new{id=2,Name="Abeokuta"},
            });
        }
    }
}


  