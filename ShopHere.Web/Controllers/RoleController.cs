using Microsoft.AspNet.Identity.EntityFramework;
using ShopHere.Database;
using ShopHere.Services;
using ShopHere.Web.Models;
using ShopHere.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopHere.Web.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class RoleController : Controller
    {
        ApplicationDbContext appDbContxt = new ApplicationDbContext();
        ProjectDbContext db = new ProjectDbContext();
        
        public ActionResult Create()
        {
            var addNewRole = new IdentityRole();
            return View(addNewRole);
        }

        [HttpPost]
        public ActionResult Create(IdentityRole identityRole)
        {
            bool success;
           
             if (identityRole.Id != null && identityRole.Name != null)
             {
                var findThisRole = appDbContxt.Roles.Where(s => s.Name != null && s.Name.ToLower() == identityRole.Name.ToLower()).FirstOrDefault();

                if (findThisRole == null)
                {
                    appDbContxt.Roles.Add(identityRole);
                    appDbContxt.SaveChanges();

                    success = true;
                }
                else
                    success = false;

             }
            
            else
                success = false;

            return Json(success,JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(int? pageNum)
        {
            var pageSize = ConfigurationService.ClassObject.PageSize();
            var TotalRoles = appDbContxt.Roles.ToList().Count();

            pageNum = pageNum.HasValue ? pageNum.Value > 0 ? pageNum.Value : 1 : 1;

            RoleViewModel model = new RoleViewModel();
            model.Roles = appDbContxt.Roles.ToList();

            return View(model);
        }
    }

}