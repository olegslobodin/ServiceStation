using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceStation.Models
{
    public class ClientMetadata
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public System.DateTime DateOfBirth { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }
    }

    [MetadataType(typeof(ClientMetadata))]
    public partial class Client
    {
        public IEnumerable<Order> Orders => Cars.SelectMany(car => car.Orders);
    }
}