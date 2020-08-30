using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopHere.Web.ViewModels
{
    public class RoleViewModel
    {
        public List<IdentityRole> Roles { get; set; }
    }
}