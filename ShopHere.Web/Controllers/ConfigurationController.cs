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
    public class ConfigurationController : Controller
    {
        
        // GET: Configuration
        //public ActionResult GetFaceBookUrl()
        //{
        //  var getFbLink =  configurationService.GetSingleConfiguration("FacebookUrl").Value;

        //  return View(getFbLink);
        //}

        //public PartialViewResult GetFaceBookUrl()
        //{
        //    ConfigViewModel model = new ConfigViewModel();

        //    model.FbLink = ConfigurationService.ClassObject.GetSingleConfiguration("FacebookUrl").Value;

        //    return PartialView(model);
        //}

        public ActionResult FooterDetails()
        {
            ConfigViewModel model = new ConfigViewModel();

            model.CompanyFbLink = ConfigurationService.ClassObject.GetSingleConfiguration("FacebookUrl") != null ? ConfigurationService.ClassObject.GetSingleConfiguration("FacebookUrl").Value : "";
            model.CompanyMobileNumber = ConfigurationService.ClassObject.GetSingleConfiguration("CompanyContactNumber") != null ? ConfigurationService.ClassObject.GetSingleConfiguration("CompanyContactNumber").Value : "";
            model.CompanyAddress = ConfigurationService.ClassObject.GetSingleConfiguration("CompanyAddress") != null ? ConfigurationService.ClassObject.GetSingleConfiguration("CompanyAddress").Value : "";
            model.CompanyMail = ConfigurationService.ClassObject.GetSingleConfiguration("CompanyMail") != null ? ConfigurationService.ClassObject.GetSingleConfiguration("CompanyMail").Value : "";
            model.GoogleUrl = ConfigurationService.ClassObject.GetSingleConfiguration("GoogleUrl") != null ? ConfigurationService.ClassObject.GetSingleConfiguration("GoogleUrl").Value : "";
            model.LinkedinUrl = ConfigurationService.ClassObject.GetSingleConfiguration("LinkedinUrl") != null ? ConfigurationService.ClassObject.GetSingleConfiguration("LinkedinUrl").Value : "";
            model.TwitterUrl = ConfigurationService.ClassObject.GetSingleConfiguration("TwitterUrl") != null ? ConfigurationService.ClassObject.GetSingleConfiguration("TwitterUrl").Value : "";
            return View(model);
        }
    }
}