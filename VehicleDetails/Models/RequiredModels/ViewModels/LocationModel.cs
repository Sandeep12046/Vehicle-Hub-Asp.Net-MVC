using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleDetails.Models.RequiredModels.ViewModels
{
    public class LocationModel
    {
        [Required(ErrorMessage = "Please Select Locations")]
        [Display(Name = "Location")]
        public int LocationID { get; set; }
        public string Locations { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}