using ShopHere.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopHere.Web.ViewModels
{
    public class ProductSearchViewModel
    {
        public List<Product> Products { get; set; }
        public int pageNum { get; set; }
        public string searchTerm { get; set; }

        public Pager Pager { get; set; }
    }
}