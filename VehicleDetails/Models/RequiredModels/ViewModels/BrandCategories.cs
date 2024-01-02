using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleDetails.Models.RequiredModels.ViewModels
{
    public class BrandCategories
    {
        public VehicleModel vehicles { get; set; }
        public CategoryModel category { get; set; }
        public BrandModel brand { get; set; }
        public ReviewModel reviewModel { get; set; }
        public List<BrandModel> Brands { get; set; }
        public List<CategoryModel> Categories { get; set;}
        public List<ReviewModel> reviews { get; set; }  
        public List<VehicleModel> vehiclesModel { get; set; }

      
    }
}