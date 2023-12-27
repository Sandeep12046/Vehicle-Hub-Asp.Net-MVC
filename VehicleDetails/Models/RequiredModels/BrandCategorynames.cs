using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleDetails.Models.RequiredModels
{
    public class BrandCategorynames
    {
        [Required(ErrorMessage = "Please select Brand")]
        [Display(Name = "Brand")]
        public int BrandID { get; set; }
        [Required(ErrorMessage = "Please select Category")]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }
       
        public string BrandName { get; set; }
    
        public string CategoryName { get; set; }
    
        public int BrandCategoryID { get; set; }



    }
}