using OnlineTeachingSystem.Filter;
using OnlineTeachingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTeachingSystem.Models;

namespace OnlineTeachingSystem.Controllers
{

    public class ArticleController : Controller
    {
        [NavStatusFilter]
        public ActionResult Test()
        {
            BaseViewModel bvm = new BaseViewModel();
            bvm.SideBarData = new SideBarViewModel();
            bvm.SideBarData.CurrentIndex = 1;

            if (HttpContext.Session["User"] != null && Session["User"].ToString() != "")
            {
                bvm.NavStatusData = new NavStatusViewModel();
                bvm.NavStatusData.LeftLink = "#";
                bvm.NavStatusData.LeftText = Session["User"].ToString();
                bvm.NavStatusData.RightLink = "/User/Logout";
                bvm.NavStatusData.RightText = "Log out";
            }

            return View("Test", bvm);
        }
        public ActionResult GetArticle()
        {
            ArticleViewModel articleViewModel = new ArticleViewModel();
            ArticleBusinessLayer articleBusinessLayer = new ArticleBusinessLayer();
            List<Article> articleList = articleBusinessLayer.GetArticleList();
            articleViewModel.ArticleList = articleList;

            int articleID = Convert.ToInt32(Request.QueryString["ArticleID"]);
            if(articleID != 0)
            {
                Article ShowArticle = new Article();
                bool FindedFlag = false;
                foreach(Article art in articleList)
                {
                    if(art.ArticleID == articleID)
                    {
                        ShowArticle.ArticleID = art.ArticleID;
                        ShowArticle.Author = art.Author;
                        ShowArticle.Content = art.Content;
                        ShowArticle.CreateDate = art.CreateDate;
                        ShowArticle.Title = art.Title;
                        FindedFlag = true;
                        break;
                    }
                }
                if(FindedFlag)
                {
                    articleViewModel.ArticleID = ShowArticle.ArticleID;
                    articleViewModel.Author = ShowArticle.Author;
                    articleViewModel.Content = ShowArticle.Content;
                    articleViewModel.CreateDate = ShowArticle.CreateDate;
                    articleViewModel.Title = ShowArticle.Title;
                }
            }

            return View("Test");
        }
        public ActionResult UploadArticle()
        {
            Article readyArticle = new Article();
            ArticleBusinessLayer articleBusinessLayer = new ArticleBusinessLayer();
            readyArticle.Author = Request.Form["Author"];
            readyArticle.Title = Request.Form["Title"];
            readyArticle.CreateDate = Convert.ToDateTime(Request.Form["CreateDate"]);
            readyArticle.Content = Request.Form["Content"];

            articleBusinessLayer.UploadArticle(readyArticle);

            return View("Test");
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            Session["User"] = filterContext.HttpContext.Session["User"];
            Session["Mail"] = filterContext.HttpContext.Session["Mail"];
        }
    }
}