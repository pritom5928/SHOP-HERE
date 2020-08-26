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
    public class OrderService
    {
        ProjectDbContext db = new ProjectDbContext();

        #region: Singleton Design Pattern
        private static OrderService privateInMemoryObject { get; set; }
        public static OrderService ClassObject
        {
            get
            {
                if (privateInMemoryObject == null)
                    return privateInMemoryObject = new OrderService();

                return privateInMemoryObject;
            }
        }

        private OrderService()
        {
        }
        #endregion

        public List<Order> SearchOrders(string userId, string status, int pageNo, int pageSize)
        {

            var orders = db.Orders.ToList();

            if (!string.IsNullOrEmpty(userId))
            {
                orders = orders.Where(s => s.UserId.ToLower().Contains(userId.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(status))
            {
                orders = orders.Where(s => s.Status.ToLower().Contains(status.ToLower())).ToList();
            }

            return orders.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

        }
        
        public int SearchOrdersCount(string userId, string status)
        {
            var orders = db.Orders.ToList();

            if (!string.IsNullOrEmpty(userId))
            {
                orders = orders.Where(s => s.UserId.ToLower().Contains(userId.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(status))
            {
                orders = orders.Where(s => s.Status.ToLower().Contains(status.ToLower())).ToList();
            }

            return orders.Count();
        }


        public Order GetOrderDetailsById(int id)
        {
            return db.Orders.Where(s => s.Id == id).FirstOrDefault();
        }

        public bool UpdateOrderStatus(int orderid, string status)
        {
            var order = db.Orders.FirstOrDefault(s => s.Id == orderid);

            order.Status = status;
            
            return db.SaveChanges() > 0;
        }
    }
}
