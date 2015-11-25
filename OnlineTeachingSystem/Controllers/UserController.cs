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

            return View("SignUp", signUpViewModel);
        }

        [NavStatusFilter]
        public ActionResult TrySignUp()
        {
            SignUpViewModel signUpViewModel = new SignUpViewModel();
            UserInfoBusinessLayer userInfoBusinessLayer = new UserInfoBusinessLayer();
            List<UserInfo> userInfoList = userInfoBusinessLayer.GetUserInfoList();
            UserInfo userInfo = new UserInfo();
            userInfo.NickName = Request.Form["NickName"];
            userInfo.Mail = Request.Form["Mail"];
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
                HttpContext.Session["User"]= userInfo.UserInfoID;
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
            HttpContext.Session["Mail"] ="";

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
            userInfo.Mail = Request.Form["Mail"];
            userInfo.Password = Request.Form["Password"];

            bool LoginFlag = false;

            foreach (UserInfo ui in userInfoList)
            {
                if (ui.Mail == userInfo.Mail && ui.Password == userInfo.Password)
                {
                    LoginFlag = true;
                    break;
                }
            }


            if (LoginFlag == true)
            {
                signUpViewModel.NavStatusData.AlertType = "success";
                signUpViewModel.NavStatusData.Message = "Login successfully!";
                HttpContext.Session["User"] = userInfo.Mail;
            }
            else
            {
                signUpViewModel.NavStatusData.Message = "Wrong Email or password";
                signUpViewModel.NavStatusData.AlertType = "danger";
            }

            return View("Signup", signUpViewModel);
        }
    }
}