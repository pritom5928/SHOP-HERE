using ShopHere.Database;
using ShopHere.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopHere.Services
{
    public class CategoriesService
    {
        ProjectDbContext db = new ProjectDbContext();
        

        #region: Singleton Design Pattern
        private static CategoriesService privateInMemoryObject { get; set; }
        public static CategoriesService ClassObject
        {
            get
            {
                if (privateInMemoryObject == null)
                    return privateInMemoryObject = new CategoriesService();

                return privateInMemoryObject;
            }
        }

        private CategoriesService()
        {
        }
        #endregion

        public int GetAllCategoriesCount(string search)
        {
            int totalRecords;
            var listsOfCategories = db.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                totalRecords = listsOfCategories.Where(s => s.Name != null && s.Name.ToLower().Contains(search.ToLower())).Count();
            }
            else
            {
                totalRecords = listsOfCategories.Count();
            }

            return totalRecords;
        }
        public List<Category> GetAllCategories(int pageNum, string search, int pageSize)
        {
            //int pageSize = 3;
            var listsOfCategories = db.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                listsOfCategories = listsOfCategories
                            .Where(s => s.Name != null && s.Name.ToLower().Contains(search.ToLower()))
                            .OrderBy(s => s.Id)
                            .Skip((pageNum - 1) * pageSize)
                            .Take(pageSize);
            }

            else
            {
                listsOfCategories = listsOfCategories
                                .OrderBy(s => s.Id)
                                .Skip((pageNum - 1) * pageSize)
                                .Take(pageSize);
            }
           

            return listsOfCategories.ToList();
        }

        public List<Category> GetAllCategories()
        {
            var listsOfCategories = db.Categories.ToList();
            return listsOfCategories;
        }

        public List<Category> GetFeaturedCategories()
        {
            var listsOfFeaturedCategories = db.Categories.Where(s => s.IsFeatured).ToList();
            return listsOfFeaturedCategories;
        }

        public void SaveCategory(Category category)
        {
            var isExistData = db.Categories.FirstOrDefault(s => s.Id == category.Id);

            if (isExistData == null)
            {
                db.Categories.Add(category);
                db.SaveChanges();
            }

            else
            {
                isExistData.Name = category.Name;
                isExistData.Description = category.Description;
                isExistData.IsFeatured = category.IsFeatured;
                isExistData.ImageUrl = category.ImageUrl;
                db.SaveChanges();
            }
        }

        public Category EditCategory(int id)
        {
            var searchCategoryForEdit = db.Categories.Find(id);

            return searchCategoryForEdit;
        }

        public Category DeleteCategory(int id)
        {
            var searchCategoryForDel = db.Categories.Find(id);

            return searchCategoryForDel;
        }

        public Category GetCategory(int id)
        {
            var searchCategory = db.Categories.Find(id);

            return searchCategory;
        }

        public void DeleteCatagoryPermanent(Category category)
        {
           db.Entry(category).State = EntityState.Deleted;
           db.SaveChanges();
        }

        public void DeleteCatagoryMain(int id)
        {
            //db.Entry(category).State = EntityState.Deleted;
            //db.SaveChanges();

            var category = db.Categories.Find(id);

            db.Categories.Remove(category);
            db.SaveChanges();
        }
    }
}
