using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CityInfo.API.Entities
{
    [Table("PointOfInterest",Schema="dbo")]
    public class PointOfInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
       public string Description { get; set; }

        public City City { get; set; }

        public int CityId { get; set; }


    }
}