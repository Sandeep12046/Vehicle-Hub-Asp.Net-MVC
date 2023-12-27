using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleDetails.Models;

namespace VehicleDetails.Repository
{
    internal interface ICategory
    {
        List<Category> GetCategories();
        List<Vehicle> GetVehiclesByCategories(int id);
    }
}
