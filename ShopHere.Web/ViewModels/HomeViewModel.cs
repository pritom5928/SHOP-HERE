using ShopHere.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopHere.Web.ViewModels
{
    public class HomeViewModel
    {
        public List<Category> FeaturedCategories { get; set; }
        public List<Product> FeaturedProducts { get; set; }
    }

    public class VmSelectList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}