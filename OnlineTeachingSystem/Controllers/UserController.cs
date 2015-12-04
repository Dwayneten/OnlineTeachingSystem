using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTeachingSystem.Models;
using OnlineTeachingSystem.ViewModels;
using OnlineTeachingSystem.Filter;
using System.Web.Security;

namespace OnlineTeachingSystem.Controllers
{
    public class UserController : Controller
    {
        [NavStatusFilter]
        public ActionResult SignUp()
        {
            // init message
            SignUpViewModel signUpViewModel = new SignUpViewModel();
            signUpViewModel.Message = "";
            signUpViewModel.AlertType = "success";

            signUpViewModel.SideBarData = new SideBarViewModel();
            signUpViewModel.SideBarData.CurrentIndex = 0;


            if (HttpContext.Session["User"] != null && Session["User"].ToString() != "")
            {
                signUpViewModel.NavStatusData = new NavStatusViewModel();
                signUpViewModel.NavStatusData.LeftLink = "/User/Profile/" + HttpContext.Session["User"].ToString();
                signUpViewModel.NavStatusData.LeftText = Session["User"].ToString();
                signUpViewModel.NavStatusData.RightLink = "/User/Logout";
                signUpViewModel.NavStatusData.RightText = "Log out";
            }

            return View("SignUp", signUpViewModel);
        }
        protected override void OnActionExecuting(ActionExecutingContext aec)
        {
            base.OnActionExecuting(aec);
            Session["User"] = aec.HttpContext.Session["User"];
            Session["Mail"] = aec.HttpContext.Session["Mail"];
        }

        /* Convert email address to lower case. Code by Dwayne */
        public string UniqueEmail(string x)
        {
            int len = x.Length;
            string unqname = "";
            for (int i = 0; i < len; ++i)
                if (Char.IsUpper(x[i]))
                    unqname += char.ToLower(x[i]);
                else
                    unqname += x[i];
            return unqname;
        }

        [NavStatusFilter]
        public ActionResult TrySignUp()
        {
            SignUpViewModel signUpViewModel = new SignUpViewModel();
            UserInfoBusinessLayer userInfoBusinessLayer = new UserInfoBusinessLayer();
            List<UserInfo> userInfoList = userInfoBusinessLayer.GetUserInfoList();
            UserInfo userInfo = new UserInfo();
            userInfo.NickName = Request.Form["NickName"];
            userInfo.Mail = UniqueEmail(Request.Form["Mail"]);
            userInfo.Password = Request.Form["Password"];

            bool SignUpFlag = true;

            foreach (UserInfo ui in userInfoList)
            {
                if (ui.Mail == userInfo.Mail)
                {
                    SignUpFlag = false;
                    break;
                }
            }

            signUpViewModel.NickName = userInfo.NickName;
            signUpViewModel.Password = userInfo.Password;
            signUpViewModel.Mail = signUpViewModel.Mail;

            signUpViewModel.SideBarData = new SideBarViewModel();
            signUpViewModel.SideBarData.CurrentIndex = 0;

            if (SignUpFlag == true)
            {
                userInfoBusinessLayer.SignUp(userInfo);
                signUpViewModel.Message = "Signup successfully!";
                signUpViewModel.AlertType = "success";
                HttpContext.Session["User"]= userInfo.NickName;
                HttpContext.Session["Mail"] = userInfo.Mail;
                
                return View("SignUp", signUpViewModel);
            }
            else
            {
                signUpViewModel.Message = "Email is invalid or already taken";
                signUpViewModel.AlertType = "danger";

                return View("SignUp", signUpViewModel);
            }
            
        }

        public ActionResult Logout()
        {
            HttpContext.Session["User"] = "";
            HttpContext.Session["Mail"] = "";

            Response.Redirect("~");
            return View("");
        }

        public ActionResult TryLogin()
        {
            SignUpViewModel signUpViewModel = new SignUpViewModel();
            signUpViewModel.SideBarData = new SideBarViewModel();
            signUpViewModel.SideBarData.CurrentIndex = 0;
            signUpViewModel.NavStatusData = new NavStatusViewModel();
            signUpViewModel.NavStatusData.LeftText = "Log in";
            signUpViewModel.NavStatusData.LeftLink = "/User/LogIn";
            signUpViewModel.NavStatusData.RightText = "Sign up";
            signUpViewModel.NavStatusData.RightLink = "/User/SignUp";

            UserInfoBusinessLayer userInfoBusinessLayer = new UserInfoBusinessLayer();
            List<UserInfo> userInfoList = userInfoBusinessLayer.GetUserInfoList();
            UserInfo userInfo = new UserInfo();
            userInfo.Mail = UniqueEmail(Request.Form["Mail"]);
            userInfo.Password = Request.Form["Password"];

            bool LoginFlag = false;

            foreach (UserInfo ui in userInfoList)
            {
                if (ui.Mail == userInfo.Mail && ui.Password == userInfo.Password)
                {
                    userInfo.NickName = ui.NickName;
                    LoginFlag = true;
                    break;
                }
            }


            if (LoginFlag == true)
            {
                signUpViewModel.NavStatusData.AlertType = "success";
                signUpViewModel.NavStatusData.Message = "Login successfully!";
                HttpContext.Session["Mail"] = userInfo.Mail;
                HttpContext.Session["User"] = userInfo.NickName;

                Response.Redirect("~");
                return View("Index", signUpViewModel);
            }
            else
            {
                signUpViewModel.NavStatusData.Message = "Wrong Email or password";
                signUpViewModel.NavStatusData.AlertType = "danger";

                return View("Signup", signUpViewModel);
            } 
        }

        /* Create by Dwayne 2015-12-4 17:25:49 */
        [NavStatusFilter]
        public ActionResult showProfile()
        {
            ProfileViewModel pvm = new ProfileViewModel();

            if (HttpContext.Session["User"] != null && Session["User"].ToString() != "")
            {
                pvm.NavStatusData = new NavStatusViewModel();
                pvm.NavStatusData.LeftLink = "/User/Profile/" + Session["Mail"].ToString();
                pvm.NavStatusData.LeftText = Session["User"].ToString();
                pvm.NavStatusData.RightLink = "/User/Logout";
                pvm.NavStatusData.RightText = "Log out";

                pvm.NickName = Session["User"].ToString();
                pvm.Mail = Session["Mail"].ToString();

                pvm.SideBarData = new SideBarViewModel();
                pvm.SideBarData.CurrentIndex = 0;
            }
            else
            {
                Response.Redirect("~");
            }

            if (Session["Mail"].ToString() == "admin@ots.com")
            {
                pvm.IsAdmin = true;
            }
            else
            {
                pvm.IsAdmin = false;
            }

            return View("Profile", pvm);
        }

        public ActionResult UserInfoUpdate()
        {
            UserInfoBusinessLayer userInfoBusinessLayer = new UserInfoBusinessLayer();
            List<UserInfo> userInfoList = userInfoBusinessLayer.GetUserInfoList();
            UserInfo userInfo = new UserInfo();

            userInfo.Password = Request.Form["Password"];
            userInfo.Mail = HttpContext.Session["Mail"].ToString();
            foreach (UserInfo ui in userInfoList)
            {
                if (ui.Mail == userInfo.Mail)
                {  
                    userInfoBusinessLayer.Remove(ui);
                    break;
                }
            }
            userInfoBusinessLayer.Add(userInfo);
            Response.Redirect("~");
            return View("Profile");
        }
    }
}