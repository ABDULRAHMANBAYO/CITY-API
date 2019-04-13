using System;
using System.Collections.Generic;
using System.Linq;
using CityInfo.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API
{
    public static class CityInfoContextExtensions
    {
        public static void EnsureSeedDataForContext(this CityInfoContext context)
        {
            if (context.Cities.Any())
            {
                return;
            }

            //Init Seed Data

            var cities = new List<City>()
            {
                new City()
                {


                     Name="Lagos",
                     Description="Center of excellence",
                     PointOfInterest = new List<PointOfInterest>()
                     {
                         new PointOfInterest()
                         {

                             Name="Ikeja",
                             Description="Computer Village"
                         },
                         new PointOfInterest()
                         {

                             Name="Victoria Island",
                             Description="Home of the rich men"
                         }
                     }
                },
                new City()
                {

                    Name="Abeokuta",
                    Description="Rockcity",
                    PointOfInterest = new List<PointOfInterest>()
                     {
                         new PointOfInterest()
                         {

                             Name="Ayetoro",
                             Description="Jenifer's  Village"
                         },
                         new PointOfInterest()
                         {

                             Name="Itoku",
                             Description="Home of adire"
                         }
                     }
                },
                new City()
                {

                    Name="Ibadan",
                    Description="Center of amala dudu",
                    PointOfInterest = new List<PointOfInterest>()
                     {
                         new PointOfInterest()
                         {

                             Name="Challenge",
                             Description="Main garage"
                         },
                         new PointOfInterest()
                         {

                             Name="Jerico GRA",
                             Description="Estate for the rich kid"
                         }
                     }
                },

            };
            context.Cities.AddRange(cities);
            context.SaveChanges();

        }
    }
}