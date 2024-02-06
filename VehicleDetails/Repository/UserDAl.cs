using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using VehicleDetails.Helpers;
using VehicleDetails.Models;
using VehicleDetails.Models.RequiredModels.ViewModels;
using VehicleDetails.Repository;

namespace VehicleDetails.Repository
{
    public class UserDAl:IUser
    {
        VehicleDBEntities Entities;
        IVehicle VehicleDAL;
        public UserDAl(VehicleDBEntities entities)
        {
            this.Entities = entities;
            this.VehicleDAL = new VehicleDAL(entities);
        }

        public void CreateUser(UserModel user,string path)
        {
            
            var date = DateTime.Now;    
            User users = new User();
            users.UserName = user.UserName;
            users.FirstName = user.FirstName;
            users.LastName = user.LastName;
            users.Email = user.Email;
            users.Passwords = AllData.HashPassword(user.Passwords);
            users.UserType = 0;
            users.UserTypeName  = "User";
            users.CreatedAt = date;
            users.Address = user.Address;
            users.PhoneNumber = user.PhoneNumber;
            users.UserImage = path;
            users.City = user.City;
            users.State = user.State;
            users.Country = user.Country;
            users.ZipCode = user.ZipCode;
            Entities.Users.Add(users);
            Entities.SaveChanges();

        }

        public void UpdateUserData(UserModel user,string path)
        {
            User oldInfo = Entities.Users.Where(ids=>ids.UserID==user.UserID).FirstOrDefault();
            oldInfo.UserName = user.UserName;
            oldInfo.FirstName = user.FirstName;
            oldInfo.LastName = user.LastName;
            oldInfo.Email = user.Email;
            oldInfo.Passwords = AllData.HashPassword(user.Passwords);
            oldInfo.Address = user.Address;
            oldInfo.PhoneNumber = user.PhoneNumber;
            oldInfo.UserImage = path;
            oldInfo.City = user.City;
            oldInfo.State = user.State;
            oldInfo.Country = user.Country;
            oldInfo.ZipCode = user.ZipCode;
            Entities.SaveChanges();
        }

        public string Signin(UserModel user)
        {
            var hashedPassword = AllData.HashPassword(user.Passwords);
            string isMatch = (from id in Entities.Users
                             where id.UserName == user.UserName || id.Email == user.Email && id.Passwords == hashedPassword
                              select id.UserTypeName).FirstOrDefault();
            return isMatch;
        }

        public UserModel SignInID(UserModel user)
        {
            var hashedPassword = AllData.HashPassword(user.Passwords);

            UserModel isMatch = (from id in Entities.Users
                                 where (id.UserName == user.UserName || id.Email == user.Email) && id.Passwords == hashedPassword
                                 select new UserModel
                                 {
                                     UserID = id.UserID,
                                     UserName = id.UserName,
                                     UserImage = id.UserImage,
                                     Email = id.Email,
                                     Passwords = user.Passwords,
                                 }).FirstOrDefault();

            return isMatch;
        }

        public List<UserModel> GetUsers()
        {
            List<UserModel> users = Entities.Users.Select(user => new UserModel
            {
                UserID = user.UserID,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address= user.Address,
                Passwords = user.Passwords,
                UserType = (int)user.UserType,
                UserTypeName = user.UserTypeName,
                CreatedAt = user.CreatedAt,
                UserImage=user.UserImage,
                City=user.City,
                State=user.State,
                Country=user.Country,
                ZipCode=user.ZipCode,
            }).ToList();
            return users;
        }

        public void DeleteUserAccount(int id)
        {
            var userAct= Entities.Users.Where(usrID => usrID.UserID == id).FirstOrDefault();
            Entities.Users.Remove(userAct);
            Entities.SaveChanges();

        }

        public UserModel getUserByVehicleID(int id)
        {
            try
            {
                UserModel data = (from user in Entities.Users
                                  join veh in Entities.Vehicles on user.UserID equals veh.VehicleUserID
                                  where veh.VehicleID == id
                                  select new UserModel
                                  {
                                      Email = user.Email,
                                      FirstName = user.FirstName,
                                      PhoneNumber = user.PhoneNumber,
                                      Address = user.Address,
                                      UserID = user.UserID,
                                      UserName = user.UserName,
                                      LastName = user.LastName,
                                      Passwords = user.Passwords,
                                      UserType = (int)user.UserType,
                                      UserTypeName = user.UserTypeName,
                                      CreatedAt = user.CreatedAt,
                                      UserImage = user.UserImage,
                                      City = user.City,
                                      State = user.State,
                                      Country = user.Country,
                                      ZipCode = user.ZipCode,
                                  }).FirstOrDefault();

                return data ?? new UserModel(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ActiveVehicle(int id)
        {
            Vehicle vehicles = Entities.Vehicles
            .Where(temp => temp.VehicleID == id)
            .FirstOrDefault();
           
            vehicles.Active = 1;
           
            vehicles.Status = 1;
           
            Entities.SaveChanges();
        }

        public void DeActiveVehicle(int id)
        {
            Vehicle vehicles = Entities.Vehicles
            .Where(temp => temp.VehicleID == id)
            .FirstOrDefault();
            vehicles.Active = 0;

            vehicles.Status = 0;
            Entities.SaveChanges();
        }


        public UserModel GetUserInfoById(int id)
        {
            UserModel info=Entities.Users.Select(user=>new UserModel
            {
                UserID = user.UserID,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                //Passwords = user.Passwords,
                UserType = (int)user.UserType,
                UserTypeName = user.UserTypeName,
                CreatedAt = user.CreatedAt,
                UserImage = user.UserImage,
                City = user.City,
                State = user.State,
                Country = user.Country,
                ZipCode = user.ZipCode,
            }).Where(data=>data.UserID == id).FirstOrDefault();

           

            return info;
        }

        [HttpPost]
        public void SendQueryInfo(UserQueryModel query)
        {
           UserQuery userQuery = new UserQuery();
            userQuery.FullName = query.FullName;
            userQuery.MobileNumber = query.MobileNumber;
            userQuery.Message = query.Message;
            userQuery.EmailID = query.EmailID;
            userQuery.MsgDateTime = DateTime.Now;
            Entities.UserQuerys.Add(userQuery);
            Entities.SaveChanges();

        }

        public List<VehicleModel> GetVehiclesByUserID(int id)
        {
            try
            {
                List<VehicleModel> userVehicles = Entities.Vehicles.Where(Vehicle => Vehicle.VehicleUserID == id).Select(veh => new VehicleModel
                {
                    VehicleID = veh.VehicleID,
                    VehicleName = veh.VehicleName,
                    price = veh.price,
                    ManufactureDate = veh.ManufactureDate,
                    AvailabilityStatus = veh.AvailabilityStatus,
                    FuelType = veh.FuelType,
                    Mileage = veh.Mileage,
                    ImageUrl = veh.ImageUrl,
                    Color = veh.Color,
                    RegistrationNumber = veh.RegistrationNumber,
                    Transmission = veh.Transmission,
                    Owner = veh.Owner,
                    Address = veh.Address,
                    Description = veh.Description,
                }).ToList();
                return userVehicles ?? new List<VehicleModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
    }



   
}


