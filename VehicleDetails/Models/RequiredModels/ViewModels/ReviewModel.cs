using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleDetails.Models.RequiredModels.ViewModels
{
    public class ReviewModel
    {
        public int ReviewID { get; set; }
        public Nullable<int> VehicleID { get; set; }
        public string Comment { get; set; }
        public DateTime DateTimes { get; set; }
        public Nullable<int> Active { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> UserID { get; set; }
        public virtual User User { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}