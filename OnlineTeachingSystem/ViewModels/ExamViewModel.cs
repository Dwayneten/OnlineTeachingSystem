using OnlineTeachingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTeachingSystem.ViewModels
{
    public class ExamViewModel : BaseViewModel
    {
        public string Name { set; get; }
        public List<Exam> QuestionList { set; get; }
        public int QuestionNum { get; set; }
        public int ExamScore { get; set; }

    }
}