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
                url: "article/{id}",
                defaults: new { controller = "article", action = "Index", id = 1 },
                constraints: new {id = @"\d+" }
            );

            routes.MapRoute(
                name: "Add article",
                url: "article/add",
                defaults: new { controller = "article", action = "Add" }
            );

            routes.MapRoute(
                name: "Article",
                url: "article/content/{articleId}",
                defaults: new { controller = "article", action = "getArticle" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
