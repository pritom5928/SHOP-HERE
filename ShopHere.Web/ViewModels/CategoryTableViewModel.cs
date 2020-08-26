using ShopHere.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopHere.Web.ViewModels
{
    public class CategoryTableViewModel
    {
        public List<Category> Categories { get; set; }

        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
        public int PageNum { get; set; }
    }
}