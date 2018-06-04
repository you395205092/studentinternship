using Exam.CommonMVC;
using Exam.DTO;
using Exam.IService;
using MyExam.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyExam.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/User
        public IAdminService AdminService { get; set; }

        [CheckPermission("Admin.Index")]
        // GET: User
        public ActionResult Index()
        {
            AdminDTO[] data = AdminService.GetAll();
            return View(data);
        }
        [CheckPermission("Admin.Edit")]
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var data = AdminService.GetById(Id);
            return View(data);
        }
        [CheckPermission("Admin.Edit")]
        [HttpPost]
        public ActionResult Edit(int Id, string PassWord)
        {
            var b=AdminService.Edit(Id, PassWord);
            if (b)
            {
                return Json(new AjaxResult
                {
                    Status = "ok"
                });
            }
            else
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    ErrorMsg = "修改ID不存在"
                });
            }
        }
        [CheckPermission("Admin.Add")]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [CheckPermission("Admin.Add")]
        [HttpPost]
        public ActionResult Add(string UserName, string PassWord,string TrueName, long[] RoleIds)
        {
            long adminId = AdminService.Add(UserName, PassWord,TrueName);

            if (adminId > 0)
            {
                return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "添加失败" });
            }
        }
        [CheckPermission("Admin.Delete")]
        public ActionResult Delete(int id)
        {
            AdminService.MarkDeleted(id);
            return Json(new AjaxResult { Status = "ok" });
        }


        [CheckPermission("Admin.Delete")]
        public ActionResult BatchDelete(int[] Ids)
        {
            foreach (int id in Ids)
            {
                AdminService.MarkDeleted(id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }
    }
}