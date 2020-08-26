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
    public class CategoryController : Controller
    {
        //CategoriesService categoryService = new CategoriesService();

        //public CategoryController(CategoriesService categories)
        //{
        //    this.categories = categories;
        //}

        #region: Table and view page
        [HttpGet]
        public ActionResult Index()
        {

            var categoryList = CategoriesService.ClassObject.GetAllCategories();
            return View(categoryList);
        }

        public ActionResult CategoryTable(string search, int? pageNum)
        {
            var pageSize = ConfigurationService.ClassObject.PageSize();
            CategoryTableViewModel model = new CategoryTableViewModel();
            var GetCategoryServiceClassObject = CategoriesService.ClassObject;

            model.SearchTerm = search;
            pageNum = pageNum.HasValue ? (pageNum.Value > 0 ? pageNum.Value : 1) :  1;

            var totalRecordsCount = GetCategoryServiceClassObject.GetAllCategoriesCount(search);

            model.Categories = GetCategoryServiceClassObject.GetAllCategories(pageNum.Value, search, pageSize);

            model.Pager = new Pager(totalRecordsCount, pageNum.Value, pageSize);

            return PartialView(model);

            //if (model.Categories != null)
            //{

            //    model.Pager = new Pager(totalRecordsCount, pageNum, 3);
            //    return PartialView(model);
            //}
            //else
            //{
            //    return HttpNotFound();
            //}

        }
        #endregion

        #region: Creation
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category();
                category.Name = model.Name;
                category.Description = model.Description;
                category.IsFeatured = model.IsFeatured;
                category.ImageUrl = model.ImageUrl;

                CategoriesService.ClassObject.SaveCategory(category);

                return Redirect("CategoryTable");
            }

            else
            {
                return new HttpStatusCodeResult(500);
            }

           
        }
        #endregion


        #region: Edition
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var editedData = CategoriesService.ClassObject.EditCategory(id);

            return PartialView(editedData);
        }

        [HttpPost]
        public ActionResult Update(Category category)
        {
            CategoriesService.ClassObject.SaveCategory(category);

            return Redirect("CategoryTable");
        }
        #endregion

        #region: Deletion
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var deletedData = CategoriesService.ClassObject.DeleteCategory(id);

            return View(deletedData);
        }

        [HttpPost]
        public ActionResult DeleteCategory(int id)
        {
            CategoriesService.ClassObject.DeleteCatagoryMain(id);

            return Redirect("CategoryTable");
        }
        #endregion

    }
}