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
        public Exam AddExam(Exam exam)
        {
            OTSDBDAL otsdbDAL = new OTSDBDAL();
            otsdbDAL.Exam.Add(exam);
            otsdbDAL.SaveChanges();
            return exam;
        }
    }
}