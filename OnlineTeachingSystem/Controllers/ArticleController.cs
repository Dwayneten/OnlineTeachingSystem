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
        public ActionResult Index()
        {
            ArticleListViewModel alvm = new ArticleListViewModel();
            alvm.SideBarData = new SideBarViewModel();
            alvm.SideBarData.CurrentIndex = 1;

            ArticleViewModel articleListViewModel = new ArticleViewModel();
            ArticleBusinessLayer articleBusinessLayer = new ArticleBusinessLayer();

            /* Code by Dwayne 2015-12-3 17:02:50 */
            const int numPerPage = 5;
            int pageNum = Int32.Parse(RouteData.Values["id"].ToString()) - 1;
            List<Article> articleList = articleBusinessLayer.GetArticleList();
            alvm.ArticleList = articleList.Skip(pageNum * numPerPage).Take(numPerPage).ToList();
            alvm.PageNum = pageNum;
            alvm.ArticleNum = alvm.ArticleList.Count;
            alvm.TotalNum = articleList.Count;

            if (HttpContext.Session["User"] != null && Session["User"].ToString() != "")
            {
                alvm.NavStatusData = new NavStatusViewModel();
                alvm.NavStatusData.LeftLink = "#";
                alvm.NavStatusData.LeftText = Session["User"].ToString();
                alvm.NavStatusData.RightLink = "/User/Logout";
                alvm.NavStatusData.RightText = "Log out";
            }
            return View("", alvm);
        }

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
            
            // articleViewModel.ArticleList = articleList;

            /*  Create by Mimikami 
            *   Edit by Dwayne
            */
            int articleID = Convert.ToInt32(RouteData.Values["articleId"].ToString());
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
                        ShowArticle.Description = art.Description;
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
                    articleViewModel.Description = ShowArticle.Description;
                }
            }

            return View("content", articleViewModel);
        }

        [NavStatusFilter]
        public ActionResult UploadArticle()
        {
            AddArticleViewModel aavm = new AddArticleViewModel();
            aavm.SideBarData = new SideBarViewModel();
            aavm.SideBarData.CurrentIndex = 1;
            aavm.CreateDate = DateTime.Now;

            Article readyArticle = new Article();
            ArticleBusinessLayer articleBusinessLayer = new ArticleBusinessLayer();
            readyArticle.Author = Request.Form["Author"];
            readyArticle.Title = Request.Form["Title"];
            readyArticle.CreateDate = Convert.ToDateTime(Request.Form["CreateDate"]);
            readyArticle.Content = Request.Form["Content"];
            readyArticle.Description = readyArticle.Content.Substring(0, 80) + "...";

            articleBusinessLayer.UploadArticle(readyArticle);

            aavm.Message = "Add article successfully!";
            aavm.AlertType = "success";

            return View("Add", aavm);
        }

        /* Add artcle page message and logic. Code by Dwayne */
        [NavStatusFilter]
        public ActionResult Add()
        {
            AddArticleViewModel aavm = new AddArticleViewModel();
            aavm.SideBarData = new SideBarViewModel();
            aavm.SideBarData.CurrentIndex = 1;
            aavm.CreateDate = DateTime.Now;

            if (HttpContext.Session["User"] != null && Session["User"].ToString() != "")
            {
                aavm.NavStatusData = new NavStatusViewModel();
                aavm.NavStatusData.LeftLink = "#";
                aavm.NavStatusData.LeftText = Session["User"].ToString();
                aavm.NavStatusData.RightLink = "/User/Logout";
                aavm.NavStatusData.RightText = "Log out";
            }

            return View("Add", aavm);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            Session["User"] = filterContext.HttpContext.Session["User"];
            Session["Mail"] = filterContext.HttpContext.Session["Mail"];
        }
    }
}