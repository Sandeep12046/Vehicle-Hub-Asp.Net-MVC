using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleDetails.Models;
using VehicleDetails.Models.RequiredModels.ViewModels;

namespace VehicleDetails.Repository
{
    internal interface IReview
    {
        void insertReviews(BrandCategories review,int UserID);
        void DeleteReviews(int id, int userID);
        List<Review> getVehicleReviewById(int review);
    }
}
