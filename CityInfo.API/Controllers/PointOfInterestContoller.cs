using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using CityInfo.API.Services;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointofInterestController : Controller
    {
        private ILogger<PointofInterestController> _logger;
        private IMailService _mailService;

        private ICityInfoRepository _cityInfoRepository;
        public PointofInterestController(
        ILogger<PointofInterestController> logger,
        IMailService mailService,
        ICityInfoRepository cityInfoRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _cityInfoRepository = cityInfoRepository;

        }
        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {

            try
            {
                //   throw new Exception("Exception sample");
                //var city = CitiesDataStore.Current.Cities.FirstOrDefault(p => p.Id == cityId);
                if (!_cityInfoRepository.CityExists(cityId))
                {
                    _logger.LogInformation($"The city with {cityId} wasn't found when accessing point of interest");
                    return NotFound();
                }
                var pointsOfInterestForCity = _cityInfoRepository.GetPointsOfInterestForCity(cityId);
                var pointOfInterestForCityResults = new List<PointOfInterestDto>();
                foreach (var poi in pointsOfInterestForCity)
                {
                    pointOfInterestForCityResults.Add(new PointOfInterestDto()
                    {
                        Id = poi.Id,
                        Name = poi.Name,
                        Description = poi.Description
                    });
                }
                 return Ok(pointOfInterestForCityResults);

            }
                // if (city == null)
                // {
                //     _logger.LogInformation($"The city with {cityId} wasn't found when accessing point of interest");
                //     return NotFound();
                // }
                // return Ok(city.PointOfInterest);

            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting point of interest for {cityId}.", ex);
                return StatusCode(500, "A problem occured while handling your request");
            }


        }
    

    [HttpGet("{cityId}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
    public IActionResult GetPointsOfInterestById(int cityId, int id)
    {
        if(!_cityInfoRepository.CityExists(cityId))
        {
            return NotFound();
        }

        var pointOfInterest = _cityInfoRepository.GetPointOfInterestForCity(cityId,id);
        if(pointOfInterest== null)
        {
            return NotFound();
        }
        var pointOfInterestResult = new PointOfInterestDto()
        {
            Id=pointOfInterest.Id,
            Name = pointOfInterest.Name,
            Description=pointOfInterest.Description
        };

        return Ok(pointOfInterestResult);
        // var city = CitiesDataStore.Current.Cities.FirstOrDefault(p => p.Id == cityId);
        // if (city == null)
        // {
        //     return NotFound();
        // }

        // var pointOfInterest = city.PointOfInterest.FirstOrDefault(c => c.Id == id);
        // if (pointOfInterest == null)
        // {
        //     return NotFound();
        // }
        // return Ok(pointOfInterest);
    }

    [HttpPost("{cityId}/pointsofinterest")]
    public IActionResult CreatePointOfInterest(int cityId,
    [FromBody] PointOfInterestForCreationDto pointOfInterest)
    {
        if (pointOfInterest == null)
        {
            return BadRequest();
        }
        if (pointOfInterest.Description == pointOfInterest.Name)
        {
            ModelState.AddModelError("Description", "Enter a name value different from that of the name");
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }


        var city = CitiesDataStore.Current.Cities.FirstOrDefault(p => p.Id == cityId);
        if (city == null)
        {
            return NotFound();
        }

        //demo purpose - to be improved
        var maxPointofInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointOfInterest).Max(p => p.Id);

        var finalPointOfInterest = new PointOfInterestDto()
        {
            Id = ++maxPointofInterestId,
            Name = pointOfInterest.Name,
            Description = pointOfInterest.Description
        };
        city.PointOfInterest.Add(finalPointOfInterest);

        return CreatedAtRoute("GetPointOfInterest", new { cityId = cityId, id = finalPointOfInterest.Id }, finalPointOfInterest);

    }
    [HttpPut("{cityId}/pointsofinterest/{id}")]
    public IActionResult UpdatePointOfInterest(int cityId, int id, [FromBody]PointOfInterestForUpdateDto pointOfInterest)
    {
        if (pointOfInterest == null)
        {
            return BadRequest();
        }
        if (pointOfInterest.Description == pointOfInterest.Name)
        {
            ModelState.AddModelError("Description", "Enter a name value different from that of the name");
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(p => p.Id == cityId);
        if (city == null)
        {
            return NotFound();
        }
        var pointOfInterestFromStore = city.PointOfInterest.FirstOrDefault(b => b.Id == id);
        if (pointOfInterestFromStore == null)
        {
            return NotFound();
        }
        pointOfInterestFromStore.Name = pointOfInterest.Name;
        pointOfInterestFromStore.Description = pointOfInterest.Description;

        return NoContent();




    }
    [HttpPatch("{cityId}/pointsofinterest/{id}")]
    public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id, [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest();
        }
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(p => p.Id == cityId);
        if (city == null)
        {
            return NotFound();
        }

        var pointOfInterestFromStore = city.PointOfInterest.FirstOrDefault(b => b.Id == id);
        if (pointOfInterestFromStore == null)
        {
            return NotFound();
        }

        var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
        {
            Name = pointOfInterestFromStore.Name,
            Description = pointOfInterestFromStore.Description,
        };
        patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name)
        {
            ModelState.AddModelError("Description", "Enter a name value different from that of the name");
        }
        TryValidateModel(pointOfInterestToPatch);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
        pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

        return NoContent();

    }

    [HttpDelete("{cityId}/pointsofinterest/{id}")]
    public IActionResult DeletePointOfInterest(int cityId, int id)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(p => p.Id == cityId);
        if (city == null)
        {
            return NotFound();
        }

        var pointOfInterestFromStore = city.PointOfInterest.FirstOrDefault(b => b.Id == id);
        if (pointOfInterestFromStore == null)
        {
            return NotFound();
        }
        city.PointOfInterest.Remove(pointOfInterestFromStore);
        _mailService.Send("Point of interest deleted.", $"Point of interest{pointOfInterestFromStore.Name} with id{pointOfInterestFromStore.Id} was deleted");
        return NoContent();


    }


    }

}
