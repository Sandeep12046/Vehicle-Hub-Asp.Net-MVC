using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleDetails.Models;
using VehicleDetails.Models.RequiredModels.ViewModels;
using VehicleDetails.Repository;

namespace VehicleDetails.Helpers
{
    public class AllData
    {
        IVehicle VehicleDAL;
        IBrand BrandDAL;
        ICategory CategoryDAL;
        IReview IReviewDAL;

        public AllData()
        {
            this.VehicleDAL = new VehicleDAL(new VehicleDBEntities());
            this.BrandDAL = new BrandDAL(new VehicleDBEntities());
            this.CategoryDAL = new CategoryDAL(new VehicleDBEntities());
            this.IReviewDAL = new ReviewDAL(new VehicleDBEntities());
        }

        public  object AllDataInfo()
        {

            
            BrandCategories categoryCategories = new BrandCategories
            {
                vehiclesModel = VehicleDAL.GetAllVehicles().OrderByDescending(d=>d.VehicleID).Select(VehicleModel => new VehicleModel
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
                }).ToList(),
                Brands = BrandDAL.GetAllBrand().OrderByDescending(b=>b.BrandID).Select(BrandModel => new BrandModel
                {
                    BrandID = BrandModel.BrandID,
                    BrandName = BrandModel.BrandName,
                    Active = BrandModel.Active,
                    BrandCategoryID = BrandModel.BrandCategoryID,
                    ImageUrl=BrandModel.ImageUrl,
                }).ToList(),
                Categories = CategoryDAL.GetCategories().OrderByDescending(c=>c.CategoryID).Select(CategoryModel => new CategoryModel
                {
                    CategoryID = CategoryModel.CategoryID,
                    CategoryName = CategoryModel.CategoryName,
                    ImageUrl=CategoryModel.ImageUrl,
                }).ToList(),
            };

            return categoryCategories;
        }
    }
}