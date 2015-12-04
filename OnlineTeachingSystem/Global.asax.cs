using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using OnlineTeachingSystem.DataAccessLayer;
using System.Data.Entity;
using OnlineTeachingSystem.Models;

namespace OnlineTeachingSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<OTSDBDAL>());

            /* Create by Dwayne 2015-12-4 14:05:12 */
            UserInfoBusinessLayer userInfoBusinessLayer = new UserInfoBusinessLayer();
            UserInfo userInfo = new UserInfo();
            userInfo.NickName = "Admin";
            userInfo.Mail = "admin@ots.com";
            userInfo.Password = "otsadmin";
            userInfoBusinessLayer.SignUp(userInfo);
        }
    }
}
