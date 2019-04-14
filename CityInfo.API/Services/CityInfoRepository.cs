using System;
using System.Collections.Generic;
using System.Linq;
using CityInfo.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private CityInfoContext _context;
        private CityInfoRepository(CityInfoContext context)
        {
            _context = context;
        }
        public IEnumerable<City> GetCities()
        {
        return _context.Cities.OrderBy(c=>c.Name).ToList();
        }

        public City GetCity(int cityId,bool includePointsOfInterest)
        {
            if(includePointsOfInterest)
            
            {
                return _context.Cities.Include(p=>p.PointOfInterest).Where(c=>c.Id==cityId).FirstOrDefault();
            }
            return _context.Cities.Where(p=>p.Id==cityId).FirstOrDefault();
        }

        public PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId)
        {
           return _context.PointOfInterest.Where(c=>c.CityId==cityId && c.Id==pointOfInterestId).FirstOrDefault();
        }

        public IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId)
        {
            return _context.PointOfInterest.Where(c=>c.CityId==cityId);
        }
    }
}