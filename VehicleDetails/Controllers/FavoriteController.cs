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
    public class FavoriteController : Controller
    {
        IFavorite favorite;
        IVehicle vehicle;
        public FavoriteController()
        {
            this.favorite = new FavoriteDAL(new VehicleDBEntities());
            this.vehicle=new VehicleDAL(new VehicleDBEntities()); 
        }
        // GET: Favorite
        public ActionResult Index()
        {
          
            int userID = Convert.ToInt32(Session["UserID"]);
            BrandCategories brandCategories=new BrandCategories();
            brandCategories.favoriteModels = new List<FavoriteModel>();
            brandCategories.vehiclesModel = new List<VehicleModel>(); 
            //brandCategories.vehiclesModel = favorite.getAllFavorite(userID).ToList();
            brandCategories.vehiclesModel = favorite.getAllFavoriteCompare(userID).ToList();
            return View(brandCategories);
        }


        [HttpGet]
        public ActionResult Create()
        {
            int userID = Convert.ToInt32(Session["UserID"]);
            BrandCategories brandCategories = new BrandCategories();
            brandCategories.favoriteModels = new List<FavoriteModel>();
            brandCategories.vehiclesModel = new List<VehicleModel>();
            brandCategories.vehiclesModel = favorite.getAllFavorite(userID).ToList() ;

            return View(brandCategories);
        }

        [HttpPost]
        public ActionResult Create(BrandCategories data)
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            favorite.insertFavorite(data, userID);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteFavorite(int id)
        {
            int userID = Convert.ToInt32(Session["UserID"]);
            favorite.deleteFavorite(id, userID);
            return RedirectToAction("Index");
        }


        //public ActionResult CompareVehicleData(int id)
        //{
        //    BrandCategories brandCategories = new BrandCategories();
        //    brandCategories.vehiclesModel = new List<VehicleModel>();
        //    brandCategories.vehiclesModel = favorite.getCompare(id).ToList();

        //    return View();
        //}
    }
}