using System;
using System.Collections.Generic;
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
        AllData allData;
        public HomeController()
        {
            this.VehicleDAL = new VehicleDAL(new VehicleDBEntities());
            this.BrandDAL=new BrandDAL(new VehicleDBEntities());
            this.CategoryDAL=new CategoryDAL(new VehicleDBEntities());
            this.IReviewDAL=new ReviewDAL(new VehicleDBEntities());
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
                VehicleDAL.InsertNewVehicle(newVehicle);
                return RedirectToAction("Index");
            }
          return RedirectToAction("create");
        }


        [HttpPost]
        public ActionResult CreateBrandData(BrandCategories data)
        {
            BrandDAL.InsertBrandData(data);
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult CreateCategoryData(BrandCategories data)
        {
            CategoryDAL.CreteCategoryBrand(data);
            return RedirectToAction("index");
        }

        [HttpGet]
        //[Route("edit")]
        public ActionResult Edit(int id)
        {
            BrandCategories model = new BrandCategories();

            model.vehicles = new VehicleModel();
            model.Categories = new List<CategoryModel>();

            Vehicle data = VehicleDAL.GetVehicleById(id);
            model.vehicles.VehicleID = data.VehicleID;
            model.vehicles.price = data.price;
            model.vehicles.VehicleName = data.VehicleName;
            model.vehicles.AvailabilityStatus = data.AvailabilityStatus;
            model.vehicles.ManufactureDate = (DateTime)data.ManufactureDate;
            model.vehicles.VehicleCategoryID = data.VehicleCategoryID;
            model.vehicles.VehicleBrandID = data.VehicleBrandID;
            model.vehicles.FuelType= data.FuelType;
            model.vehicles.ImageUrl= data.ImageUrl;
            model.vehicles.Mileage= data.Mileage;
            List<Brand> brands = BrandDAL.GetAllBrand();
            model.Brands=brands.Select(b=>new BrandModel
            {
                BrandID=b.BrandID,
                BrandName=b.BrandName,
                Active=b.Active,
                BrandCategoryID=b.BrandCategoryID,
                ImageUrl=b.ImageUrl,
            }).ToList();
    
            List<Category> categories = CategoryDAL.GetCategories();

            model.Categories = categories.Select(c => new CategoryModel
            {
                CategoryID = c.CategoryID,
                CategoryName = c.CategoryName,
                ImageUrl = c.ImageUrl,
                Active = c.Active,
            }).ToList();

            return View(model);
        }



        [HttpPost]
        //[Route("edit")]
        public ActionResult Edit(BrandCategories newVehicle) {
            if (ModelState.IsValid)
            {
                VehicleDAL.UpdateVehicle(newVehicle);
                return RedirectToAction("Index");
            }
            return View(newVehicle);
        }

        public ActionResult Delete(int id)
        {
            VehicleDAL.DeleteVehicle(id);
            return RedirectToAction("Index");
        }

        //[Route("VehicleById")]
        public ActionResult Details(int id)
        {
            BrandCategories model = new BrandCategories();
            model.vehicles = new VehicleModel();
            model.brand=new BrandModel();
            model.reviews=new List<ReviewModel>();
            Vehicle data=VehicleDAL.GetVehicleById(id);
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
            model.brand.BrandID = data.Brand.BrandID;
            model.brand.Active = data.Brand.Active;
            model.brand.BrandCategoryID=data.Brand.BrandCategoryID;
            model.brand.BrandName = data.Brand.BrandName;   
            model.brand.ImageUrl=data.Brand.ImageUrl;
            model.reviews = reviews.Select(ReviewModel => new ReviewModel
            {
                ReviewID=ReviewModel.ReviewID,
                Comment = ReviewModel.Comment,
                DateTime = ReviewModel.DateTime,
            }).ToList();
            return View(model);
        }


        public ActionResult AddReview(BrandCategories brandCategories)
        {

            IReviewDAL.insertReviews(brandCategories);
            return View(brandCategories);
        }

        public ActionResult AllVehicles(string search = "")
        {
            if (!string.IsNullOrEmpty(search))
            {
                BrandCategories brandCategories = new BrandCategories();
                brandCategories.vehiclesModel = VehicleDAL.GetAllVehicleSearch(search);
                ViewBag.Search = search;
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
            }).ToList();
            

            return View(categoryCategories);

        }

      


     
        public ActionResult RemoveComment(int id)
        {
            IReviewDAL.DetachReviews(id);
            return RedirectToAction("Index", "Home");
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
            model.brand.BrandID = data.Brand.BrandID;
            model.brand.Active = data.Brand.Active;
            model.brand.BrandCategoryID = data.Brand.BrandCategoryID;
            model.brand.BrandName = data.Brand.BrandName;
            model.brand.ImageUrl = data.Brand.ImageUrl;
            model.reviews = reviews.Select(ReviewModel => new ReviewModel
            {
                ReviewID = ReviewModel.ReviewID,
                Comment = ReviewModel.Comment,
                DateTime = ReviewModel.DateTime,
            }).ToList();

            return View();
        }

    }
}