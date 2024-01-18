using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleDetails.Models;
using VehicleDetails.Models.RequiredModels.ViewModels;

namespace VehicleDetails.Repository
{
    internal interface IFavorite
    {
        void insertFavorite(BrandCategories favority,int userID);
        List<VehicleModel> getAllFavorite(int userID);
        void deleteFavorite(int id,int userID);

        List<VehicleModel> getAllFavoriteCompare(int id);
    }
}
