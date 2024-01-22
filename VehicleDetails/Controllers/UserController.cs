using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using VehicleDetails.Models;
using VehicleDetails.Models.RequiredModels.ViewModels;
using VehicleDetails.Repository;

namespace VehicleDetails.Controllers
{
    public class UserController : Controller
    {

        IUser UserDAl;
        IVehicle VehicleDAL;
        IBrand BrandDAL;
        ICategory CategoryDAL;
        ILocation LocationDal;
        VehicleDBEntities db=new VehicleDBEntities();
        public UserController()
        {
            this.UserDAl = new UserDAl(new VehicleDBEntities());
            this.VehicleDAL = new VehicleDAL(new VehicleDBEntities());
            this.BrandDAL=new BrandDAL(new VehicleDBEntities());
            this.CategoryDAL=new CategoryDAL(new VehicleDBEntities());
            this.LocationDal=new LocationDal(new VehicleDBEntities());
        }
        // GET: User
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(UserModel sigin)
        {
            if (ModelState.IsValidField("Email") && ModelState.IsValidField("Passwords"))
            {
                if(db.Users.Any(check=>check.Email == sigin.Email && check.Passwords == sigin.Passwords))
                {
                string Sigin = UserDAl.Signin(sigin);
                BrandCategories userData = new BrandCategories();
                userData.user = new UserModel();
                userData.user = UserDAl.SignInID(sigin);
                var UserName = userData.user.UserName != null ? userData.user.UserName : "NA";
                var UserID = userData.user.UserID != 0 ? userData.user.UserID : 0;
                    var UserImage = userData.user.UserImage;
                if (Sigin == "Admin")
                    {
                        Session["UserName"] = UserName;
                        Session["UserID"] = UserID;
                        Session["UserImage"] = UserImage;
                        return RedirectToAction("AllVehicleDetails", "User");
                    }
                else if (Sigin == "User")
                    {
                         Session["UserName"] = UserName;
                         Session["UserID"] = UserID;
                         Session["UserImage"] = UserImage;
                        return RedirectToAction("Index", "Home");
                    }
                     else
                         {
                           ViewBag.SignIn = "Please Check Your Email ID or Password";
                           return View();
                         }
                }
                else
                {
                    ViewBag.SignIn = "Please Check Your Email ID or Password";
                }
            }
            return View();
        }

        public ActionResult SignOut()
        {
            Session.Abandon();
            return RedirectToAction("SignIn");
        }
    

