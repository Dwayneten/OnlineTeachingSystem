using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTeachingSystem.Models;
using OnlineTeachingSystem.ViewModels;

namespace OnlineTeachingSystem.Controllers
{
    public class ExamController : Controller
    {
        public ActionResult ExamList()
        {

            ExamListViewModel examlistViewModel = new ExamListViewModel();
            ExamListBusinessLayer examlistBusinessLayer = new ExamListBusinessLayer();
            List<ExamList> examList = examlistBusinessLayer.GetExamList();

            string mailID = Convert.ToString(Request.QueryString["Mail"]);
            if (mailID != null)
            {
                ExamList ShowExamlist = new ExamList();
                foreach (ExamList examlist in examList)
                {
                    if (examlist.Mail == mailID)
                    {
                        ShowExamlist.ExamName = examlist.ExamName;
                        ShowExamlist.Mail = examlist.Mail;
                        ShowExamlist.StartTime = examlist.StartTime;
                        ShowExamlist.Duration = examlist.Duration;

                        examlistViewModel.ShowExamList.Add(ShowExamlist);
                    }
                }
            }

            return View("ExamList", examlistViewModel);
        }
        /* show exam */ 
        public ActionResult ShowExam()
        {
            ExamViewModel examViewModel = new ExamViewModel();
            ExamBusinessLayer examBusinessLayer = new ExamBusinessLayer();
            List<Exam> exam = examBusinessLayer.GetExamList();

            string examName = Convert.ToString(Request.QueryString["ExamName"]);
            if (examName != null)
            {
                Exam userexam = new Exam();
                foreach (Exam t in exam)
                {
                    if (t.ExamName == examName)
                    {
                        userexam.ExamID = t.ExamID;
                        userexam.ExamName = t.ExamName;
                        userexam.Problem = t.Problem;
                        userexam.ProblemProperty = t.ProblemProperty;
                        userexam.Answer = t.Answer;
                        // single-choice question
                        if (userexam.ProblemProperty == 1)
                        {
                            userexam.First = t.First;
                            userexam.Second = t.Second;
                            userexam.Third = t.Third;
                            userexam.Fourth = t.Fourth;
                        }

                        examViewModel.UserExam.Add(userexam);
                    }
                }
            }
            return View("StartExam", examViewModel);
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            Session["User"] = filterContext.HttpContext.Session["User"];
            Session["Mail"] = filterContext.HttpContext.Session["Mail"];
        }
    }
}