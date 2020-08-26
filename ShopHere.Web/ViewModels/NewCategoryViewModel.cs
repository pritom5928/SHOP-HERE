using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopHere.Web.ViewModels
{
    public class NewCategoryViewModel
    {
        [Required]
        [MinLength(5), MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }
        public bool IsFeatured { get; set; }
    }
}