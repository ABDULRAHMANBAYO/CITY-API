using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Models
{
    public class CityWithoutPointsOfInterestDto
    {
         public int Id{get;set;}

         public string  Name{ get; set; }

         public string Description { get; set; }
    }
}