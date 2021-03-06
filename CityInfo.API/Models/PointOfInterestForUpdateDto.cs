using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Models
{
    public class PointOfInterestForUpdateDto
    {
        [Required(ErrorMessage = "Please enter a Name value")]
        [MaxLength(50)]
        public string Name { get; set; }

        
        [MaxLength(200)]
        public string Description { get; set; }

    }
}