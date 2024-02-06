using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Net.Mail;
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


        public static void sendMails(UserQueryModel query)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("sanjusandeep12046@gmail.com", "lgotlmqgefomscda"),
                    EnableSsl = true,

                };
                MailMessage mailMessage = new MailMessage
                {

                    From = new MailAddress("sanjusandeep12046@gmail.com"),
                    Subject = "Thank You for Your Inquiry -  Vehicle Hub ",
                    Body = $"Dear {query.FullName},\r\n\r\n " +
                    "Thank you for reaching out to Project Vehicle Hub! We appreciate your interest in our services. Your inquiry is important to us, and we are excited to assist you in finding the perfect new or used vehicle.\r\n\r\n" +
                    "Here is a summary of the information you provided:\r\n\r\n " +
                    $"Full Name: {query.FullName},\r\nMobile Number: {query.MobileNumber}\r\nEmail ID: {query.EmailID}\r\nMessage: {query.Message} " +
                    "Our team will review your inquiry and get back to you as soon as possible. In the meantime, feel free to explore our website for a preview of the exciting vehicles we have available.\r\n\r\n" +
                    $"If you have any immediate questions or require urgent assistance, please don't hesitate to contact us at 9876543210 or reply to this email VehicleHubInfo@vehhub.com.\r\n\r\n" +
                    "Thank you once again for considering Project Vehicle Hub. We look forward to assisting you in your search for the perfect vehicle!\r\n\r\nBest regards,\r\n\r\n" +
                    "Customer Service Team\r\nVehicle Hub\r\n" +
                    "980225544554",
                    IsBodyHtml = false,
                };

                mailMessage.To.Add("sanjusandeep12046@gmail.com");
                smtpClient.Send(mailMessage);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static void TestDriveEmail(UserModel userData)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("sanjusandeep12046@gmail.com", "lgotlmqgefomscda"),
                    EnableSsl = true,
                };
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("sanjusandeep12046@gmail.com"),
                    Subject = "Thank You for Contacting Us!",
                    Body = $"Dear {userData.UserName},\r\n\r\n" +
                   $"We're delighted to offer {userData.UserName} an exclusive home test drive experience! \r\n\r\n" +
                   $"Our representative will be bringing the latest models directly to {userData.UserName}'s doorstep at {userData.Address},\r\n\r\n " +
                   $"and they can be reached at {userData.PhoneNumber} to schedule this personalized test drive.\r\n",
                    IsBodyHtml = false,
                };

                mailMessage.To.Add("sanjusandeep12046@gmail.com");
                smtpClient.Send(mailMessage);
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}