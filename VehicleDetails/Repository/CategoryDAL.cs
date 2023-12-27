using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VehicleDetails.Models;

namespace VehicleDetails.Repository
{
    public class CategoryDAL : ICategory
    {
        private VehicleDBEntities VehicleDBEntitie;
        //internal DbSet<Vehicle> Vehicles;
        public CategoryDAL(VehicleDBEntities vehicleDBEntities) {
        this.VehicleDBEntitie = vehicleDBEntities;
            //this.Vehicles = vehicleDBEntities.Set<Vehicle>();
        }
        public List<Category> GetCategories()
        {
            return VehicleDBEntitie.Categories.ToList();
        }

        public List<Vehicle> GetVehiclesByCategories(int id)
        {
            return VehicleDBEntitie.Vehicles.Where(temp=>temp.VehicleCategoryID==id).ToList();  
        }
    }
}