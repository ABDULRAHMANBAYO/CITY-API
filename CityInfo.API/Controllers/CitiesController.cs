using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CityInfo.API.Services;
using CityInfo.API.Entities;
using CityInfo.API.Models;
using AutoMapper;

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

            var results = Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities);
             
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
                    var cityResult = Mapper.Map<CityDto>(city);
                    return Ok(cityResult);
                }
                var CityWithoutPointsOfInterest = Mapper.Map<CityWithoutPointsOfInterestDto>(city);
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


