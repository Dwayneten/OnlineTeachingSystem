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
        public int PageNum { get; set; }

        // This is stand for how many article we get form data base to be shown
        // in current list page.
        // by Dwayne 2015-12-4 11:02:19
        public int ArticleNum { get; set; }

        // Stand for total article number which help us ensure the total page number.
        public int TotalNum { get; set; }
    }
}