using OnlineTeachingSystem.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTeachingSystem.Models
{
    public class ExamListBusinessLayer
    {
        public List<ExamList> GetExamList()
        {
            OTSDBDAL otsdbDAL = new OTSDBDAL();
            return otsdbDAL.ExamList.ToList();
        }
        public ExamList AddExamlist(ExamList examlist)
        {
            OTSDBDAL otsdbDAL = new OTSDBDAL();
            otsdbDAL.ExamList.Add(examlist);
            otsdbDAL.SaveChanges();
            return examlist;
        }
    }
}