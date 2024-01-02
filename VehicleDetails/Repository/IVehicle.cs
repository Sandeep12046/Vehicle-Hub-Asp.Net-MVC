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
    internal interface IVehicle
    {
        void InsertNewVehicle(BrandCategories vehicle);
        void UpdateVehicle(BrandCategories vehicle);
        Vehicle GetVehicleById(int id);
        List<Vehicle> GetAllVehicles();
        List<Vehicle> GetAllVehicleByBrand(int id);
        void DeleteVehicle(int id);
        //List<BrandCategoryData> GetBrandAndCategoryName();
        IEnumerable<Vehicle> searchData(string search);
        List<VehicleModel> GetAllVehicleSearch(string search);

       
    }
}
