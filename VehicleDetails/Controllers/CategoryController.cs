using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleDetails.Models;
using VehicleDetails.Repository;

namespace VehicleDetails.Controllers
{
    public class CategoryController : Controller
    {
        ICategory category;
        public CategoryController() {
            this.category = new CategoryDAL(new VehicleDBEntities());
        }
        // GET: Category
        public ActionResult Index()
        {
            IEnumerable<Category> categoriesList=category.GetCategories().ToList();
            return View(categoriesList);
        }


       public ActionResult VehicleDetails(int id) {
            IEnumerable<Vehicle> data=category.GetVehiclesByCategories(id).ToList();
            return View(data);
        }

    }
}