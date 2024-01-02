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
        public byte[] DateTime { get; set; }
        public Nullable<int> Active { get; set; }
        public Nullable<int> Status { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}