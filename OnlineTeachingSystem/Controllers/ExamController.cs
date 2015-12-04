﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTeachingSystem.Models;
using OnlineTeachingSystem.ViewModels;
using OnlineTeachingSystem.Filter;
using Excel = Microsoft.Office.Interop.Excel;

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
            ExamBusinessLayer EBL = new ExamBusinessLayer();

            /* Business code here. */
            bool IsSuccessUpload = false;
            if (HttpContext.Session["User"].ToString()=="Admin")
            {
                HttpPostedFileBase examFile = Request.Files["examFile"];
                List<Exam> examList = new List<Exam>();

                if (examFile != null)
                {
                    examList = FileTrans(examFile);
                    if (examList != null)
                    {
                        EBL.AddExam(examList);

                        /* if success then */
                        aevm.Message = "Add Exam successfully!";
                        aevm.AlertType = "success";
                    }
                    else
                    {
                        /* if not success then */
                        aevm.Message = "Fail to add Exam.";
                        aevm.AlertType = "danger";
                    }
                }
            }

            if (IsSuccessUpload == true) /* if success then */
            {
                aevm.Message = "Add Exam successfully!";
                aevm.AlertType = "success";
            }
            else /* if not success then */
            {
                aevm.Message = "Warning infomation here.";
                aevm.AlertType = "danger";
            }
            return View("Add", aevm);
        }

        /* Create by Mimikami 2015-13-3 14:50 */
        private List<Exam> FileTrans(HttpPostedFileBase examFile)
        {
            if(!System.IO.Directory.Exists(Server.MapPath("../Upload/"))) {
                System.IO.Directory.CreateDirectory(Server.MapPath("../Upload/"));
            }
            string filePath = Server.MapPath("../Upload/") + System.IO.Path.GetFileName(examFile.FileName);

            /* Delete the suffix */
            char[] temp = examFile.FileName.ToCharArray(0, examFile.FileName.Length);
            int i = 0;
            for(i=temp.Length-1;i>=0;i--) {
                if(temp[i]=='.') {
                    temp[i] = '\0';
                    break;
                }
            }

            string filename = new string(temp);
            examFile.SaveAs(filePath);
            List<Exam> questionList = new List<Exam>();

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;
            
            int rCnt;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(filePath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            range = xlWorkSheet.UsedRange;

            for (rCnt = 1; rCnt <= range.Rows.Count; rCnt++) {
                Exam question = new Exam();
                question.ExamName = filename;
                question.Problem = (range.Cells[rCnt, 2] as Excel.Range).Value2.ToString();
                switch ((int)(range.Cells[rCnt, 1] as Excel.Range).Value2) {
                    case 0:
                        question.First = (range.Cells[rCnt,3] as Excel.Range).Value2.ToString();
                        question.Second = (range.Cells[rCnt, 4] as Excel.Range).Value2.ToString();
                        question.Answer = (int)(range.Cells[rCnt, 7] as Excel.Range).Value2;
                        break;
                    case 1:
                        question.First = (range.Cells[rCnt, 3] as Excel.Range).Value2.ToString();
                        question.Second = (range.Cells[rCnt, 4] as Excel.Range).Value2.ToString();
                        question.Third = (range.Cells[rCnt, 5] as Excel.Range).Value2.ToString();
                        question.Fourth = (range.Cells[rCnt, 6] as Excel.Range).Value2.ToString();
                        question.Answer = (int)(range.Cells[rCnt, 7] as Excel.Range).Value2;
                        break;
                }
                question.ProblemProperty = (int)(range.Cells[rCnt, 1] as Excel.Range).Value2;

                questionList.Add(question);
            }

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            if(System.IO.File.Exists(filePath)) {
                System.IO.File.Delete(filePath);
            }

            return questionList;
        }

        /* Create by Mimikami 2015-13-3 14:50 */
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
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