using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        IUser UserDAL;
        ILocation LocationDAL;
        public AllData()
        {
            this.VehicleDAL = new VehicleDAL(new VehicleDBEntities());
            this.BrandDAL = new BrandDAL(new VehicleDBEntities());
            this.CategoryDAL = new CategoryDAL(new VehicleDBEntities());
            this.IReviewDAL = new ReviewDAL(new VehicleDBEntities());
            this.UserDAL=new UserDAl(new VehicleDBEntities());
            this.LocationDAL=new LocationDal(new VehicleDBEntities());
        }

        public  object AllDataInfo()
        {


            BrandCategories categoryCategories = new BrandCategories
            {
                vehiclesModel = VehicleDAL.GetAllVehicles().OrderByDescending(d => d.VehicleID).Select(VehicleModel => new VehicleModel
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
                    FuelType = VehicleModel.FuelType,
                    Color = VehicleModel.Color,
                    Description = VehicleModel.Description,
                    Address = VehicleModel.Address,
                    RegistrationNumber = VehicleModel.RegistrationNumber,
                    Transmission = VehicleModel.Transmission,
                    Owner = VehicleModel.Owner,
                    VehicleType = VehicleModel.VehicleType,
                }).ToList(),
                Brands = BrandDAL.GetAllBrand().OrderByDescending(b => b.BrandID).Select(BrandModel => new BrandModel
                {
                    BrandID = BrandModel.BrandID,
                    BrandName = BrandModel.BrandName,
                    Active = BrandModel.Active,
                    BrandCategoryID = BrandModel.BrandCategoryID,
                    ImageUrl = BrandModel.ImageUrl,
                }).ToList(),
                Categories = CategoryDAL.GetCategories().OrderByDescending(c => c.CategoryID).Select(CategoryModel => new CategoryModel
                {
                    CategoryID = CategoryModel.CategoryID,
                    CategoryName = CategoryModel.CategoryName,
                    ImageUrl = CategoryModel.ImageUrl,
                }).ToList(),
                userModel = UserDAL.GetUsers().OrderByDescending(u => u.UserID).Select(users => new UserModel
                {
                    UserID = users.UserID,
                    FirstName = users.FirstName,
                    LastName = users.LastName,
                    Passwords = users.Passwords,
                    Email = users.Email,
                    UserType = users.UserType,
                    UserTypeName = users.UserTypeName,
                    Address = users.Address,
                    PhoneNumber = users.PhoneNumber,
                }).ToList(),
                //locationModel = LocationDAL.GetLocation().Select(LocationModel => new LocationModel
                //{
                //    Locations = LocationModel.Locations,
                //    LocationID = LocationModel.LocationID,
                //}).ToList(),
        };
      
            return categoryCategories;
        }


        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        public static string VerifyPassword(string hashedPassword, string inputPassword)
        {
            var hashedInput = HashPassword(inputPassword);

            if (hashedInput == hashedPassword)
            {
                Console.WriteLine("Password matched: " + inputPassword);
            }
            else
            {
                Console.WriteLine("Password does not match.");
            }
            return hashedInput;
        }
    }
}