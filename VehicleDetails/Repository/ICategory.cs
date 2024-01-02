using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleDetails.Models;
using VehicleDetails.Models.RequiredModels;
using VehicleDetails.Models.RequiredModels.ViewModels;

namespace VehicleDetails.Repository
{
    internal interface ICategory
    {
        //void CreateCategory(CategoryModel category);
        void CreteCategoryBrand(BrandCategories category);
        List<Category> GetCategories();
        List<Vehicle> GetVehiclesByCategories(int id);
    }
}
