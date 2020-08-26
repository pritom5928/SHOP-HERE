using ShopHere.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopHere.Web.ViewModels
{
    public class ProductWidgetViewModel
    {
        public List<Product> Products { get; set; }
        public bool isLatestproducts { get; set; }
    }
}