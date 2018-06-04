using Exam.CommonMVC;
using Exam.IService;
using MyExam.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyExam.Areas.Admin.Controllers
{
    public class BankClassController : Controller
    {
        public IBankClassService bankClassService { get; set; }

        // GET: Admin/BankClass
        [CheckPermission("BankClass.Index")]
        public ActionResult Index()
        {
            var data= bankClassService.GetAll();
            return View(data);
        }
        [CheckPermission("BankClass.Add")]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [CheckPermission("BankClass.Add")]
        [HttpPost]
        public ActionResult Add(string title)
        {
            var data= bankClassService.Add(title);
            if (data>0)
            {
                return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "添加失败" });
            }
        }

        [CheckPermission("BankClass.Edit")]
        public ActionResult Edit(int id)
        {
            var data = bankClassService.GetById(id);

            return View(data);
        }

        [CheckPermission("BankClass.Edit")]
        [HttpPost]
        public ActionResult Edit(int id,string title)
        {
            var data = bankClassService.Edit(id,title);
            
            if (data > 0)
            {
                return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "修改失败" });
            }
        }

        [CheckPermission("BankClass.Delete")]
        public ActionResult Delete(int id)
        {
            bankClassService.MarkDeleted(id);
            return Json(new AjaxResult { Status = "ok" });
        }

    }
}