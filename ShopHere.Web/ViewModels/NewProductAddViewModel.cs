using ShopHere.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopHere.Web.ViewModels
{
    public class NewProductAddViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
    }

    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
    }
}