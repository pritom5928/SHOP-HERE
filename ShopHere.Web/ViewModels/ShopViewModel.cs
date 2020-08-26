using ShopHere.Entities;
using ShopHere.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopHere.Web.ViewModels
{
    public class CheckoutVieModel
    {
        public List<Product> CartProducts { get; set; }

        public List<int> CartProductIds { get; set; }

        public ApplicationUser User { get; set; }
    }

    public class ShopViewModel
    {
        public string searchTerm { get; set; }

        public List<Product> Products { get; set; }

        public List<Category> FeaturedCategories { get; set; }

        public int MaximumPrice { get; set; }
        public int? SortBy { get; set; }
        public int? CategoryId { get;  set; }
        public Pager Pager { get; set; }
    }

    public class FilterproductsBySliderViewModel
    {
        public string searchTerm { get; set; }

        public List<Product> Products { get; set; }
        public Pager Pager { get; set; }
        public int? SortBy { get;  set; }
        public int? CategoryId { get;  set; }
    }
}