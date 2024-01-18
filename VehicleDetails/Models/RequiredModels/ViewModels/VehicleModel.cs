﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleDetails.Models.RequiredModels.ViewModels
{
    public class VehicleModel
    {


        public int VehicleID { get; set; }
        [Required(ErrorMessage = "Please enter the vehicle name")]
        [Display(Name = "Vehicle Name")]
        public string VehicleName { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid price")]
        [Display(Name = "Price")]
        [Required(ErrorMessage = "Please enter Price")]
        public Nullable<int> price { get; set; }
        [Display(Name = "Manufacture Date")]
        [Required(ErrorMessage = "Please enter a valid date")]
        public Nullable<System.DateTime> ManufactureDate { get; set; }
        [Required(ErrorMessage = "Please select availability status")]
        [Display(Name = "Availability Status")]
        public string AvailabilityStatus { get; set; }
        public Nullable<int> Active { get; set; }
        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please Select Fuel")]
        public Nullable<int> VehicleCategoryID { get; set; }
        [Display(Name = "Brand")]
        [Required(ErrorMessage = "Please enter the brand name")]
        public Nullable<int> VehicleBrandID { get; set; }
        [Display(Name = "Fuel")]
        [Required(ErrorMessage = "Please Select Fuel")]
        public string FuelType { get; set; }
        [Display(Name = "Mileage")]
        [Required(ErrorMessage = "Please Enter Mileage")]
        public Nullable<int> Mileage { get; set; }
        public Nullable<int> Status { get; set; }

        [Required(ErrorMessage = "Please Add Vehicle Image Url")]
        [Display(Name = "Vehicle Image")]
        public string ImageUrl { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public Nullable<int> VehicleUserID { get; set; }

        [Required(ErrorMessage = "Please enter color")]
        [Display(Name = "Color")]
        public string Color { get; set; }
        [Required(ErrorMessage = "Please enter registration Number")]
        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }
        [Required(ErrorMessage = "Please select transmission")]
        [Display(Name = "Transmission")]
        public string Transmission { get; set; }
        [Required(ErrorMessage = "Please select owner1")]
        [Display(Name = "Owner1")]
        public List<string>  Owner1 { get; set; }
        [Required(ErrorMessage = "Please select owner")]
        [Display(Name = "Owner")]
        public string Owner { get; set; }
        [Required(ErrorMessage = "Please enter Address")]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter Description")]
        [Display(Name = "Description")]
        public string Description { get; set; }
        public Nullable<int> VehicleType { get; set; }
        public string BrandID { get; set; }
        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }

       
        public virtual ICollection<Favority> Favorities { get; set; }
     
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
      
    }
}