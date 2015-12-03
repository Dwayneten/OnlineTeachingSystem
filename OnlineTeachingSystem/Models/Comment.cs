using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTeachingSystem.Models
{
    public class Comment
    {
        [Key]
        public int UserID { get; set; }
        public int ArticleID { get; set; }
        public DateTime CreateDate { get; set; }
        public string Content { get; set; }
    }
}