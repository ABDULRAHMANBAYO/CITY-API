using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
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
                     Description="Center of excellence"
                },
                new CityDto()
                {
                    Id=2,
                    Name="Abeokuta",
                    Description="Rockcity"
                },
                new CityDto()
                {
                    Id=3,
                    Name="Ibadan",
                    Description="Center of amala dudu"
                },

            };
        }
    }
}