using OnlineTeachingSystem.Filter;
using OnlineTeachingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTeachingSystem.Controllers
{
    public class HomeController : Controller
    {
        [NavStatusFilter]
        public ActionResult Index()
        {
            BaseViewModel bvm = new BaseViewModel();
            bvm.SideBarData = new SideBarViewModel();
            bvm.SideBarData.CurrentIndex = 0;
            return View(bvm);
        }

        [NavStatusFilter]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View(new BaseViewModel());
        }

        [NavStatusFilter]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View(new BaseViewModel());
        }
    }
}