using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web.Mvc;
using VehicleDetails.Helpers;
using VehicleDetails.Models;
using VehicleDetails.Models.RequiredModels.ViewModels;
using VehicleDetails.Repository;
using System.Threading.Tasks;
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

            int userID = Convert.ToInt32(Session["UserID"]);
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
               
                
            };
            category.user = new UserModel();

            if (category.user.UserType != 0)
            {
                category.user.UserType = userDAL.getUserByVehicleID(userID).UserType;

            }
            else if (category.user.UserType != 1)
            {
                category.user.UserType = userDAL.getUserByVehicleID(userID).UserType;
            }
            else
            {
                
                category.user = new UserModel { UserType = 0 }; 
            }
            return View(category);
        }
        [HttpPost]
        //[Route("create")]
        public ActionResult Create(BrandCategories newVehicle)
        {
            
                int userID = Convert.ToInt32(Session["UserID"]);
                VehicleDAL.InsertNewVehicle(newVehicle, userID);
                return RedirectToAction("Index");
          
        }

        public ActionResult Details(int id)
        {
            BrandCategories model = new BrandCategories();
            model.vehicles = new VehicleModel();
            model.brand=new BrandModel();
            model.reviews=new List<ReviewModel>();
            model.user= new UserModel();
            model.vehiclesModel = new List<VehicleModel>();
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
            model.vehiclesModel = VehicleDAL.GetAllRelatedVehicles((int)model.vehicles.VehicleBrandID, (int)model.vehicles.VehicleCategoryID,(int)id);
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


        public ActionResult AboutUS()
        {
            return View();  
        }

        public ActionResult ContactUS()
        {
            return View();
        }


        [HttpPost]
        public ActionResult QueryInfo(UserQueryModel query)
        {
            int? userID = Convert.ToInt32(Session["UserID"]);
            userDAL.SendQueryInfo(query);
            SendMail.sendMails(query);
            if (userID!=0 && userID!=null)
            {
                return RedirectToAction("Index");
            }
            else
            {

                return RedirectToAction("ContactUs","Home");
            }
           
        }

        [HttpPost]
        public ActionResult SendEmail()
        {


            SendMail.MailSend();
            return RedirectToAction("Index");
           //try
           // {
           //     SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
           //     {
           //         Port = 587,
           //         Credentials = new NetworkCredential("sanjusandeep12046@gmail.com", "lgotlmqgefomscda"),
           //         EnableSsl = true,

            //     };
            //     MailMessage mailMessage = new MailMessage
            //     {

            //         From = new MailAddress("sanjusandeep12046@gmail.com"),
            //         Subject = "Thank You for Your Inquiry - Vehicle Hub Name Customer Service Will Reach Out Soon",
            //         Body = "Thank you for reaching out to Vehicle Hub regarding your interest in selling vehicles on our platform. " +
            //         "We appreciate your inquiry, and our dedicated customer service team is eager to assist you.\r\n\r\n" +
            //         "A representative from our customer service department will be contacting you shortly to discuss your questions and provide you with the information you need. " +
            //         "We understand that your time is valuable, and we want to ensure that we address all your queries comprehensively.\r\n\r\nIn the meantime, " +
            //         "if there's anything specific you'd like to share or if you have additional details you'd like us to be aware of, please feel free to reply to this email.\r\n\r\n" +
            //         "We look forward to the opportunity to assist you and facilitate your experience on Vehicle Hub.\r\n\r\n" +
            //         "Thank you for considering Vehicle Hub as your platform for selling vehicles.\r\n\r\nBest regards,\r\n\r\n" +
            //         "Customer Service Team\r\nVehicle Hub\r\n" +
            //         "980225544554",
            //         IsBodyHtml = false, 
            //     };

            //     mailMessage.To.Add("sanjusandeep12046@gmail.com");
            //     smtpClient.Send(mailMessage);
            //     return Redirect("index");
            // }
            // catch (Exception ex)
            // {
            //     return Content($"Error sending email: {ex.Message}");
            // }
        }


        public ActionResult SendEmailContactUS()
        {

            SendMail.ContactUsMail();
            return RedirectToAction("Index");
            //try
            //{
            //    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            //    {
            //        Port = 587,
            //        Credentials = new NetworkCredential("sanjusandeep12046@gmail.com", "lgotlmqgefomscda"),
            //        EnableSsl = true,
            //    };
            //    MailMessage mailMessage = new MailMessage
            //    {
            //        From = new MailAddress("sanjusandeep12046@gmail.com"),
            //        Subject = "Thank You for Contacting Us!",
            //        Body = "Dear[Customer's Name],\r\n\r\n       " +
            //        "Thank you for reaching out to us! We appreciate your interest in our products and services." +
            //        "A member of our Sales team will be in touch with you shortly to discuss your requirements and guide you " +
            //        "through the process of finding the right vehicle and pricing that best suits your needs.\r\n\r\n        " +
            //        "We look forward to assisting you in making an informed decision and ensuring a smooth and enjoyable experience with our team.\r\n\r\n" +
            //        "If you have any immediate questions or concerns, feel free to reach out to us at[Your Contact Information].\r\n\r\n" +
            //        "Best regards,\r\n\r\n" +
            //        "Vehicle Hub\r\n" +
            //        "9874554214155",
            //        IsBodyHtml = false,
            //    };

            //    mailMessage.To.Add("sanjusandeep12046@gmail.com");
            //    smtpClient.Send(mailMessage);
            //    return Redirect("index");
            //}
            //catch (Exception ex)
            //{
            //    return Content($"Error sending email: {ex.Message}");
            //}
        }
        public ActionResult EmailTestDrive()
        {
             int id=Convert.ToInt32(Session["UserID"]);
            //BrandCategories userData= new BrandCategories();
            //userData.user = new UserModel();
            UserModel userData = userDAL.GetUserInfoById(id);
            SendMail.TestDriveEmail(userData);

            return View();
            //try
            //{
            //    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            //    {
            //        Port = 587,
            //        Credentials = new NetworkCredential("sanjusandeep12046@gmail.com", "lgotlmqgefomscda"),
            //        EnableSsl = true,
            //    };
            //    MailMessage mailMessage = new MailMessage
            //    {
            //        From = new MailAddress("sanjusandeep12046@gmail.com"),
            //        Subject = "Thank You for Contacting Us!",
            //        Body = $"Dear {userData.UserName},\r\n\r\n" +
            //       $"We're delighted to offer {userData.UserName} an exclusive home test drive experience! \r\n\r\n" +
            //       $"Our representative will be bringing the latest models directly to {userData.UserName}'s doorstep at {userData.Address},\r\n\r\n " +
            //       $"and they can be reached at {userData.PhoneNumber} to schedule this personalized test drive.\r\n",
            //        IsBodyHtml = false,
            //    };

            //    mailMessage.To.Add("sanjusandeep12046@gmail.com");
            //    smtpClient.Send(mailMessage);
            //    return Redirect("index");
            //}
            //catch (Exception ex)
            //{
            //    return Content($"Error sending email: {ex.Message}");
            //}
        }
    }
}