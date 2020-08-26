using ShopHere.Database;
using ShopHere.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopHere.Services
{
    public class ShopService
    {
        ProjectDbContext _db = new ProjectDbContext();
        #region: Singleton Design Pattern
        private static ShopService privateInMemoryObject { get; set; }
        public static ShopService ClassObject
        {
            get
            {
                if (privateInMemoryObject == null)
                    return privateInMemoryObject = new ShopService();

                return privateInMemoryObject;
            }
        }

        private ShopService()
        {
        }
        #endregion

        public int SaveOrder(Order order)
        {
             _db.Orders.Add(order);
              return _db.SaveChanges();
        }

        //public int Saveproduct(product product)
        //{
        //    _db.products.Add(product);
        //    return  _db.SaveChanges();
        //}
    }
}
