using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleDetails.Models.RequiredModels.ViewModels;

namespace VehicleDetails.Repository
{
    internal interface IBuyVehicle
    {
        List<VehicleModel> getBudgets();
    }
}
