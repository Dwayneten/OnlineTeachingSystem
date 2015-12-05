using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineTeachingSystem.Models
{
    public class Exam
    {
        [Key]
        public int ExamID { get; set; }
        public string ExamName{ get; set; }
        public string Problem { get; set; }
        public int ProblemProperty { get;set; }
        public string First{ get;set; }
        public string Second{ get;set; }
        public string Third{ get;set; }
        public string Fourth{ get;set; }
        public int Answer { get; set; }
        public string ImgSrc { get; set; }
        public int Score { get; set; }
    }
}