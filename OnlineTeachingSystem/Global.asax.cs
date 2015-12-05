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
using MarkdownSharp;
using System.Text.RegularExpressions;
using System.IO;

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

            ArticleBusinessLayer articleBusinessLayer = new ArticleBusinessLayer();
            Article article = new Article();
            Regex rgx = new Regex("<[^>]+>");
            String TEMP;
            Markdown m = new Markdown();
            
            for(int i = 0;i<3;i++)
            {
                StreamReader SR1 = new StreamReader(Server.MapPath("./testArticle/Markdown_Documentation_Basics.text"));
                StreamReader SR2 = new StreamReader(Server.MapPath("./testArticle/Markdown_Documentation_Syntax.text"));
                StreamReader SR3 = new StreamReader(Server.MapPath("./testArticle/markdown-readme.text"));

                article.Author = "TestTest";
                article.CreateDate = DateTime.Now;
                article.Title = "Test" + (1 + i).ToString();

                TEMP = SR1.ReadToEnd();
                article.Content = m.Transform(TEMP);
                TEMP = rgx.Replace(article.Content, " ");
                TEMP = TEMP.Replace("\n", "");
                if (TEMP.Length >= 80) article.Description = TEMP.Substring(0, 79) + "...";
                else article.Description = TEMP.Substring(0) + "...";

                articleBusinessLayer.UploadArticle(article);

                article.Author = "TestTest";
                article.CreateDate = DateTime.Now;
                article.Title = "Test" + (2 + i).ToString();

                TEMP = SR2.ReadToEnd();
                article.Content = m.Transform(TEMP);
                TEMP = rgx.Replace(article.Content, " ");
                TEMP = TEMP.Replace("\n", "");
                if (TEMP.Length >= 80) article.Description = TEMP.Substring(0, 79) + "...";
                else article.Description = TEMP.Substring(0) + "...";

                articleBusinessLayer.UploadArticle(article);

                article.Author = "TestTest";
                article.CreateDate = DateTime.Now;
                article.Title = "Test" + (3 + i).ToString();

                TEMP = SR3.ReadToEnd();
                article.Content = m.Transform(TEMP);
                TEMP = rgx.Replace(article.Content, " ");
                TEMP = TEMP.Replace("\n", "");
                if (TEMP.Length >= 80) article.Description = TEMP.Substring(0, 79) + "...";
                else article.Description = TEMP.Substring(0) + "...";

                articleBusinessLayer.UploadArticle(article);

                SR1.Close();
                SR2.Close();
                SR3.Close();
            }
        }
    }
}
