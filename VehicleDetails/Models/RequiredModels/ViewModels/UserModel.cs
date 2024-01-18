using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleDetails.Models.RequiredModels.ViewModels
{
    public class UserModel
    {
        [Required(ErrorMessage = "Please enter the user name")]
        [Display(Name = "User Name")]
        public int UserID { get; set; }
        [Required(ErrorMessage = "Please enter the user name")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter the email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter the password")]
        [Display(Name = "Password")]
        public string Passwords { get; set; }
        [Required(ErrorMessage = "Please enter the first name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter the last name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
      
        public int UserType { get; set; }

        [Required(ErrorMessage = "Please enter address")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter phone number")]
        [Display(Name = "Phone Number")]
        public Nullable<long> PhoneNumber { get; set; }

        public string UserTypeName { get; set; }

        public string UserImage { get; set; }
        public string State { get; set; }
        public Nullable<long> ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }


        public System.DateTime CreatedAt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favority> Favorities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}