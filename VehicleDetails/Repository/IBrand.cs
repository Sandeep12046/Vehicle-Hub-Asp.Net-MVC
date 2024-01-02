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
    internal interface IBrand
    {
        void InsertBrandData(BrandCategories data);
        List<Brand> GetAllBrand();

        Brand GetBrandById(int id);

   
    }
}
