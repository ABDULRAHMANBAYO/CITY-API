using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
         public static CitiesDataStore Current{get;} = new CitiesDataStore();
        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            //init dummy data

            Cities = new List<CityDto>()
            {
                new CityDto()
                {

                     Id=1,
                     Name="Lagos",
                     Description="Center of excellence",
                     PointOfInterest = new List<PointOfInterestDto>()
                     {
                         new PointOfInterestDto()
                         {
                             Id=1,
                             Name="Ikeja",
                             Description="Computer Village"
                         },
                         new PointOfInterestDto()
                         {
                             Id=2,
                             Name="Victoria Island",
                             Description="Home of the rich men"
                         }
                     }
                },
                new CityDto()
                {
                    Id=2,
                    Name="Abeokuta",
                    Description="Rockcity",
                    PointOfInterest = new List<PointOfInterestDto>()
                     {
                         new PointOfInterestDto()
                         {
                             Id=1,
                             Name="Ayetoro",
                             Description="Jenifer's  Village"
                         },
                         new PointOfInterestDto()
                         {
                             Id=2,
                             Name="Itoku",
                             Description="Home of adire"
                         }
                     }
                },
                new CityDto()
                {
                    Id=3,
                    Name="Ibadan",
                    Description="Center of amala dudu",
                    PointOfInterest = new List<PointOfInterestDto>()
                     {
                         new PointOfInterestDto()
                         {
                             Id=1,
                             Name="Challenge",
                             Description="Main garage"
                         }, 
                         new PointOfInterestDto()
                         {
                             Id=2,
                             Name="Jerico GRA",
                             Description="Estate for the rich kid"
                         }
                     }
                },

            };
        }
    }
}