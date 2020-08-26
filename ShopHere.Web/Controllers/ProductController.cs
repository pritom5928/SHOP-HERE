using ShopHere.Entities;
using ShopHere.Services;
using ShopHere.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopHere.Web.Controllers
{
    public class ProductController : Controller
    {

        // productsService productsService = new productsService();

        #region: Table and view page
        // GET: product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string search, int? pageNum)
        {
            var pageSize = ConfigurationService.ClassObject.PageSize();
            ProductSearchViewModel model = new ProductSearchViewModel();
            var GetproductServiceClassObject = ProductsService.ClassObject;

            model.searchTerm = search;

            pageNum = pageNum.HasValue ? pageNum.Value > 0 ? pageNum.Value : 1 : 1;

            var totalRecordsCount = GetproductServiceClassObject.GetAllProductsCount(search);

            model.Products = GetproductServiceClassObject.GetProducts(search, pageNum.Value, pageSize);

            model.Pager = new Pager(totalRecordsCount, pageNum.Value, pageSize);

            return PartialView(model);
        }
        #endregion


        #region: Creation
        [HttpGet]
        public ActionResult Create()
        {
            var categories = CategoriesService.ClassObject.GetAllCategories();

            ViewBag.Categories = categories.Select(s => new VmSelectList
            {
               Id = s.Id,
               Name = s.Name

            }).ToList();
            
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(NewProductAddViewModel product)
        {
            var newproduct = new Product();

            newproduct.Name = product.Name;
            newproduct.Description = product.Description;
            newproduct.Price = product.Price;
            newproduct.CategoryId = product.CategoryId;
            newproduct.ImageUrl = product.ImageUrl;
            //newproduct.Category = CategoriesService.ClassObject.GetCategory(product.CategoryId);

            ProductsService.ClassObject.SaveOrEditProducts(newproduct);

            return RedirectToAction("ProductTable");
        }
        #endregion

        #region: Edition
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var getEditedproduct = ProductsService.ClassObject.EditProduct(id);
            

            ViewBag.Categories = CategoriesService.ClassObject.GetAllCategories().Select(s => new VmSelectList
            {
                Id = s.Id,
                Name = s.Name

            }).ToList();

            return PartialView(getEditedproduct);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            ProductsService.ClassObject.SaveOrEditProducts(product);

            return RedirectToAction("ProductTable");
        }
        #endregion

        #region: Deletion
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var checkDeletedproduct = ProductsService.ClassObject.GetSingleProduct(id);

            ProductsService.ClassObject.DeleteProductPermanent(checkDeletedproduct);

            return RedirectToAction("ProductTable");
        }

        #endregion

        public ActionResult Details(int id)
        {
            ProductDetailViewModel model = new ProductDetailViewModel();

            model.Product = ProductsService.ClassObject.GetSingleProduct(id);

            return View(model);
        }

        public ActionResult sex(int id)
        {

            return Json(id, JsonRequestBehavior.AllowGet);
            
        }
    }
}