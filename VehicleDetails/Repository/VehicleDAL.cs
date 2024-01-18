using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleDetails.Models;
using VehicleDetails.Models.RequiredModels;
using VehicleDetails.Models.RequiredModels.ViewModels;
using VehicleDetails.Repository;

namespace VehicleDetails.Repository
{
    public class VehicleDAL : IVehicle
    {
        VehicleDBEntities VehicleDBEntitie;
        IBrand BrandDAL;
        ICategory CategoryDAL;
        public VehicleDAL(VehicleDBEntities VehicleDBEntities) { 
        this.VehicleDBEntitie = VehicleDBEntities;
            this.BrandDAL = new BrandDAL(new VehicleDBEntities());
            this.CategoryDAL=new CategoryDAL(new VehicleDBEntities());
        }
        public void DeleteVehicle(int id, int userID)
        {
            Favority Favdata = VehicleDBEntitie.Favorities.Where(temp => temp.VehicleID == id && temp.UserID == userID).FirstOrDefault();
            Review RevData = VehicleDBEntitie.Reviews.Where(temp => temp.VehicleID == id && temp.UserID == userID).FirstOrDefault();
            Vehicle data = VehicleDBEntitie.Vehicles.Where(temp => temp.VehicleID == id && temp.VehicleUserID == userID).FirstOrDefault();

            if (data != null)
            {
                if (RevData != null)
                {
                    VehicleDBEntitie.Reviews.Remove(RevData);
                    VehicleDBEntitie.SaveChanges();
                }

                if (Favdata != null)
                {
                    VehicleDBEntitie.Favorities.Remove(Favdata);
                    VehicleDBEntitie.SaveChanges();
                }

                // Finally, delete the vehicle
                VehicleDBEntitie.Vehicles.Remove(data);
                VehicleDBEntitie.SaveChanges();
            }

            //VehicleDBEntitie.Vehicles.Remove(data);
            //VehicleDBEntitie.SaveChanges();


            //VehicleDBEntitie.SaveChanges();
            //VehicleDBEntitie.Vehicles.SqlQuery("exec DeleteVehicleByUserID @id, @userID",
            //    new SqlParameter("@id",id),
            //     new SqlParameter("@userID",userID)
            //).FirstOrDefault();
        }

        public List<Vehicle> GetAllVehicles()
        {
            return VehicleDBEntitie.Vehicles.Where(active=>active.Active!=0).ToList();
        }

        public List<VehicleModel> GellAllVehiclesMain()
        {

            List<VehicleModel> allVehicleData = VehicleDBEntitie.Vehicles.Select(Vehicles => new VehicleModel
            {
                VehicleID = Vehicles.VehicleID,
                VehicleName = Vehicles.VehicleName,
                VehicleBrandID = Vehicles.VehicleBrandID,
                VehicleCategoryID = Vehicles.VehicleCategoryID,
                Active = Vehicles.Active,
                AvailabilityStatus = Vehicles.AvailabilityStatus,
                price = Vehicles.price,
                Status = Vehicles.Status,
                ManufactureDate = Vehicles.ManufactureDate,
                Mileage = Vehicles.Mileage,
                FuelType = Vehicles.FuelType,
                ImageUrl = Vehicles.ImageUrl,
                Owner = Vehicles.Owner,
                Color = Vehicles.Color,
                RegistrationNumber = Vehicles.RegistrationNumber,
                Transmission = Vehicles.Transmission,
                Address = Vehicles.Address,
                Description = Vehicles.Description,
                VehicleType = Vehicles.VehicleType,
            }).ToList();

            return allVehicleData;
        }

        public Vehicle GetVehicleById(int id)
        {
           Vehicle singleData=VehicleDBEntitie.Vehicles.Where(Vid=>Vid.VehicleID==id).FirstOrDefault(); 
            return singleData;
        }