        [HttpGet]
        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel user)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(check => check.Email == user.Email))
                {
                    ViewBag.EmailMsg = "Email already exist";
                }
                else
                {
              
                    UserDAl.CreateUser(user);
                    Session["UserName"] = user.UserName;
                    BrandCategories userData = new BrandCategories();
                    userData.user = new UserModel();
                    userData.user = UserDAl.SignInID(user);
                    Session["UserID"] = userData.user.UserID;
                    return RedirectToAction("Index", "Home");
                }
             
            }
            else
            {
                return RedirectToAction("Register", "User");
            }
            return View();
        }

        public ActionResult AllVehicleDetails()
        {
            BrandCategories info= new BrandCategories();
            info.vehiclesModel = new List<VehicleModel>();
            info.Brands=new List<BrandModel>();
            info.Categories=new List<CategoryModel>();
            info.vehiclesModel= VehicleDAL.GellAllVehiclesMain();
            info.Brands= VehicleDAL.GetAllBrandDetails().ToList();
            info.Categories = VehicleDAL.GetAllCategoryDetails();
            
            return View(info);
        }


        public ActionResult AllUserData()
        {
            BrandCategories userData = new BrandCategories();
            userData.userModel = UserDAl.GetUsers();
            return View(userData);
        }


        [HttpGet]
        //[Route("create")]
        public ActionResult CreateNewVehicle()
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
                Categories = CategoryDAL.GetCategories().Select(CategoryModel => new CategoryModel
                {
                    CategoryID = CategoryModel.CategoryID,
                    CategoryName = CategoryModel.CategoryName,
                    ImageUrl = CategoryModel.ImageUrl,
                }).ToList(),
                //locationModel = LocationDal.GetLocation().Select(LocationModel => new LocationModel
                //{
                //    LocationID = LocationModel.LocationID,
                //    Locations = LocationModel.Locations,
                //}).ToList(),

            };

            return View(category);
        }
        [HttpPost]
        //[Route("create")]
        public ActionResult CreateNewVehicle(BrandCategories newVehicle)
        {
            if (ModelState.IsValid)
            {
                //var data=string.Join(",",newVehicle.vehicles.Owner1);

                int userID = Convert.ToInt32(Session["UserID"]);
                VehicleDAL.InsertNewVehicle(newVehicle, userID);
                return RedirectToAction("AllVehicleDetails");
            }
            return RedirectToAction("CreateNewVehicle");
        }


        [HttpPost]
        public ActionResult CreateBrandData(BrandCategories data)
        {
            BrandDAL.InsertBrandData(data);
            return RedirectToAction("AllVehicleDetails");
        }

        [HttpPost]
        public ActionResult CreateCategoryData(BrandCategories data)
        {
            CategoryDAL.CreteCategoryBrand(data);
            return RedirectToAction("AllVehicleDetails");
        }

        [HttpGet]
        //[Route("edit")]
        public ActionResult EditVehicleDetails(int id)
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
            model.vehicles.FuelType = data.FuelType;
            model.vehicles.ImageUrl = data.ImageUrl;
            model.vehicles.Mileage = data.Mileage;
            model.vehicles.Color = data.Color;
            model.vehicles.Description = data.Description;
            model.vehicles.Address = data.Address;
            model.vehicles.Description=data.Description;
            model.vehicles.RegistrationNumber = data.RegistrationNumber;
            model.vehicles.Transmission=data.Transmission;

            List<Brand> brands = BrandDAL.GetAllBrand();
            model.Brands = brands.Select(b => new BrandModel
            {
                BrandID = b.BrandID,
                BrandName = b.BrandName,
                Active = b.Active,
                BrandCategoryID = b.BrandCategoryID,
                ImageUrl = b.ImageUrl,
                
            }).ToList();

            List<Category> categories = CategoryDAL.GetCategories();

            model.Categories = categories.Select(c => new CategoryModel
            {
                CategoryID = c.CategoryID,
                CategoryName = c.CategoryName,
                ImageUrl = c.ImageUrl,
                Active = c.Active,
            }).ToList();
            //List<LocationModel> locations=LocationDal.GetLocation();

            //model.locationModel = locations.Select(LocationModel => new LocationModel
            //{
            //    LocationID = LocationModel.LocationID,
            //    Locations = LocationModel.Locations,
            //}).ToList();

            return View(model);
        }



        [HttpPost]
        //[Route("edit")]
        public ActionResult EditVehicleDetails(BrandCategories newVehicle)
        {
            if (ModelState.IsValid)
            {
                VehicleDAL.UpdateVehicle(newVehicle);
                return RedirectToAction("AllVehicleDetails", "User");
            }
            return View("EditVehicleDetails");
        }

        public ActionResult DeleteVehicleData(int id)
        {
            return RedirectToAction("AllVehicleDetails");
        }


        public ActionResult DeleteUserAccount(int id)
        {
            UserDAl.DeleteUserAccount(id);
            return RedirectToAction("AllVehicleDetails");
        }


        public ActionResult Activate(int id)
        {
            UserDAl.ActiveVehicle(id);
            return RedirectToAction("AllVehicleDetails");
        }


        public ActionResult Deactivate(int id)
        {
            UserDAl.DeActiveVehicle(id);
            return RedirectToAction("AllVehicleDetails");
        }

        [HttpGet]
        public ActionResult UserProfile()
        {
            int id=Convert.ToInt32(Session["UserID"]);
            //BrandCategories userData= new BrandCategories();
            //userData.user = new UserModel();
            UserModel userData = UserDAl.GetUserInfoById(id);
            return View(userData);
        }

        [HttpPost]
        public ActionResult UserProfile(UserModel data,HttpPostedFileBase UserImage)
        {
            string path = UploadImage(UserImage);
            if (path.Equals("-1"))
            {
                ViewBag.imageErrorMsg = "Please select proper image type";
            }
            else
            {
                UserDAl.UpdateUserData(data,path);
            }
            
            return RedirectToAction("Index","Home");
        }

        public string UploadImage(HttpPostedFileBase file)
        {
            string path = "-1";
            string extension=Path.GetExtension(file.FileName);
            if (extension.ToLower().Equals(".jpg")|| extension.ToLower().Equals(".png")|| extension.ToLower().Equals(".jpeg"))
            {
                path = Path.Combine(Server.MapPath("~/Asserts/Images"), Path.GetFileName(file.FileName));
                file.SaveAs(path);
                path = "~/Asserts/Images/" + Path.GetFileName(file.FileName);
                return path;
            }
            else
            {
                return path;
            }
           
        }
    }

}