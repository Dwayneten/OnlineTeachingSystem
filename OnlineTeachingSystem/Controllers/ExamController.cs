using System;
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
        /* Create by Dwayne 2015-12-4 12:51:57 */
        [NavStatusFilter]
        public ActionResult Index()
        {
            ExamListViewModel elvm = new ExamListViewModel();
            elvm.SideBarData = new SideBarViewModel();
            elvm.SideBarData.CurrentIndex = 2;

            ExamListBusinessLayer examlistBusinessLayer = new ExamListBusinessLayer();
            

            /* Code by Dwayne 2015-12-4 12:57:17 */
            const int numPerPage = 5;
            int pageNum = Int32.Parse(RouteData.Values["id"].ToString()) - 1;
            List<ExamList> examList = examlistBusinessLayer.GetExamList();
            elvm.ExamList = examList.Skip(pageNum * numPerPage).Take(numPerPage).ToList();
            elvm.PageNum = pageNum;
            elvm.ExamNum = elvm.ExamList.Count;
            elvm.TotalNum = examList.Count;

            // 查找该年级的考试科目  加到ExamList中  Create by HuaFeng-Miki
            int GroupId = Convert.ToInt32(HttpContext.Session["Group"]);

            if (GroupId != 0)
            {
                foreach (ExamList examlist in examList)
                {
                    if (examlist.Group == GroupId)
                    {
                        ExamList ShowExamlist = new ExamList();
                        ShowExamlist.ExamName = examlist.ExamName;
                        ShowExamlist.Group = examlist.Group;
                        ShowExamlist.StartTime = examlist.StartTime;
                        ShowExamlist.Duration = examlist.Duration;
                        elvm.ExamList.Add(ShowExamlist);
                    }
                }
            }

            // Add default exam
            // Dwayne 2015-12-5 09:33:13
            if (elvm.ExamNum == 0)
            {
                ExamList exam = new ExamList();
                exam.ExamName = "Test";
                exam.StartTime = DateTime.Now;
                exam.Group = 1;
                elvm.ExamList.Add(exam);
                elvm.ExamNum = elvm.TotalNum = 1;
            }

            if (HttpContext.Session["User"] != null && Session["User"].ToString() != "")
            {
                elvm.NavStatusData = new NavStatusViewModel();
                elvm.NavStatusData.LeftLink = "/User/Profile/" + HttpContext.Session["User"].ToString();
                elvm.NavStatusData.LeftText = Session["User"].ToString();
                elvm.NavStatusData.RightLink = "/User/Logout";
                elvm.NavStatusData.RightText = "Log out";
            }
            return View("", elvm);
        }

        // 展示考试列表  Create by HuaFeng-Miki
        public ActionResult ExamList()
        {

            ExamListViewModel examlistViewModel = new ExamListViewModel();
            ExamListBusinessLayer examlistBusinessLayer = new ExamListBusinessLayer();
            List<ExamList> examList = examlistBusinessLayer.GetExamList();

            // 查找该年级的考试科目  加到ExamList中  Create by HuaFeng-Miki
            int GroupId = Convert.ToInt32(HttpContext.Session["Group"]);

            if (GroupId != 0)
            {
                foreach (ExamList examlist in examList)
                {
                    if (examlist.Group == GroupId)
                    {
                        ExamList ShowExamlist = new ExamList();
                        ShowExamlist.ExamName = examlist.ExamName;
                        ShowExamlist.Group = examlist.Group;
                        ShowExamlist.StartTime = examlist.StartTime;
                        ShowExamlist.Duration = examlist.Duration;
                        examlistViewModel.ExamList.Add(ShowExamlist);
                    }
                }
            }

            if (HttpContext.Session["User"] != null && Session["User"].ToString() != "")
            {
                examlistViewModel.NavStatusData = new NavStatusViewModel();
                examlistViewModel.NavStatusData.LeftLink = "/User/Profile/" + HttpContext.Session["User"].ToString();
                examlistViewModel.NavStatusData.LeftText = Session["User"].ToString();
                examlistViewModel.NavStatusData.RightLink = "/User/Logout";
                examlistViewModel.NavStatusData.RightText = "Log out";
            }

            return View("ExamList", examlistViewModel);
        }

        // 存储每题答案、分数和题目总数 Create by HuaFeng-Miki
        private int[] Ans = new int[200];
        private int[] ProblemScore = new int[200];
        private int ProblemTotal;
        /* show exam */ 
        public ActionResult ShowExam()
        {
            ExamViewModel examViewModel = new ExamViewModel();
            examViewModel.QuestionList = new List<Exam>();
            examViewModel.SideBarData = new SideBarViewModel();
            examViewModel.SideBarData.CurrentIndex = 2;

            ExamBusinessLayer examBusinessLayer = new ExamBusinessLayer();
            List<Exam> exam = examBusinessLayer.GetExamList();
            ProblemTotal=0;
            // 获取考试科目
            string examName = Convert.ToString(RouteData.Values["examName"]);
            if (examName != null)
            {
                foreach (Exam t in exam)
                {
                    if (t.ExamName == examName)
                    {
                        Exam userexam = new Exam();
                        userexam.ExamID = t.ExamID;
                        userexam.ExamName = t.ExamName;
                        userexam.Problem = t.Problem;
                        userexam.ProblemProperty = t.ProblemProperty;
                        userexam.Answer = t.Answer;
                        userexam.ImgSrc = t.ImgSrc;
                        userexam.Score = t.Score;

                        Ans[ProblemTotal] = t.Answer;
                        ProblemScore[ProblemTotal++] = t.Score; 

                        // single-choice question
                        if (userexam.ProblemProperty == 1)
                        {
                            userexam.First = t.First;
                            userexam.Second = t.Second;
                            userexam.Third = t.Third;
                            userexam.Fourth = t.Fourth;
                        }

                        examViewModel.QuestionList.Add(userexam);
                    }
                }
            }
            else
            {
                Response.Redirect("~");
            }

            examViewModel.QuestionNum = examViewModel.QuestionList.Count;
            examViewModel.Name = examName;

            if (HttpContext.Session["User"] != null && Session["User"].ToString() != "")
            {
                examViewModel.NavStatusData = new NavStatusViewModel();
                examViewModel.NavStatusData.LeftLink = "/User/Profile/" + HttpContext.Session["User"].ToString();
                examViewModel.NavStatusData.LeftText = Session["User"].ToString();
                examViewModel.NavStatusData.RightLink = "/User/Logout";
                examViewModel.NavStatusData.RightText = "Log out";
            }

            return View("Content", examViewModel);
        }

        /* Create by Dwayne */
        [NavStatusFilter]
        public ActionResult UploadExam()
        {
            AddExamViewModel aevm = new AddExamViewModel();
            aevm.SideBarData = new SideBarViewModel();
            aevm.SideBarData.CurrentIndex = 2;
            ExamBusinessLayer EBL = new ExamBusinessLayer();
            ExamListBusinessLayer list = new ExamListBusinessLayer();
            ExamList AddToExamLit = new ExamList();
            /* Business code here. */
            if (HttpContext.Session["Mail"].ToString()=="admin@ots.com")
            {
                HttpPostedFileBase examFile = Request.Files["examFile"];
                List<Exam> examList = new List<Exam>();

                if (examFile != null)
                {
                    examList = FileTrans(examFile);
                    if (examList != null)
                    {
                        EBL.AddExam(examList);
                        AddToExamLit.Duration = 60;
                        AddToExamLit.Group = 1;
                        AddToExamLit.ExamName = examList[0].ExamName.Split('\0')[0];

                        AddToExamLit.StartTime = DateTime.Now;
                        list.AddExamlist(AddToExamLit);
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
            else
            {
                Response.Redirect("~");
                return View("");
            }

            if (HttpContext.Session["User"] != null && Session["User"].ToString() != "")
            {
                aevm.NavStatusData = new NavStatusViewModel();
                aevm.NavStatusData.LeftLink = "/User/Profile/" + HttpContext.Session["User"].ToString();
                aevm.NavStatusData.LeftText = Session["User"].ToString();
                aevm.NavStatusData.RightLink = "/User/Logout";
                aevm.NavStatusData.RightText = "Log out";
            }

            return View("Add", aevm);
        }

        // 检查提交结果和计算分数 Create by HuaFeng-Miki
        [NavStatusFilter]
        private ActionResult CheckAnswer()
        {
            ExamViewModel examViewModel = new ExamViewModel();
            examViewModel.SideBarData = new SideBarViewModel();
            examViewModel.SideBarData.CurrentIndex = 2;
            int TotalScore = 0;

            
            for (int i = 0; i < ProblemTotal;i++)
            {
                if (Request.Form["examInfo[i].userAnswer"].ToString()[0] - 'A' == Ans[i])
                    TotalScore += ProblemScore[i];
            }
            examViewModel.ExamScore=TotalScore;

            if (HttpContext.Session["User"] != null && Session["User"].ToString() != "")
            {
                examViewModel.NavStatusData = new NavStatusViewModel();
                examViewModel.NavStatusData.LeftLink = "/User/Profile/" + HttpContext.Session["User"].ToString();
                examViewModel.NavStatusData.LeftText = Session["User"].ToString();
                examViewModel.NavStatusData.RightLink = "/User/Logout";
                examViewModel.NavStatusData.RightText = "Log out";
            }

            return View("Content", examViewModel);
        }

        /* Create by Dwayne 2015-12-4 15:04:29 */
        [NavStatusFilter]
        public ActionResult Test()
        {
            BaseViewModel bvm = new BaseViewModel();
            bvm.SideBarData = new SideBarViewModel();
            bvm.SideBarData.CurrentIndex = 1;

            if (HttpContext.Session["User"] != null && Session["User"].ToString() != "")
            {
                bvm.NavStatusData = new NavStatusViewModel();
                bvm.NavStatusData.LeftLink = "/User/Profile/" + HttpContext.Session["User"].ToString();
                bvm.NavStatusData.LeftText = Session["User"].ToString();
                bvm.NavStatusData.RightLink = "/User/Logout";
                bvm.NavStatusData.RightText = "Log out";
            }

            return View("Test", bvm);
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
            filename = filename.Split('\0')[0];
            examFile.SaveAs(filePath);
            List<Exam> questionList = new List<Exam>();

            try
            {
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                Excel.Range range;

                int rCnt;

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(filePath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                range = xlWorkSheet.UsedRange;

                for (rCnt = 2; rCnt <= range.Rows.Count; rCnt++)
                {
                    Exam question = new Exam();
                    question.ExamName = filename;
                    question.Problem = (range.Cells[rCnt, 2] as Excel.Range).Value2.ToString();
                    question.Score = (int)(range.Cells[rCnt, 8] as Excel.Range).Value2;
                    switch ((int)(range.Cells[rCnt, 1] as Excel.Range).Value2)
                    {
                        case 0:
                            question.First = (range.Cells[rCnt, 3] as Excel.Range).Value2.ToString();
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

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            catch(Exception ex)
            {
                Response.Write(ex.ToString());
                questionList = null;
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

            try 
            {
                if (HttpContext.Session["Mail"].ToString() == null || HttpContext.Session["Mail"].ToString() != "admin@ots.com")
                    Response.Redirect("~");
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
                Response.Redirect("~");
            }

            if (HttpContext.Session["User"] != null && Session["User"].ToString() != "")
            {
                aevm.NavStatusData = new NavStatusViewModel();
                aevm.NavStatusData.LeftLink = "/User/Profile/" + HttpContext.Session["User"].ToString();
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
            Session["Group"] = filterContext.HttpContext.Session["Group"];
        }
    }
}