using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleDetails.Models;
using VehicleDetails.Models.RequiredModels.ViewModels;

namespace VehicleDetails.Repository
{
    internal interface IUser
    {
        void CreateUser(UserModel user);

        string Signin(UserModel user);
        UserModel SignInID(UserModel user);

        UserModel GetUserInfoById(int id);
        List<UserModel> GetUsers();

        void DeleteUserAccount(int id);

        UserModel getUserByVehicleID(int id);
        void ActiveVehicle(int id);
        void DeActiveVehicle(int id);

        void UpdateUserData(UserModel user,string path);

        //void DeactivateUserAccount(int id);

    }
}
