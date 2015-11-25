using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineTeachingSystem.DataAccessLayer;

namespace OnlineTeachingSystem.Models
{
    public class ArticleBusinessLayer
    {
        public List<Article> GetArticleList()
        {
            OTSDBDAL otsdbDAL = new OTSDBDAL();
            return otsdbDAL.ArticleList.ToList();
        }
        public Article UploadArticle(Article article)
        {
            OTSDBDAL otsdbDAL = new OTSDBDAL();
            otsdbDAL.ArticleList.Add(article);
            otsdbDAL.SaveChanges();
            return article;
        }
    }
}