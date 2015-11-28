using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineTeachingSystem.Models;

namespace OnlineTeachingSystem.ViewModels
{
    public class ArticleViewModel : BaseViewModel
    {
        public int ArticleID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime CreateDate { get; set; }
        public string Content { get; set; }
    }
}