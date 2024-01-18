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

        public UserModel user { get; set; }
        public LocationModel Location { get; set; }
        public FavoriteModel favorite { get; set; }
        public List<UserModel> userModel { get; set; }
        public List<BrandModel> Brands { get; set; }
        public List<CategoryModel> Categories { get; set;}
        public List<ReviewModel> reviews { get; set; }  
        public List<VehicleModel> vehiclesModel { get; set; }
        public List<LocationModel> locationModel { get; set; }

        public List<FavoriteModel> favoriteModels { get; set;}

       
    }
}