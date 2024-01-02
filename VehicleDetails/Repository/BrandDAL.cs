using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleDetails.Models;
using VehicleDetails.Models.RequiredModels;
using VehicleDetails.Models.RequiredModels.ViewModels;

namespace VehicleDetails.Repository
{
    public class BrandDAL : IBrand
    {
        VehicleDBEntities VehicleDBEntitie;
        public BrandDAL(VehicleDBEntities VehicleDBEntities)
        {
            this.VehicleDBEntitie = VehicleDBEntities;
        }

        public List<Brand> GetAllBrand()
        {
            List<Brand> list = VehicleDBEntitie.Brands.ToList();
            return list;
        }

       

        public void InsertBrandData(BrandCategories data)
        {
             Brand brand = new Brand();
            brand.BrandName = data.brand.BrandName;
            brand.BrandCategoryID = data.category.CategoryID;
            brand.Active = data.brand.BrandName!=null ? 1 : 0;
            brand.ImageUrl = data.brand.ImageUrl;
            VehicleDBEntitie.Brands.Add(brand);
            VehicleDBEntitie.SaveChanges();
        }

        public Brand GetBrandById(int id)
        {

            return VehicleDBEntitie.Brands.Where(ids=>ids.BrandID==id).FirstOrDefault();
        }

    }
}