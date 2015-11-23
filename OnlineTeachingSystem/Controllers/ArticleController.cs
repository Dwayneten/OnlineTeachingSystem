using OnlineTeachingSystem.Filter;
using OnlineTeachingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTeachingSystem.Controllers
{
    public class ArticleController : Controller
    {
        [NavStatusFilter]
        public ActionResult Test()
        {
            return View("Test", new BaseViewModel());
        }
    }
}