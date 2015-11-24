using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTeachingSystem.Models;
using OnlineTeachingSystem.ViewModels;
using OnlineTeachingSystem.Filter;

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

            if (SignUpFlag == true)
            {
                userInfoBusinessLayer.SignUp(userInfo);
                signUpViewModel.Message = "Signup successfully!";
                signUpViewModel.AlertType = "success";
            }
            else
            {
                signUpViewModel.Message = "Email is invalid or already taken";
                signUpViewModel.AlertType = "danger";
            }

            signUpViewModel.NickName = userInfo.NickName;
            signUpViewModel.Password = userInfo.Password;
            signUpViewModel.Mail = signUpViewModel.Mail;

            return View("SignUp", signUpViewModel);
        }
    }
}