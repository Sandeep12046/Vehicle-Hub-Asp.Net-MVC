using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleDetails.Models.RequiredModels.ViewModels
{
    public class BrandModel
    {
        [Required(ErrorMessage = "Please select Brand")]
        [Display(Name = "Brand")]
        public int BrandID { get; set; }
        [Required(ErrorMessage = "Please Enter Brand Name")]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }
        public int? BrandCategoryID { get; set; }
        public int? Active { get; set; }

        [Required(ErrorMessage = "Please Add Brand Image Url")]
        [Display(Name = "Brand Image")]
        public string ImageUrl { get; set; }

        public virtual Category Category { get; set; }
        public List<VehicleModel> vehiclesModelList { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}