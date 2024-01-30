using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleDetails.Models;
using VehicleDetails.Models.RequiredModels.ViewModels;

namespace VehicleDetails.Repository
{
    public class BuyVehicleDAL : IBuyVehicle
    {
        VehicleDBEntities Entities;
        public BuyVehicleDAL(VehicleDBEntities vehicleDBEntities) {
            this.Entities = vehicleDBEntities;
        }
        public List<VehicleModel> getBudgets()
        {
            BrandCategories vehicle = new BrandCategories();
            vehicle.vehiclesModel = new List<VehicleModel>();


            var data = (from vehicles in Entities.Vehicles
                        group vehicle by vehicles.price into budget
                        orderby budget.Key
                        select new VehicleModel
                        {
                            price = budget.Key,
                        }).OrderBy(p => p.price).ToList();

            return data;
        }
    }
}