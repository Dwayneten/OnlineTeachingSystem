using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineTeachingSystem
{
    public class RouteConfig
    {
        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /* Code by Dwayne 2015-12-3 17:19:56 
            *  Last edit by Dwayen 2015-12-4 10:45:29
            */
            routes.MapRoute(
                name: "Article list",
                url: "{controller}/{id}",
                defaults: new { action = "Index", id = 1 },
                constraints: new {id = @"\d+" }
            );

            routes.MapRoute(
                name: "Add article",
                url: "{controller}/add",
                defaults: new { action = "Add" }
            );

            routes.MapRoute(
                name: "Article",
                url: "{controller}/content/{articleId}",
                defaults: new { action = "getArticle" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
