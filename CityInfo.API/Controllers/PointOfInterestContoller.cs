using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using CityInfo.API.Services;
using AutoMapper;

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
                if (!_cityInfoRepository.CityExists(cityId))
                {
                    _logger.LogInformation($"The city with {cityId} wasn't found when accessing point of interest");
                    return NotFound();
                }
                var pointsOfInterestForCity = _cityInfoRepository.GetPointsOfInterestForCity(cityId);
                var pointOfInterestForCityResults = Mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity);
                return Ok(pointOfInterestForCityResults);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting point of interest for {cityId}.", ex);
                return StatusCode(500, "A problem occured while handling your request");
            }
        }

        [HttpGet("{cityId}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointsOfInterestById(int cityId, int id)
        {
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }
            var pointOfInterest = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterest == null)
            {
                return NotFound();
            }
            var pointOfInterestResult = Mapper.Map<PointOfInterestDto>(pointOfInterest);
            return Ok(pointOfInterestResult);
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
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }
            var finalPointOfInterest = Mapper.Map<Entities.PointOfInterest>(pointOfInterest);
            _cityInfoRepository.AddPointOfInterestForCity(cityId, finalPointOfInterest);
            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "Sorry,error ecountered while handling your request.");
            }
            var createPointOfInterestToReturn = Mapper.Map<Models.PointOfInterestDto>(finalPointOfInterest);
            return CreatedAtRoute("GetPointOfInterest", new { cityId = cityId, id = createPointOfInterestToReturn.Id }, createPointOfInterestToReturn);

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
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }
            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }
            Mapper.Map(pointOfInterest, pointOfInterestEntity);
            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "Sorry,error ecountered while handling your request.");
            }
            return NoContent();
        }

        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id, [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }
            var pointOfInterestToPatch = Mapper.Map<PointOfInterestForUpdateDto>(pointOfInterestEntity);
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
           Mapper.Map(pointOfInterestToPatch,pointOfInterestEntity);

           if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "Sorry,error ecountered while handling your request.");
            }
            return NoContent();
        }

        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _cityInfoRepository.DeletePointofInterest(pointOfInterestEntity);
            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "Sorry,error ecountered while handling your request.");
            }
            _mailService.Send("Point of interest deleted.", $"Point of interest{pointOfInterestEntity.Name} with id{pointOfInterestEntity.Id} was deleted");
            return NoContent();


        }


    }

}
