using OnlineTeachingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTeachingSystem.ViewModels
{
    public class ExamListViewModel : BaseViewModel
    {
        public List<ExamList> ExamList { get; set; }
        public int PageNum { get; set; }
        public int TotalNum { get; set; }
        public int ExamNum { get; set; }
    }
}