using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleDetails.Models;
using VehicleDetails.Models.RequiredModels;
using VehicleDetails.Models.RequiredModels.ViewModels;

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

        public List<Vehicle> GetAllVehicles()
        {
            return VehicleDBEntitie.Vehicles.ToList();
        }

        public Vehicle GetVehicleById(int id)
        {
           Vehicle singleData=VehicleDBEntitie.Vehicles.Where(Vid=>Vid.VehicleID==id).FirstOrDefault(); 
            return singleData;
        }

        public void InsertNewVehicle(BrandCategories vehicle)
        {
            Vehicle vehicles=new Vehicle();
            vehicles.VehicleName = vehicle.vehicles.VehicleName;
            vehicles.price=vehicle.vehicles.price;
            vehicles.ManufactureDate=vehicle.vehicles.ManufactureDate;
            vehicles.AvailabilityStatus=vehicle.vehicles.AvailabilityStatus;
            vehicles.Active=vehicle.vehicles.AvailabilityStatus=="Available"?1:0;
            vehicles.VehicleBrandID=vehicle.vehicles.VehicleBrandID;
            vehicles.VehicleCategoryID=vehicle.vehicles.VehicleCategoryID;
            vehicles.FuelType=vehicle.vehicles.FuelType;
            vehicles.ImageUrl=vehicle.vehicles.ImageUrl;
            vehicles.Mileage=vehicle.vehicles.Mileage;
            vehicles.Status = vehicle.vehicles.Active == 1 ? 1 : 0;
            VehicleDBEntitie.Vehicles.Add(vehicles);
            VehicleDBEntitie.SaveChanges();
        }

        public void UpdateVehicle(BrandCategories vehicle)
        {
            Vehicle vehicle1 = VehicleDBEntitie.Vehicles
            .Where(temp => temp.VehicleID == vehicle.vehicles.VehicleID)
            .FirstOrDefault();

            if (vehicle1 != null)
            {
                vehicle1.VehicleID = vehicle.vehicles.VehicleID;
                vehicle1.VehicleName = vehicle.vehicles.VehicleName;
                vehicle1.price = vehicle.vehicles.price;
                vehicle1.ManufactureDate = vehicle.vehicles.ManufactureDate;
                vehicle1.AvailabilityStatus = vehicle.vehicles.AvailabilityStatus;
                vehicle1.Active = vehicle.vehicles.AvailabilityStatus == "Available" ? 1 : 0;
                vehicle1.VehicleBrandID = vehicle.vehicles.VehicleBrandID;
                vehicle1.VehicleCategoryID = vehicle.vehicles.VehicleCategoryID;
                vehicle1.FuelType = vehicle.vehicles.FuelType;
                vehicle1.Mileage = vehicle.vehicles.Mileage;
                vehicle1.ImageUrl = vehicle.vehicles.ImageUrl;
                vehicle1.Status = vehicle.vehicles.Active == 1 ? 1 : 0;
                VehicleDBEntitie.SaveChanges();
            }
        }



        public IEnumerable<Vehicle> searchData(string search)
        {
            return VehicleDBEntitie.Vehicles.Where(temp => temp.VehicleName.Contains(search)).ToList();
        }

        public List<VehicleModel> GetAllVehicleSearch(string search)
        {
            List<VehicleModel> vehicleModels=null;
           
            if (!string.IsNullOrEmpty(search))
            {

                vehicleModels = new List<VehicleModel>();
                vehicleModels = searchData(search).Select(VehicleModel => new VehicleModel
                {
                    VehicleName = VehicleModel.VehicleName,
                    VehicleBrandID = VehicleModel.VehicleBrandID,
                    VehicleCategoryID = VehicleModel.VehicleCategoryID,
                    VehicleID = VehicleModel.VehicleID,
                    Active = VehicleModel.Active,
                    AvailabilityStatus = VehicleModel.AvailabilityStatus,
                    price = VehicleModel.price,
                    ManufactureDate = VehicleModel.ManufactureDate,
                    Mileage = VehicleModel.Mileage,
                    ImageUrl = VehicleModel.ImageUrl,
                    Status = VehicleModel.Status,
                }).ToList();
            }
            return vehicleModels;
        }

        public List<Vehicle> GetAllVehicleByBrand(int id)
        {

            List<Vehicle> data = VehicleDBEntitie.Vehicles.Where(ids => ids.VehicleBrandID == id).ToList();

            return data;
        }

       
    }
}