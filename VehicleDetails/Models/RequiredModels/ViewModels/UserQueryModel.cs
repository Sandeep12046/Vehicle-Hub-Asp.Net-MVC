using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleDetails.Models.RequiredModels.ViewModels
{
    public class UserQueryModel
    {

        public int QueryID { get; set; }
  
        [Required(ErrorMessage = "Please enter name")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Please enter Mobile Number")]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
        [Required(ErrorMessage = "Please EmailID")]
        [Display(Name = "Email ID")]
        public string EmailID { get; set; }
        [Required(ErrorMessage = "Please Message")]
        [Display(Name = "Message")]
        public string Message { get; set; }
        public Nullable<System.DateTime> MsgDateTime { get; set; }
    }
}