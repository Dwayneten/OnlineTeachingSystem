using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTeachingSystem.Controllers
{
    public class UserController : Controller
    {
        public ActionResult SignUp()
        {
            return View("SignUp");
        }
    }
}