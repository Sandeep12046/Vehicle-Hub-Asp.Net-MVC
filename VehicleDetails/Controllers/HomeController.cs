using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleDetails.Models;
using VehicleDetails.Models.RequiredModels;
using VehicleDetails.Repository;

namespace VehicleDetails.Controllers
{
    //[RoutePrefix("vehicleDetails")]
    public class HomeController : Controller
    {
        IVehicle VehicleDAL;
        public HomeController()
        {
            this.VehicleDAL = new VehicleDAL(new VehicleDBEntities());
        }
   
        //[Route("home")]
        public ActionResult Index(string search="")
        {
            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
                IEnumerable<Vehicle> vehiclesData = VehicleDAL.searchData(search).ToList();
                return View(vehiclesData);
              
            }
            else
            {
                IEnumerable<Vehicle> vehiclesData = VehicleDAL.GetAllVehicles().ToList();
                return View(vehiclesData);
            }
        }

        [HttpGet]
        //[Route("create")]
        public ActionResult Create()
        {
          

            VehicleModel model = new VehicleModel();
            model.BrandCategorynames  = VehicleDAL.GetBrandAndCategoryName();
            
            return View(model);
        }
        [HttpPost]
        //[Route("create")]
        public ActionResult Create(VehicleModel newVehicle)
        {
            if (ModelState.IsValid)
            {
                VehicleDAL.InsertNewVehicle(newVehicle);
                return RedirectToAction("Index");
            }
          return RedirectToAction("create");
        }

        [HttpGet]
        //[Route("edit")]
        public ActionResult Edit(int id)
        {
            VehicleModel model = new VehicleModel();
            Vehicle data = VehicleDAL.GetVehicleById(id);
            model.VehicleName = data.VehicleName;
            model.price= data.price;
            model.AvailabilityStatus = data.AvailabilityStatus;
            model.VehicleId = data.VehicleID;
            model.ManufactureDate = data.ManufactureDate;
            model.BrandCategorynames = VehicleDAL.GetBrandAndCategoryName();
            model.VehicleCategoryID = data.VehicleCategoryID;
            model.VehicleBrandID = data.VehicleBrandID;
            return View(model);
        }
        [HttpPost]
        //[Route("edit")]
        public ActionResult Edit(Vehicle newVehicle) {

            VehicleDAL.UpdateVehicle(newVehicle);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            VehicleDAL.DeleteVehicle(id);
            return RedirectToAction("Index");
        }

        //[Route("VehicleById")]
        public ActionResult Details(int id)
        {
            Vehicle data=VehicleDAL.GetVehicleById(id);

            return View(data);
        }
    }
}