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

            if (HttpContext.Session["User"] != null && Session["User"].ToString() != "")
            {
                bvm.NavStatusData = new NavStatusViewModel();
                bvm.NavStatusData.LeftLink = "#";
                bvm.NavStatusData.LeftText = Session["User"].ToString();
                bvm.NavStatusData.RightLink = "/User/Logout";
                bvm.NavStatusData.RightText = "Log out";
            }

            return View(bvm);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            Session["User"] = filterContext.HttpContext.Session["User"];
            Session["Mail"] = filterContext.HttpContext.Session["Mail"];
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