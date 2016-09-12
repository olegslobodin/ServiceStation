using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceStation.Models
{
    public class OrderMetadata
    {
        [Required]
        [Display(Name = "Car VIN")]
        public long CarId { get; set; }
        
        [Required]
        public System.DateTime Date { get; set; }

        [Required]
        [Range(0, 10000)]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Status")]
        public long StatusId { get; set; }
    }

    [MetadataType(typeof(OrderMetadata))]
    public partial class Order
    {
    }
}