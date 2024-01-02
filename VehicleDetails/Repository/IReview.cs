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
        void insertReviews(BrandCategories review);
        void DetachReviews(int id);
        List<Review> getVehicleReviewById(int review);
    }
}
