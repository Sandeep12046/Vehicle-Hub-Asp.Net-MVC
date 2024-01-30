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
    public class BuyVehiclesController : Controller
    {
        // GET: BuyVehicles

        IBuyVehicle buyVehicle;
        public BuyVehiclesController()
        {
            this.buyVehicle = new BuyVehicleDAL(new VehicleDBEntities());
        }
        public ActionResult Index()
        {
            BrandCategories price = new BrandCategories();
            price.vehiclesModel = buyVehicle.getBudgets();
            return View();
        }

        public ActionResult getDataByBudget(int price)
        {

            return View();
        }

    }
}