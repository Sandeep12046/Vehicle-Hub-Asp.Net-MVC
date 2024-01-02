using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleDetails.Models;
using VehicleDetails.Models.RequiredModels.ViewModels;
using VehicleDetails.Repository;

namespace VehicleDetails.Controllers
{
    public class ReviewController : Controller
    {
        // GET: Review
        IReview IReviewDAL;
        IVehicle IVehicleDAL;
        public ReviewController()
        {
            this.IReviewDAL=new ReviewDAL(new VehicleDBEntities());
            this.IVehicleDAL = new VehicleDAL(new VehicleDBEntities());
        }
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public ActionResult CreateComment(BrandCategories data)
        {
            var id = data.vehicles.VehicleID;
            IReviewDAL.insertReviews(data);

            return RedirectToAction("Index", "Home");
        }

    }
}