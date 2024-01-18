using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleDetails.Helpers;
using VehicleDetails.Models;
using VehicleDetails.Models.RequiredModels;
using VehicleDetails.Models.RequiredModels.ViewModels;
using VehicleDetails.Repository;

namespace VehicleDetails.Controllers
{
    //[RoutePrefix("vehicleDetails")]
    public class HomeController : Controller
    {
        IVehicle VehicleDAL;
        IBrand BrandDAL;
        ICategory CategoryDAL;
        IReview IReviewDAL;
        IUser userDAL;
        AllData allData;
        public HomeController()
        {
            this.VehicleDAL = new VehicleDAL(new VehicleDBEntities());
            this.BrandDAL=new BrandDAL(new VehicleDBEntities());
            this.CategoryDAL=new CategoryDAL(new VehicleDBEntities());
            this.IReviewDAL=new ReviewDAL(new VehicleDBEntities());
            this.userDAL=new UserDAl(new VehicleDBEntities());
            this.allData = new AllData();
        }
   
        //[Route("home")]
        public ActionResult Index()
        {
                var data = allData.AllDataInfo();
                return View(data);
           
        }

        [HttpGet]
        //[Route("create")]
        public ActionResult Create()
        {


            BrandCategories category = new BrandCategories
            {
                Brands = BrandDAL.GetAllBrand().Select(BrandModel => new BrandModel
                {
                    BrandID = BrandModel.BrandID,
                    BrandName = BrandModel.BrandName,
                    BrandCategoryID = BrandModel.BrandCategoryID,
                    ImageUrl = BrandModel.ImageUrl,
                }).ToList(),
                Categories = CategoryDAL.GetCategories().Select(CategoryModel=>new CategoryModel
                {
                    CategoryID = CategoryModel.CategoryID,
                    CategoryName = CategoryModel.CategoryName,
                    ImageUrl=CategoryModel.ImageUrl,
                }).ToList(),
                
            };

            return View(category);
        }
        [HttpPost]
        //[Route("create")]
        public ActionResult Create(BrandCategories newVehicle)
        {
            if (ModelState.IsValid)
            {
                int userID = Convert.ToInt32(Session["UserID"]);
                VehicleDAL.InsertNewVehicle(newVehicle, userID);
                return RedirectToAction("Index");
            }
          return RedirectToAction("create");
        }

        public ActionResult Details(int id)
        {
            BrandCategories model = new BrandCategories();
            model.vehicles = new VehicleModel();
            model.brand=new BrandModel();
            model.reviews=new List<ReviewModel>();
            model.user= new UserModel();
            Vehicle data=VehicleDAL.GetVehicleById(id);
            UserModel usersData = userDAL.getUserByVehicleID(id);
            List<Review> reviews = IReviewDAL.getVehicleReviewById(id);
            List<Brand> brands = BrandDAL.GetAllBrand();
            model.vehicles.VehicleID = data.VehicleID;
            model.vehicles.price = data.price;
            model.vehicles.VehicleName = data.VehicleName;
            model.vehicles.AvailabilityStatus = data.AvailabilityStatus;
            model.vehicles.ManufactureDate = (DateTime)data.ManufactureDate;
            model.vehicles.VehicleCategoryID = data.VehicleCategoryID;
            model.vehicles.VehicleBrandID = data.VehicleBrandID;
            model.vehicles.VehicleUserID = data.VehicleUserID;
            model.vehicles.FuelType = data.FuelType;
            model.vehicles.ImageUrl = data.ImageUrl;
            model.vehicles.Mileage = data.Mileage;
            model.vehicles.FuelType = data.FuelType;
            model.vehicles.Color = data.Color;
            model.vehicles.Description = data.Description;
            model.vehicles.Address = data.Address;
            model.vehicles.RegistrationNumber = data.RegistrationNumber;
            model.vehicles.Transmission = data.Transmission;
            model.vehicles.Owner = data.Owner;
            model.brand.BrandID = data.Brand.BrandID;
            model.brand.Active = data.Brand.Active;
            model.brand.BrandCategoryID=data.Brand.BrandCategoryID;
            model.brand.BrandName = data.Brand.BrandName;   
            model.brand.ImageUrl=data.Brand.ImageUrl;
            model.user.Address = usersData.Address;
            model.user.PhoneNumber = usersData.PhoneNumber;
            model.user.FirstName = usersData.FirstName;
            model.user.Email = usersData.Email;
            model.reviews = reviews.Select(ReviewModel => new ReviewModel
            {
                ReviewID=ReviewModel.ReviewID,
                Comment = ReviewModel.Comment,
                DateTimes= (DateTime)ReviewModel.DateTimes,
                UserID=ReviewModel.UserID,
                VehicleID=ReviewModel.VehicleID,
            }).ToList();
            return View(model);
        }

