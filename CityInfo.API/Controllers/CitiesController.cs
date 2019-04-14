using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CityInfo.API.Services;
using CityInfo.API.Entities;
using CityInfo.API.Models;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private ICityInfoRepository _cityInfoRepository;
        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;

        }

        [HttpGet()]
        public IActionResult GetCities()
        {
            // return Ok(CitiesDataStore.Current.Cities);
            var cityEntities = _cityInfoRepository.GetCities();

            var results = new List<CityWithoutPointsOfInterestDto>();

            foreach (var cityEntity in cityEntities)
            {
                results.Add(new CityWithoutPointsOfInterestDto
                {
                    Id = cityEntity.Id,
                    Name = cityEntity.Name,
                    Description = cityEntity.Description

                });
            }
            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInterest = false)
        {
            var city = _cityInfoRepository.GetCity(id, includePointsOfInterest);
            {
                if (city == null)
                {
                    return NotFound();
                }
                if(includePointsOfInterest)
                {
                    var cityResult = new CityDto()
                    {
                        Id= city.Id,
                        Name =city.Name,
                        Description=city.Description,
                        
                    };
                    foreach(var poi in city.PointOfInterest)
                    {
                        cityResult.PointOfInterest.Add(
                            new PointOfInterestDto()
                            {
                                Id = poi.Id,
                                Name =poi.Name,
                                Description =poi.Description

                            }
                        );

                    }
                    return Ok(cityResult);
                }
                var CityWithoutPointsOfInterest = new CityWithoutPointsOfInterestDto()
                {
                    Id= city.Id,
                    Name=city.Name,
                    Description=city.Description
                };
                return Ok(CityWithoutPointsOfInterest);
            }

            //find city by Id
            // var cityToReturn=CitiesDataStore.Current.Cities.FirstOrDefault(p => p.Id == id);
            // if (cityToReturn==null)
            // {
            //     return NotFound();
            // }
            // return Ok(cityToReturn);

        }
    }
}


