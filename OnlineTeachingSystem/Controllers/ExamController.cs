using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTeachingSystem.Models;
using OnlineTeachingSystem.ViewModels;
using OnlineTeachingSystem.Filter;

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

        /* Create by Dwayne */
        [NavStatusFilter]
        public ActionResult UploadExam()
        {
            AddExamViewModel aevm = new AddExamViewModel();
            aevm.SideBarData = new SideBarViewModel();
            aevm.SideBarData.CurrentIndex = 2;

            /* Business code here. */


            /* if success then */
            aevm.Message = "Add Exam successfully!";
            aevm.AlertType = "success";
            /* if not success then */
            aevm.Message = "Warning infomation here.";
            aevm.AlertType = "danger";

            return View("Add", aevm);
        }

        /* Create by Dwayne  2015-12-3 12:28:49 */
        [NavStatusFilter]
        public ActionResult add()
        {
            AddExamViewModel aevm = new AddExamViewModel();
            aevm.SideBarData = new SideBarViewModel();
            aevm.SideBarData.CurrentIndex = 2;

            if (HttpContext.Session["User"] != null && Session["User"].ToString() != "")
            {
                aevm.NavStatusData = new NavStatusViewModel();
                aevm.NavStatusData.LeftLink = "#";
                aevm.NavStatusData.LeftText = Session["User"].ToString();
                aevm.NavStatusData.RightLink = "/User/Logout";
                aevm.NavStatusData.RightText = "Log out";
            }

            return View("Add", aevm);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            Session["User"] = filterContext.HttpContext.Session["User"];
            Session["Mail"] = filterContext.HttpContext.Session["Mail"];
        }
    }
}