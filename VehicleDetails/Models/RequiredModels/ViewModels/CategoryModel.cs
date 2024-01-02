using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleDetails.Models.RequiredModels.ViewModels
{
    public class CategoryModel
    {
        [Required(ErrorMessage = "Please select Category")]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "Please Enter Category Name")]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Please Add Category Image Url")]
        [Display(Name = "Category Image")]
        public string ImageUrl { get; set; }
        public int? Active { get; set; }
        public virtual ICollection<Brand> Brands { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}