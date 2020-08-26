using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ShopHere.Services;
using ShopHere.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopHere.Web.Controllers
{
    public class OrderController : Controller
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

        // GET: Order
        public ActionResult Index(string userId, string status, int? pageNo)
        {
            OrderViewModel model = new OrderViewModel();
            model.UserId = userId;
            model.Status = status;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            var pageSize = ConfigurationService.ClassObject.PageSize();

            model.Orders = OrderService.ClassObject.SearchOrders(userId, status, pageNo.Value, pageSize);

            var totalRecordsCount = OrderService.ClassObject.SearchOrdersCount(userId, status);

            model.Pager = new Pager(totalRecordsCount, pageNo.Value, pageSize);


            return View(model);
        }

        public ActionResult Details(int id)
        {
            OrderDetailsViewModel model = new OrderDetailsViewModel();
            model.Order = OrderService.ClassObject.GetOrderDetailsById(id);

            if (model.Order != null)
            {
                model.OrderBy = UserManager.FindById(model.Order.UserId);
            }
            model.AvailableStatuses = new List<string>() {
                "pending", "in progress", "deliverd"
            };
            
            return View(model);
        }
        public JsonResult ChangeStatus(string status, int orderid)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            jsonResult.Data = new { Success = OrderService.ClassObject.UpdateOrderStatus(orderid, status) };

            return jsonResult;
        }
    }
}