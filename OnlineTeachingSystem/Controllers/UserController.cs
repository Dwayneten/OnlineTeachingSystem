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

            return View("SignUp", signUpViewModel);
        }

        [NavStatusFilter]
        public ActionResult TrySignUp(UserInfo userInfo)
        {
            SignUpViewModel signUpViewModel = new SignUpViewModel();
            UserInfoBusinessLayer userInfoBusinessLayer = new UserInfoBusinessLayer();
            List<UserInfo> userInfoList = userInfoBusinessLayer.GetUserInfoList();

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
            }
            else
            {
                signUpViewModel.Message = "Email is invalid or already taken";
            }

            signUpViewModel.NickName = userInfo.NickName;
            signUpViewModel.Password = userInfo.Password;
            signUpViewModel.Mail = signUpViewModel.Mail;

            return View("SignUp", signUpViewModel);
        }
    }
}