using CaptchaGen;
using Exam.Common;
using Exam.CommonMVC;
using Exam.DTO;
using Exam.IService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyExam.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        public IAdminService adminService { get; set; }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string username, string password, string Code)
        {

            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    ErrorMsg = MVCHelper.GetValidMsg(ModelState)
                });
            }
            var yzm = (string)TempData["verifyCode"];
            if (Code != yzm)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "验证码错误！" });
            }

            if (adminService.CheckLogin(username, password))
            {
                var admin = adminService.GetAll().FirstOrDefault(e => e.AdminName == username);
                CommonHelper.WriteCookie("AdminLogin", DESEncrypt.Encrypt(JsonConvert.SerializeObject(admin)), 7200);
                return Json(new AjaxResult { Status = "ok" });

            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "账号密码错误！" });

            }

        }

        


        public ActionResult CreateVerifyCode()
        {
            string verifyCode = CommonHelper.CreateVerifyCode(4);
            TempData["verifyCode"] = verifyCode;
            //Session["verifyCode"] = verifyCode;
            /*
            using (MemoryStream ms = ImageFactory.GenerateImage(verifyCode, 60, 100, 20, 6))
            {
                return File(ms, "image/jpeg");
            }*/
            MemoryStream ms = ImageFactory.GenerateImage(verifyCode, 60, 100, 20, 6);
            return File(ms, "image/jpeg");
        }
    }
}