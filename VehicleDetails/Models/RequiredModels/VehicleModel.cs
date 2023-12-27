using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleDetails.Models.RequiredModels
{
    public class VehicleModel
    {
    
        public int VehicleId { get; set; }
        [Required(ErrorMessage = "Please enter the vehicle name")]
        [Display(Name = "Vehicle Name")]
        [RegularExpression(@"^[a-zA-Z\s]$",ErrorMessage ="Name should be string")]
        public string VehicleName { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid price")]
        [Display(Name = "Price")]
        [Required(ErrorMessage = "Please enter Price")]
        public Nullable<int> price { get; set; }
        [Display(Name = "Manufacture Date")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date")]
        public Nullable<System.DateTime> ManufactureDate { get; set; }
        [Required(ErrorMessage = "Please select availability status")]
        [Display(Name = "Availability Statu")]
        public string AvailabilityStatus { get; set; }

        public Nullable<int> Active { get; set; }
        [Display(Name = "Category")]
        public Nullable<int> VehicleCategoryID { get; set; }
        [Display(Name = "Brand")]
        public Nullable<int> VehicleBrandID { get; set; }

        public List<BrandCategorynames> BrandCategorynames { get; set; }


       
        
    }
}