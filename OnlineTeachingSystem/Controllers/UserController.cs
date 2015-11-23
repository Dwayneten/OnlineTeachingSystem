using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTeachingSystem.Models;
using OnlineTeachingSystem.ViewModels;

namespace OnlineTeachingSystem.Controllers
{
    public class UserController : Controller
    {
        public ActionResult SignUp(UserInfo userInfo)
        {
            SignUpViewModel signUpViewModel = new SignUpViewModel();
            UserInfoBusinessLayer userInfoBusinessLayer = new UserInfoBusinessLayer();
            List<UserInfo> userInfoList = userInfoBusinessLayer.GetUserInfoList();

            bool SignUpFlag = true;

            foreach(UserInfo ui in userInfoList)
            {
                if(ui.Mail == userInfo.Mail)
                {
                    SignUpFlag = false;
                    break;
                }
            }

            if(SignUpFlag == true)
            {
                userInfoBusinessLayer.SignUp(userInfo);
            }
            else
            {
                signUpViewModel.ErrorMessage = "Email is invalid or already taken";
            }

            signUpViewModel.NickName = userInfo.NickName;
            signUpViewModel.Password = userInfo.Password;
            signUpViewModel.Mail = signUpViewModel.Mail;

            return View("SignUp",signUpViewModel);
        }
    }
}