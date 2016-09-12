using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceStation.Models
{
    public class CarMetadata
    {
        [Required]
        public long ClientId { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string VIN { get; set; }
    }

    [MetadataType(typeof(CarMetadata))]
    public partial class Car
    {
    }
}