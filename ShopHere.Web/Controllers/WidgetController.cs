using ShopHere.Services;
using ShopHere.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopHere.Web.Controllers
{
    public class WidgetController : Controller
    {
        // GET: Widget
        public ActionResult Products(bool isLatestproducts, int? CategoryId = 0)
        {
            ProductWidgetViewModel model = new ProductWidgetViewModel();
            model.isLatestproducts = isLatestproducts;

            if (isLatestproducts)
            {
                model.Products = ProductsService.ClassObject.GetLatestProducts(4);
            }

            else if (CategoryId.HasValue && CategoryId.Value > 0)
            {
                model.Products = ProductsService.ClassObject.GetProductsByCategory(CategoryId.Value, 4);
            }
            else
            {
                model.Products = ProductsService.ClassObject.GetProducts(1, 8);
            }


            return PartialView(model);
        }
    }
}