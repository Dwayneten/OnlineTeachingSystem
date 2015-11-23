using OnlineTeachingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTeachingSystem.Filter
{
    public class NavStatusFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewResult v = filterContext.Result as ViewResult;
            if (v != null) // v will null when v is not a ViewResult
            {
                BaseViewModel bvm = v.Model as BaseViewModel;
                if (bvm != null)//bvm will be null when we want a view without Header and footer
                {
                    bvm.NavStatusData = new NavStatusViewModel();
                    bvm.NavStatusData.LeftText = "Log in";
                    bvm.NavStatusData.LeftLink =  "/User/LogIn";
                    bvm.NavStatusData.RightText = "Sign up";
                    bvm.NavStatusData.RightLink = "/User/SignUp";
                }
            }
        }
    }
}