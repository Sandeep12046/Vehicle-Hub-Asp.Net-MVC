using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using VehicleDetails.Models;
using VehicleDetails.Models.RequiredModels.ViewModels;

namespace VehicleDetails.Repository
{
    public class ReviewDAL : IReview
    {
        VehicleDBEntities entities;
        public ReviewDAL(VehicleDBEntities VehicleDBEntities) { 
        this.entities = VehicleDBEntities;
        }
        public List<Review> getVehicleReviewById(int id)
        {
            List<Review> review= entities.Reviews.Where(ids=>ids.VehicleID==id).ToList();

            return review;
        }

        public void insertReviews(BrandCategories data)
        {
            Review review = new Review();
            review.VehicleID = data.vehicles.VehicleID;
            review.Comment=data.reviewModel.Comment;
            review.Active=data.reviewModel.Comment!=null?1:0;
            review.Status=data.reviewModel.Status!=null?1:0;
            review.DateTime=data.reviewModel.DateTime;
            entities.Reviews.Add(review);
            entities.SaveChanges();
        }

        public void DetachReviews(int id)
        {

            Review review = entities.Reviews.Where(ids=>ids.VehicleID==id).FirstOrDefault();
            entities.Reviews.Remove(review);
            entities.SaveChanges();

        }
    }
}