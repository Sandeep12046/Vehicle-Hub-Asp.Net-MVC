using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VehicleDetails.Models;
using VehicleDetails.Models.RequiredModels;
using VehicleDetails.Models.RequiredModels.ViewModels;

namespace VehicleDetails.Repository
{
    public class CategoryDAL : ICategory
    {
        private VehicleDBEntities VehicleDBEntitie;
        //internal DbSet<Vehicle> Vehicles;
        public CategoryDAL(VehicleDBEntities vehicleDBEntities) {
        this.VehicleDBEntitie = vehicleDBEntities;
            //this.Vehicles = vehicleDBEntities.Set<Vehicle>();
        }

        public void CreateCategory(CategoryModel data)
        {
            Category category = new Category();
            category.CategoryName = data.CategoryName;
            category.Active = data.CategoryName!=null?1:0;
            category.ImageUrl = data.ImageUrl;
            VehicleDBEntitie.Categories.Add(category);
           
            VehicleDBEntitie.SaveChanges();
        }

        public void CreteCategoryBrand(BrandCategories data)
        {
           

            Category categorys = new Category();
            Brand brands = new Brand();
            categorys.CategoryName = data.category.CategoryName;
            categorys.Active = data.category.Active=1;
            categorys.ImageUrl = data.category.ImageUrl;
            brands.BrandName = data.brand.BrandName;
            brands.Active = data.brand.Active =1;
            brands.BrandCategoryID = data.category.CategoryID;
            brands.ImageUrl = data.brand.ImageUrl;
            VehicleDBEntitie.Categories.Add(categorys);
            VehicleDBEntitie.Brands.Add(brands);
            VehicleDBEntitie.SaveChanges();
        }

        public List<Category> GetCategories()
        {
            return VehicleDBEntitie.Categories.ToList();
        }

        public List<Vehicle> GetVehiclesByCategories(int id)
        {
            List<Vehicle> data = VehicleDBEntitie.Vehicles.Where(ids => ids.VehicleCategoryID == id).ToList();
            return data;
        }
    }
}