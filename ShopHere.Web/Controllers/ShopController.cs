using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ShopHere.Entities;
using ShopHere.Services;
using ShopHere.Web.Utility;
using ShopHere.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopHere.Web.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        public ActionResult Index(string searchTerm, int? maximumPrice, int? minimumPrice, int? categoryId, int? sortBy, int? pageNum)
        {
            var pageSize = ConfigurationService.ClassObject.ShopPageSize();
            pageNum = pageNum.HasValue ? (pageNum.Value > 0 ? pageNum.Value : 1) : 1;

            ShopViewModel model = new ShopViewModel();
            model.searchTerm = searchTerm;
            model.FeaturedCategories = CategoriesService.ClassObject.GetFeaturedCategories();

            model.MaximumPrice = ProductsService.ClassObject.GetMaximumPriceOfProducts();

            model.Products = ProductsService.ClassObject.SearchProducts(searchTerm, maximumPrice, minimumPrice, categoryId, sortBy, pageNum.Value, pageSize);

            model.SortBy = sortBy;
            model.CategoryId = categoryId;
            
            int totalRecordsOfProduct = ProductsService.ClassObject.SearchProductsCount(searchTerm, maximumPrice, minimumPrice, categoryId, sortBy);

            model.Pager = new Pager(totalRecordsOfProduct, pageNum, pageSize);

            return View(model);
        }

        public ActionResult GetProductsBySlideFilter(string searchTerm, int? maximumPrice, int? minimumPrice, int? categoryId, int? sortBy, int? pageNum)
        {
            var pageSize = ConfigurationService.ClassObject.ShopPageSize();
            FilterproductsBySliderViewModel model = new FilterproductsBySliderViewModel();
            pageNum = pageNum.HasValue ? (pageNum.Value > 0 ? pageNum.Value : 1) : 1;

            model.searchTerm = searchTerm;
            model.Products = ProductsService.ClassObject.SearchProducts(searchTerm, maximumPrice, minimumPrice, categoryId, sortBy, pageNum.Value, pageSize);

            model.SortBy = sortBy;
            model.CategoryId = categoryId;

            int totalRecordsOfProduct = ProductsService.ClassObject.SearchProductsCount(searchTerm, maximumPrice, minimumPrice, categoryId, sortBy);
            model.Pager = new Pager(totalRecordsOfProduct, pageNum, pageSize);
            return PartialView(model);
        }

        // GET: Shop

        [Authorize]
        public ActionResult CheckOut()
        {
            var productsInCartCookie = Request.Cookies["Cartproducts"];

            CheckoutVieModel model = new CheckoutVieModel();
            if (productsInCartCookie != null && !string.IsNullOrEmpty(productsInCartCookie.Value))
            {
                model.CartProductIds = productsInCartCookie.Value.Split('-').Select(s => int.Parse(s)).ToList();

                model.CartProducts = ProductsService.ClassObject.GetProducts(model.CartProductIds);

                model.User = UserManager.FindById(User.Identity.GetUserId());
            }

            return View(model);
        }


        public JsonResult PlaceProduct(string productIds)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (!string.IsNullOrEmpty(productIds))
            {
                var productQuantities = productIds.Split('-').Select(s => int.Parse(s)).ToList();

                var boughtproducts = ProductsService.ClassObject.GetProducts(productQuantities.Distinct().ToList());

                Order neworder = new Order();
                neworder.UserId = User.Identity.GetUserId();
                neworder.Status = "pending";
                neworder.OrderDate = DateTime.Now;

                neworder.TotalAmount = boughtproducts.Sum(s => s.Price * productQuantities.Where(sa => sa == s.Id).Count());

                neworder.OrderItems = new List<OrderItem>();

                neworder.OrderItems.AddRange(boughtproducts.Select(s => new OrderItem()
                {
                    ProductId = s.Id,
                    Quantity = productQuantities.Where(sa => sa == s.Id).Count()
                }));

                var rowsAffected = ShopService.ClassObject.SaveOrder(neworder);

                result.Data = new { Success = true, rows = rowsAffected };

            }

            else
            {
                result.Data = new {Success = false };
            }


            return result;
        }
    }
}