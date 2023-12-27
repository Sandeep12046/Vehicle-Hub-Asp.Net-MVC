using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleDetails.Models;
using VehicleDetails.Models.RequiredModels;

namespace VehicleDetails.Repository
{
    internal interface IVehicle
    {
        void InsertNewVehicle(VehicleModel vehicle);
        void UpdateVehicle(Vehicle vehicle);
        Vehicle GetVehicleById(int id);
        IEnumerable<Vehicle> GetAllVehicles();
        void DeleteVehicle(int id);
        List<BrandCategorynames> GetBrandAndCategoryName();
        IEnumerable<Vehicle> searchData(string search);
    }
}
