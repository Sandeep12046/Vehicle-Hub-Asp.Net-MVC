using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleDetails.Models;
using VehicleDetails.Models.RequiredModels.ViewModels;

namespace VehicleDetails.Repository
{
    public class LocationDal: ILocation
    {
        VehicleDBEntities Entities;

        public LocationDal(VehicleDBEntities Entities)
        {
            this.Entities = Entities;
        }

        //public List<LocationModel> GetLocation()
        //{
        //  var data= Entities.Locations.Select(LocationModel => new LocationModel
        //  {
        //      Locations = LocationModel.Locations,
        //      LocationID = LocationModel.LocationID,
        //  }).ToList();
        //    return data;
        //}
    }
}