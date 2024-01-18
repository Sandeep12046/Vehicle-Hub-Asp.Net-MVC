using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using VehicleDetails.Models;
using VehicleDetails.Models.RequiredModels.ViewModels;
using VehicleDetails.Repository;

namespace VehicleDetails.Repository
{
    public class UserDAl:IUser
    {
        VehicleDBEntities Entities;
        public UserDAl(VehicleDBEntities entities)
        {
            this.Entities = entities;
        }

        public void CreateUser(UserModel user)
        {
            
            var date = DateTime.Now;    
            User users = new User();
            users.UserName = user.UserName;
            users.FirstName = user.FirstName;
            users.LastName = user.LastName;
            users.Email = user.Email;
            users.Passwords = user.Passwords;
            users.UserType = 0;
            users.UserTypeName  = "User";
            users.CreatedAt = date;
            users.Address = user.Address;
            users.PhoneNumber = user.PhoneNumber;
            users.UserImage = user.UserImage;
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
            oldInfo.Passwords = user.Passwords;
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
            string isMatch = (from id in Entities.Users
                             where id.UserName == user.UserName || id.Email == user.Email && id.Passwords == user.Passwords
                                 select id.UserTypeName).FirstOrDefault();
            return isMatch;
        }

        public UserModel SignInID(UserModel user)
        {
            UserModel isMatch = (from id in Entities.Users
                              where id.UserName == user.UserName || id.Email == user.Email && id.Passwords == user.Passwords
                              select new UserModel
                              {
                                  UserID= id.UserID,
                                  UserName= id.UserName,
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
            UserModel data = (from user in Entities.Users
                        join veh in Entities.Vehicles on user.UserID equals veh.VehicleUserID
                        where veh.VehicleID == id
                        select new UserModel
                        {
                            Email = user.Email,
                            FirstName = user.FirstName,
                            PhoneNumber = user.PhoneNumber,
                            Address=user.Address,
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



            return data;
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
                Passwords = user.Passwords,
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
    }
}


