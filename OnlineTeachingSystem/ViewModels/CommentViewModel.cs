using OnlineTeachingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTeachingSystem.ViewModels
{
    public class CommentViewModel : BaseViewModel
    {
        public int ArticleID { get; set; }
        public int UserID { get; set; }
        public DateTime CreateDate { get; set; }
        public string Content { get; set; }
    }
}