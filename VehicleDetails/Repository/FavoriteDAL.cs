using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using VehicleDetails.Models;
using VehicleDetails.Models.RequiredModels.ViewModels;

namespace VehicleDetails.Repository
{
    public class FavoriteDAL : IFavorite
    {
        private VehicleDBEntities entities;
        public FavoriteDAL(VehicleDBEntities entities) {
            this.entities = entities;
        }
        public void deleteFavorite(int id,int userID)
        {
            try
            {
                if (id!=0)
                {
                    Favority favority= entities.Favorities.Where(ids => ids.VehicleID == id && ids.UserID==userID).FirstOrDefault();
                    if (favority!=null)
                    {
                        entities.Favorities.Remove(favority);
                        entities.SaveChanges();
                    }
                }
            }catch (Exception ex)
            {
                throw new Exception("Database Query Error", ex);
            }
        }

        public List<VehicleModel> getAllFavorite(int userID)
        {
            try
            {
                BrandCategories brandCategories = new BrandCategories();
                List<VehicleModel> sd = (from fav in entities.Favorities
                                           join veh in entities.Vehicles on fav.VehicleID equals veh.VehicleID
                                           where fav.UserID==userID
                                           select new VehicleModel
                                           {
                                           
                                                VehicleID=veh.VehicleID,
                                                VehicleName=veh.VehicleName,
                                                ManufactureDate=veh.ManufactureDate,
                                                Status=veh.Status,
                                                Active=veh.Active,
                                                VehicleBrandID=veh.VehicleBrandID,
                                                VehicleCategoryID=veh.VehicleCategoryID,
                                                price=veh.price,
                                                AvailabilityStatus=veh.AvailabilityStatus,
                                                FuelType=veh.FuelType,
                                                Mileage=veh.Mileage,
                                                ImageUrl=veh.ImageUrl,
                                            
                                           }).Distinct().ToList();
                return sd;
            }
            catch(Exception ex)
            {
                throw new Exception("Dabase Query Error", ex);
            }
          
        }

        public List<VehicleModel> getAllFavoriteCompare(int id)
        {
            //List<int> VehicleIds = new List<int> { id };

            List<VehicleModel> ve = (from fav in entities.Favorities
                                     join veh in entities.Vehicles on fav.VehicleID equals veh.VehicleID
                                     join brand in entities.Brands on veh.VehicleBrandID equals brand.BrandID
                                     join category in entities.Categories on veh.VehicleCategoryID equals category.CategoryID
                                     where fav.UserID == id
                                     select new VehicleModel
                                     {
                                         VehicleID = veh.VehicleID,
                                         VehicleName = veh.VehicleName,
                                         price = veh.price,
                                         ManufactureDate = veh.ManufactureDate,
                                         AvailabilityStatus = veh.AvailabilityStatus,
                                         Active = veh.Active,
                                         VehicleBrandID = veh.VehicleBrandID,
                                         FuelType = veh.FuelType,
                                         Color=veh.Color,
                                         Transmission=veh.Transmission,
                                         Mileage = veh.Mileage,
                                         Status = veh.Status,
                                         ImageUrl = veh.ImageUrl,
                                         BrandName = brand.BrandName,
                                         CategoryName = category.CategoryName
                                     }).Distinct().ToList();
            return ve;
        }


        public void insertFavorite(BrandCategories favority,int userID)
        {
            var date= DateTime.Now;
            try
            {
                if (favority != null)
                {

                    BrandCategories brandCategories = new BrandCategories();
                    brandCategories.favoriteModels = new List<FavoriteModel>();
                    brandCategories.vehiclesModel = new List<VehicleModel>();
                    Favority favority1 = new Favority();
                
                    favority1.VehicleID= favority.vehicles.VehicleID;
                    favority1.Active = 1;
                    favority1.Status= 1;
                    favority1.DateTime= date;
                    favority1.UserID= userID;
                    entities.Favorities.Add(favority1);
                    entities.SaveChanges();
                }
            }catch(Exception ex)
            {
                throw new Exception("Database Query Error", ex);
            }
        }
    }
}