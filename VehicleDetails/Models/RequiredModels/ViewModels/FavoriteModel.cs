using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleDetails.Models.RequiredModels.ViewModels
{
    public class FavoriteModel
    {
        public int FavoriteID { get; set; }
        public Nullable<int> VehicleID { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
        public Nullable<int> Active { get; set; }
        public Nullable<int> Status { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public Nullable<int> UserID { get; set; }
        public virtual User User { get; set; }
    }
}