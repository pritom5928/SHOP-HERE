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
    public class ProductsService
    {
        ProjectDbContext db = new ProjectDbContext();

        #region: Singleton Design Pattern
        private static ProductsService privateInMemoryObject { get; set; }
        public static ProductsService ClassObject
        {
            get
            {
                if (privateInMemoryObject == null)
                    return privateInMemoryObject = new ProductsService();

                return privateInMemoryObject;
            }
        }
       
        private ProductsService()
        {
        }
        #endregion

        public List<Product> GetAllProducts(int pageNum)
        {
            int pageSize = 5;
            var listsOfproducts = db.Products.OrderBy(s => s.Id).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
          //var listsOfproducts = db.Products.ToList();
            return listsOfproducts;
        }

        public List<Product> GetAllProducts()
        {
            var listsOfproducts = db.Products.ToList();
            return listsOfproducts;
        }

        public List<Product> SearchProducts(string searchTerm, int? maximumPrice, int? minimumPrice, int? categoryId, int? sortBy, int pageNo, int pageSize)
        {
           
             var Products = db.Products.ToList();
            
             if (categoryId.HasValue)
             {
                 Products = Products.Where(s => s.Category.Id == categoryId.Value).ToList();
             }
            
             if (!string.IsNullOrEmpty(searchTerm))
             {
                 Products = Products.Where(s => s.Name != null && s.Name.ToLower().Contains (searchTerm.ToLower ())).ToList();
             }
            
             if (minimumPrice.HasValue)
             {
                 Products = Products.Where(s => s.Price >= minimumPrice.Value).ToList();
             }
            
             if (maximumPrice.HasValue)
             {
                 Products = Products.Where(s => s.Price <= maximumPrice.Value).ToList();
             }
             
            
             if (sortBy.HasValue)
             {
                 switch (sortBy.Value)
                 {
                     case 2:
                         Products = Products.OrderByDescending(s => s.Id).ToList();
                         break;
                     case 3:
                         Products = Products.OrderBy(s => s.Price).ToList();
                         break;
                     default:
                         Products = Products.OrderByDescending(s => s.Price).ToList();
                         break;
            
                 }
             }
            
             return Products.Skip((pageNo -1 ) * pageSize).Take(pageSize).ToList();
            
        }


        public List<Product> GetProducts(string search, int pageNo, int pageSize)
        {
            var allproducts = db.Products.AsQueryable();

                if (!string.IsNullOrEmpty(search))
                {
                    allproducts =  allproducts.Where(s => s.Name != null &&
                         s.Name.ToLower().Contains(search.ToLower()))
                         .OrderBy(s => s.Id)
                         .Skip((pageNo - 1) * pageSize)
                         .Take(pageSize);
                }
                else
                {
                    allproducts = allproducts.OrderBy(s => s.Id)
                     .Skip((pageNo - 1) * pageSize)
                     .Take(pageSize);
                }

            return allproducts.ToList();
        }

        public int GetAllProductsCount(string search)
        {
            int totalRecords;
            var listsOfproducts = db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                totalRecords = listsOfproducts.Where(s => s.Name != null && s.Name.ToLower().Contains(search.ToLower())).Count();
            }
            else
            {
                totalRecords = listsOfproducts.Count();
            }

            return totalRecords;
        }

        public int SearchProductsCount(string searchTerm, int? maximumPrice, int? minimumPrice, int? categoryId, int? sortBy)
        {

            var Products = db.Products.ToList();

            if (categoryId.HasValue)
            {
                Products = Products.Where(s => s.Category.Id == categoryId.Value).ToList();
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                Products = Products.Where(s => s.Name != null && s.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
            }

            if (minimumPrice.HasValue)
            {
                Products = Products.Where(s => s.Price >= minimumPrice.Value).ToList();
            }

            if (maximumPrice.HasValue)
            {
                Products = Products.Where(s => s.Price <= maximumPrice.Value).ToList();
            }


            if (sortBy.HasValue)
            {
                switch (sortBy.Value)
                {
                    case 2:
                        Products = Products.OrderByDescending(s => s.Id).ToList();
                        break;
                    case 3:
                        Products = Products.OrderBy(s => s.Price).ToList();
                        break;
                    default:
                        Products = Products.OrderByDescending(s => s.Price).ToList();
                        break;

                }
            }

            return Products.ToList().Count;

        }

        public int GetMaximumPriceOfProducts()
        {
            return Convert.ToInt32(db.Products.Max(s => s.Price));
        }

        public List<Product> GetProducts(int pageNum)
        {
            int pageSize = 5;

            var listsOfproducts = db.Products.OrderBy(s => s.Id).Skip((pageNum -1) * pageSize).Take(pageSize).ToList();
            return listsOfproducts;
        }

        public List<Product> GetProducts(int pageNum, int pageSize)
        {

            var listsOfproducts = db.Products.OrderByDescending(s => s.Id).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
            return listsOfproducts;
        }

        public List<Product> GetProductsByCategory(int categoryid, int pageSize)
        {

            var listsOfproductsByCategory = db.Products.Where(s => s.Category.Id == categoryid).OrderByDescending(s => s.Id).Take(pageSize).ToList();

            return listsOfproductsByCategory;
        }

        public List<Product> GetLatestProducts(int numOfroducts)
        {
            var listsOfproducts = db.Products.OrderByDescending(s => s.Id).Take(numOfroducts).ToList();
            return listsOfproducts;
        }

        public List<Product> GetProducts(List<int> ids)
        {
            return db.Products.Where(s => ids.Contains(s.Id)).ToList();
        }

        public Product GetSingleProduct(int id)
        {
            var getproduct = db.Products.FirstOrDefault(s => s.Id == id);

            if (getproduct != null)
            {
                return getproduct;
            }

            return new Product();
        }

        public void SaveOrEditProducts(Product Product)
        {
            var isExistData = db.Products.FirstOrDefault(s => s.Id == Product.Id);

            if (isExistData == null)
            {

                //db.Entry(Product.Category).State = EntityState.Unchanged;

                db.Products.Add(Product);
                db.SaveChanges();
            }

            else
            {
                isExistData.Name = Product.Name;
                isExistData.Description = Product.Description;
                isExistData.Price = Product.Price;
                isExistData.CategoryId = Product.CategoryId;
                if (string.IsNullOrEmpty(Product.ImageUrl))
                {
                    isExistData.ImageUrl = Product.ImageUrl;
                }

                else
                {
                    isExistData.ImageUrl = Product.ImageUrl;
                }
                db.SaveChanges();
            }
        }


        public Product EditProduct(int id)
        {
            var searchproductForEdit = db.Products.Find(id);

            return searchproductForEdit;
        }

        public Product DeleteProduct(int id)
        {
            var searchproductForDel = db.Products.Find(id);

            return searchproductForDel;
        }

        public void DeleteProductPermanent(Product Product)
        {
            db.Entry(Product).State = EntityState.Deleted;
            db.SaveChanges();
        }
    }
}
