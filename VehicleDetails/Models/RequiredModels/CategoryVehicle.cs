using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleDetails.Models.RequiredModels
{
    public class CategoryVehicle
    {
        public  List<Category> _categories { get; set; }
        public List<Vehicle> _vehicles { get; set;}
    }
}