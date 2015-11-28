using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineTeachingSystem.Models;

namespace OnlineTeachingSystem.ViewModels
{
    public class ArticleListViewModel : BaseViewModel
    {
        public List<Article> ArticleList { get; set; }
    }
}