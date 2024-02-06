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
        ICategory CategoryDAL;
        IBrand BrandDAL;
        IReview ReviewDAL;
        IVehicle VehicleDAL;
        public BuyVehicleDAL(VehicleDBEntities vehicleDBEntities) {
            this.Entities = vehicleDBEntities;
            this.CategoryDAL = new CategoryDAL(new VehicleDBEntities());
            this.BrandDAL=new BrandDAL(new VehicleDBEntities());
            this.ReviewDAL=new ReviewDAL(new VehicleDBEntities());
            this.VehicleDAL=new VehicleDAL(new VehicleDBEntities());
        }
        public BrandCategories getvehicleInfo()
        {
            BrandCategories vehicle = new BrandCategories
            {
                CategoriesList = CategoryDAL.GetCategories().Select(category => new CategoryModel
                {

                    CategoryName = category.CategoryName,
                    BrandsList = BrandDAL.GetAllBrand().Select(brand => new BrandModel
                    {

                        BrandName = brand.BrandName,
                        vehiclesModelList = VehicleDAL.GetAllVehicles().Select(vehicles => new VehicleModel
                        {

                            Owner = vehicles.Owner,
                            AvailabilityStatus = vehicles.AvailabilityStatus,
                            ManufactureDate = vehicles.ManufactureDate,
                            price = vehicles.price,
                            FuelType = vehicles.FuelType,
                            Transmission = vehicles.Transmission,
                            Color = vehicles.Color,
                            ReviewsList = Entities.Reviews.Select(review => new ReviewModel
                            {
                                Comment = review.Comment,
                            }).Distinct().ToList(),
                        }).Distinct().ToList(),
                    }).Distinct().ToList(),
                }).Distinct().ToList(),

            };
           
            return vehicle;
        }



    }
}