        public ActionResult AllVehicles(int id=0,string search = "")
        {
            if (!string.IsNullOrEmpty(search) || id!=null)
            {
                BrandCategories brandCategories = new BrandCategories();
                brandCategories.vehiclesModel = VehicleDAL.GetAllVehicleSearch(id,search);
                ViewBag.Search = search;
                ViewBag.CheckBoxValue = id;
                return View(brandCategories);
            } 
            else
            {
                var data = allData.AllDataInfo();
                return View(data);
            }
            
        }

        public ActionResult AllBrand()
        {
            var data = allData.AllDataInfo();

            return View(data);
        }

        public ActionResult AllVehicleByBrand(int id)
        {
            BrandCategories categoryCategories = new BrandCategories();
            categoryCategories.vehiclesModel = new List<VehicleModel>();
            List<Vehicle> data=VehicleDAL.GetAllVehicleByBrand(id);
            categoryCategories.vehiclesModel = data.Select(VehicleModel => new VehicleModel
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
                Owner=VehicleModel.Owner,
                Description = VehicleModel.Description,
                Address = VehicleModel.Address,
                RegistrationNumber = VehicleModel.RegistrationNumber,
                Transmission = VehicleModel.Transmission,
            }).ToList();
            

            return View(categoryCategories);

        }

        public ActionResult RemoveComment(int id,int userIDs)
        {
            IReviewDAL.DeleteReviews(id, userIDs);

            return RedirectToAction("Index", "Home");
        }


        public ActionResult DeleteVehicleData(int id, int userID)
        {
            VehicleDAL.DeleteVehicle(id, userID);
            
            return RedirectToAction("Index");
        }

        public ActionResult Comment(int id)
        {

            BrandCategories model = new BrandCategories();
            model.vehicles = new VehicleModel();
            model.brand = new BrandModel();
            model.reviews = new List<ReviewModel>();
            Vehicle data = VehicleDAL.GetVehicleById(id);
            List<Review> reviews = IReviewDAL.getVehicleReviewById(id);
            List<Brand> brands = BrandDAL.GetAllBrand();
            model.vehicles.VehicleID = data.VehicleID;
            model.vehicles.price = data.price;
            model.vehicles.VehicleName = data.VehicleName;
            model.vehicles.AvailabilityStatus = data.AvailabilityStatus;
            model.vehicles.ManufactureDate = (DateTime)data.ManufactureDate;
            model.vehicles.VehicleCategoryID = data.VehicleCategoryID;
            model.vehicles.VehicleBrandID = data.VehicleBrandID;
            model.vehicles.FuelType = data.FuelType;
            model.vehicles.ImageUrl = data.ImageUrl;
            model.vehicles.Mileage = data.Mileage;
            model.vehicles.Color = data.Color;
            model.vehicles.Description = data.Description;
            model.vehicles.Address = data.Address;
            model.vehicles.RegistrationNumber = data.RegistrationNumber;
            model.vehicles.Transmission = data.Transmission;
            model.vehicles.Owner = data.Owner;
            model.brand.BrandID = data.Brand.BrandID;
            model.brand.Active = data.Brand.Active;
            model.brand.BrandCategoryID = data.Brand.BrandCategoryID;
            model.brand.BrandName = data.Brand.BrandName;
            model.brand.ImageUrl = data.Brand.ImageUrl;
            model.reviews = reviews.Select(ReviewModel => new ReviewModel
            {
                ReviewID = ReviewModel.ReviewID,
                Comment = ReviewModel.Comment,
                DateTimes = (DateTime)ReviewModel.DateTimes,
                UserID= ReviewModel.UserID,
            }).ToList();

            return View();
        }
    }
}