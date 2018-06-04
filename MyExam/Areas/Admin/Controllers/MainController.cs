using CaptchaGen;
using Exam.Common;
using Exam.CommonMVC;
using Exam.DTO;
using Exam.IService;
using MyExam.App_Start;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyExam.Areas.Admin.Controllers
{
    public class MainController : Controller
    {
        
        // GET: Admin/Home
        [CheckPermission("Home.Index")]
        public ActionResult Index()
        {
            AdminDTO admin = AdminHelper.GetAdminId(HttpContext);
            return View(admin);
        }
        public ActionResult Logout()
        {
            Session.Abandon();//销毁Session
            //return Redirect("~/Main/Login");
            CommonHelper.RemoveCookie("AdminLogin");
            return Redirect("/Admin/Login");
        }
    }
}