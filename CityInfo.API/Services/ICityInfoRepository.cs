using System;
using System.Collections.Generic;
using System.Linq;
using CityInfo.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        bool CityExists(int cityId);
        
        IEnumerable<City> GetCities();

        City GetCity(int cityId,bool includePointsOfInterest);

        IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId);

        PointOfInterest GetPointOfInterestForCity(int cityId,int pointOfInterestId);

        void AddPointOfInterestForCity(int cityId,PointOfInterest pointOfInterest);

        void DeletePointofInterest(PointOfInterest pointOfInterest);

        bool Save();
        
    } 
   
}