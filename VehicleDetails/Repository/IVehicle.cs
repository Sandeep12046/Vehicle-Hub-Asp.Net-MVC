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
        void InsertNewVehicle(BrandCategories vehicle,int userID);
        void UpdateVehicle(BrandCategories vehicle);
        Vehicle GetVehicleById(int id);


        List<Vehicle> GetAllVehicles();
        List<Vehicle> GetAllVehicleByBrand(int id);
        void DeleteVehicle(int id,int userID);
        IEnumerable<Vehicle> searchData(int id, string search);
        List<VehicleModel> GetAllVehicleSearch(int id,string search);

        List<VehicleModel> GetAllVehicleDetails();
        List<BrandModel> GetAllBrandDetails();
        List<CategoryModel> GetAllCategoryDetails();


        List<VehicleModel> GellAllVehiclesMain();


        //List<VehicleModel> CompareVehicleDetails();
    }
}