        public void InsertNewVehicle(BrandCategories vehicle, int userID)
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
            vehicles.Status = 1;
            vehicles.VehicleUserID = userID;
            vehicles.Owner=vehicle.vehicles.Owner;
            vehicles.Color=vehicle.vehicles.Color;
            vehicles.RegistrationNumber=vehicle.vehicles.RegistrationNumber;
            vehicles.Transmission=vehicle.vehicles.Transmission;
            vehicles.Address = vehicle.vehicles.Address;
            vehicles.Description=vehicle.vehicles.Description;
            vehicles.VehicleType=userID==1 ? 1 : 2;
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
                vehicle1.Owner = vehicle.vehicles.Owner;
                vehicle1.Color = vehicle.vehicles.Color;
                vehicle1.RegistrationNumber = vehicle.vehicles.RegistrationNumber;
                vehicle1.Transmission = vehicle.vehicles.Transmission;
                vehicle1.Address = vehicle.vehicles.Address;
                vehicle1.Description = vehicle.vehicles.Description;
                VehicleDBEntitie.SaveChanges();
            }
        }



        public IEnumerable<Vehicle> searchData(int id,string search)
        {
            if (id == 0)
            {
                return VehicleDBEntitie.Vehicles.Where(temp => temp.VehicleName.Contains(search) && temp.Active != 0).ToList();
            }
            else
            {
                return VehicleDBEntitie.Vehicles.Where(temp => temp.VehicleName.Contains(search) && temp.Active != 0 && temp.VehicleType == id).ToList();
            }
            
        }

        public List<VehicleModel> GetAllVehicleSearch(int id,string search)
        {
            List<VehicleModel> vehicleModels=null;
           
            if (!string.IsNullOrEmpty(search) || id!=null)
            {

                vehicleModels = new List<VehicleModel>();
                vehicleModels = searchData(id,search).Select(VehicleModel => new VehicleModel
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
                    Owner = VehicleModel.Owner,
                    Color = VehicleModel.Color,
                    RegistrationNumber = VehicleModel.RegistrationNumber,
                    Transmission = VehicleModel.Transmission,
                    Address = VehicleModel.Address,
                    Description = VehicleModel.Description,
                    VehicleType= VehicleModel.VehicleType,
                }).Where(active=>active.Active!=0).ToList();
            }
            return vehicleModels;
        }

        public List<Vehicle> GetAllVehicleByBrand(int id)
        {

            List<Vehicle> data = VehicleDBEntitie.Vehicles.Where(ids => ids.VehicleBrandID == id && ids.Active!=0).ToList();

            return data;
        }


        public List<VehicleModel> GetAllVehicleDetails()
        {
            List<VehicleModel> allVehicleData = VehicleDBEntitie.Vehicles.Select(Vehicles => new VehicleModel
            {
                VehicleID = Vehicles.VehicleID,
                VehicleName = Vehicles.VehicleName,
                VehicleBrandID = Vehicles.VehicleBrandID,
                VehicleCategoryID = Vehicles.VehicleCategoryID,
                Active = Vehicles.Active,
                AvailabilityStatus = Vehicles.AvailabilityStatus,
                price = Vehicles.price,
                Status = Vehicles.Status,
                ManufactureDate = Vehicles.ManufactureDate,
                Mileage = Vehicles.Mileage,
                FuelType = Vehicles.FuelType,
                ImageUrl = Vehicles.ImageUrl,
                Owner = Vehicles.Owner,
                Color = Vehicles.Color,
                RegistrationNumber = Vehicles.RegistrationNumber,
                Transmission = Vehicles.Transmission,
                Address = Vehicles.Address,
                Description = Vehicles.Description,
                VehicleType = Vehicles.VehicleType,
            }).Where(active => active.Active!=0).ToList();
               
            
            return allVehicleData.ToList();
        }

        public List<BrandModel> GetAllBrandDetails()
        {
            List<BrandModel> allBrandData = VehicleDBEntitie.Brands.Select(Brands => new BrandModel
            {
                BrandID = Brands.BrandID,
                BrandName = Brands.BrandName,
                Active = Brands.Active,
                BrandCategoryID = Brands.BrandCategoryID,
                ImageUrl = Brands.ImageUrl,
            }).ToList();


            return allBrandData.ToList();
        }
        public List<CategoryModel> GetAllCategoryDetails()
        {
            List<CategoryModel> allCategoryData = VehicleDBEntitie.Categories.Select(Category => new CategoryModel
            {
                CategoryID = Category.CategoryID,
                CategoryName = Category.CategoryName,
                ImageUrl = Category.ImageUrl,
            }).ToList();


            return allCategoryData.ToList();
        }

       
    }
}




