using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleDetails.Models;
using VehicleDetails.Models.RequiredModels;

namespace VehicleDetails.Repository
{
    public class VehicleDAL : IVehicle
    {
        VehicleDBEntities VehicleDBEntitie;
        public VehicleDAL(VehicleDBEntities VehicleDBEntities) { 
        this.VehicleDBEntitie = VehicleDBEntities;
        }
        public void DeleteVehicle(int id)
        {
            Vehicle data= VehicleDBEntitie.Vehicles.Where(temp=>temp.VehicleID==id).FirstOrDefault();
            VehicleDBEntitie.Vehicles.Remove(data);
            VehicleDBEntitie.SaveChanges();
        }

        public IEnumerable<Vehicle> GetAllVehicles()
        {
            return VehicleDBEntitie.Vehicles.ToList();
        }

        public Vehicle GetVehicleById(int id)
        {
           Vehicle singleData=VehicleDBEntitie.Vehicles.Where(Vid=>Vid.VehicleID==id).FirstOrDefault(); 
            return singleData;
        }

        public void InsertNewVehicle(VehicleModel vehicle)
        {
            Vehicle vehicles=new Vehicle();
            vehicles.VehicleName = vehicle.VehicleName;
            vehicles.price=vehicle.price;
            vehicles.ManufactureDate=vehicle.ManufactureDate;
            vehicles.AvailabilityStatus=vehicle.AvailabilityStatus;
            vehicles.Active=vehicle.AvailabilityStatus=="Available"?1:0;
            vehicles.VehicleBrandID=vehicle.VehicleBrandID;
            vehicles.VehicleCategoryID=vehicle.VehicleCategoryID;
            VehicleDBEntitie.Vehicles.Add(vehicles);
            VehicleDBEntitie.SaveChanges();
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            Vehicle vehicle1=VehicleDBEntitie.Vehicles.Where(temp=>temp.VehicleID==vehicle.VehicleID).FirstOrDefault();
            vehicle1.VehicleName = vehicle.VehicleName;
            vehicle1.price = vehicle.price;
            vehicle1.ManufactureDate = vehicle.ManufactureDate;
            vehicle1.AvailabilityStatus = vehicle.AvailabilityStatus;
            vehicle1.Active = vehicle.Active;
            vehicle1.VehicleBrandID = vehicle.VehicleBrandID;
            vehicle1.VehicleCategoryID = vehicle.VehicleCategoryID;

            VehicleDBEntitie.SaveChanges();
        }

        public List<BrandCategorynames> GetBrandAndCategoryName()
        {
            var data = from category in VehicleDBEntitie.Categories
                       join brand in VehicleDBEntitie.Brands on category.CategoryID equals brand.BrandCategoryID
                       select new BrandCategorynames
                       {
                           BrandName = brand.BrandName,
                           BrandID = brand.BrandID,
                           BrandCategoryID = (int)brand.BrandCategoryID,
                           CategoryName = category.CategoryName,
                           CategoryID = category.CategoryID

                       };
            return data.ToList();
        }

        public IEnumerable<Vehicle> searchData(string search)
        {
            return VehicleDBEntitie.Vehicles.Where(temp => temp.VehicleName.Contains(search)).ToList();
        }
    }
}