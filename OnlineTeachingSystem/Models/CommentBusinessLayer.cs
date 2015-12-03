using OnlineTeachingSystem.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTeachingSystem.Models
{
    public class CommentBusinessLayer
    {
        public List<Comment> GetCommentList()
        {
            OTSDBDAL otsdbDAL = new OTSDBDAL();
            return otsdbDAL.CommentList.ToList();
        }
        public Comment UploadComment(Comment comment)
        {
            OTSDBDAL otsdbDAL = new OTSDBDAL();
            otsdbDAL.CommentList.Add(comment);
            otsdbDAL.SaveChanges();
            return comment;
        }
    }
}