using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineTeachingSystem.DataAccessLayer;

namespace OnlineTeachingSystem.Models
{

    public class ExamBusinessLayer
    {
        public List<Exam> GetExamList()
        {
            OTSDBDAL otsdbDAL = new OTSDBDAL();
            return otsdbDAL.Exam.ToList();
        }
        public List<Exam> AddExam(List<Exam> examList)
        {
            OTSDBDAL otsdbDAL = new OTSDBDAL();
            foreach(Exam exam in examList)
            {
                otsdbDAL.Exam.Add(exam);
                otsdbDAL.SaveChanges();
            }
            return examList;
        }
    }
}