using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using OnlineTeachingSystem.DataAccessLayer;

namespace OnlineTeachingSystem.Models
{
    public class UserInfoBusinessLayer
    {
        public UserInfo SignUp(UserInfo userInfo)
        {
            OTSDBDAL otsdbDAL = new OTSDBDAL();
            otsdbDAL.UserInfoList.Add(userInfo);
            otsdbDAL.SaveChanges();
            return userInfo;
        }
        public List<UserInfo> GetUserInfoList()
        {
            OTSDBDAL otsdbDAL = new OTSDBDAL();
            return otsdbDAL.UserInfoList.ToList();
        }
    }
}