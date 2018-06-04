using Exam.CommonMVC;
using Exam.DTO;
using Exam.IService;
using MyExam.App_Start;
using MyExam.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyExam.Areas.Admin.Controllers
{

    public class BankController : Controller
    {
        public IBankClassService bankClassService { get; set; }
        public IBankService bankService { get; set; }
        // GET: Admin/Bank

        [CheckPermission("Bank.Index")]
        public ActionResult Index(long? ClassId, string keyword, int pageIndex = 1)
        {

            SearchBankOpt opt = new SearchBankOpt();
            opt.CurrentIndex = pageIndex;
            opt.ClassId = ClassId;
            opt.Keyword = keyword;
            opt.PageSize = 10;
            ViewBag.pageIndex = pageIndex;
            ViewBag.ClassId = ClassId;
            ViewBag.keyword = keyword;

            var data = bankService.Search(opt);
            return View(data);
        }

        [CheckPermission("Bank.Add")]
        [HttpGet]
        public ActionResult Add(int id = 0)
        {
            BankViewAddModel model = new BankViewAddModel();
            model.Id = id;
            model.BankClass = bankClassService.GetAll();
            return View(model);
        }
        [CheckPermission("Bank.Add")]
        [HttpPost]
        public ActionResult Add(BankDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Subject) || string.IsNullOrEmpty(dto.Answer) || string.IsNullOrEmpty(dto.ItemA) || string.IsNullOrEmpty(dto.ItemB))
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "添加失败！" });
            }
            var data = bankService.Add(dto);

            if (data > 0)
            {
                return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "添加失败！" });
            }
        }
        [CheckPermission("Bank.Edit")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            BankViewEditModel model = new BankViewEditModel();
            model.Id = id;
            model.BankClass = bankClassService.GetAll();
            model.Bank = bankService.GetById(id);
            return View(model);
        }
        [CheckPermission("Bank.Edit")]
        [HttpPost]
        public ActionResult Edit(BankDTO dto)
        {
            /*if (string.IsNullOrEmpty(dto.Subject) || string.IsNullOrEmpty(dto.Answer) || string.IsNullOrEmpty(dto.ItemA) || string.IsNullOrEmpty(dto.ItemB))
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "修改失败！" });
            }*/
            var data = bankService.Edit(dto);

            if (data > 0)
            {
                return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "修改失败！" });
            }
        }

        [CheckPermission("Bank.Delete")]
        public ActionResult Delete(int id)
        {
            bankService.MarkDeleted(id);
            return Json(new AjaxResult { Status = "ok" });
        }
        [CheckPermission("Bank.Delete")]
        public ActionResult BatchDelete(int[] Ids)
        {
            foreach (int id in Ids)
            {
                bankService.MarkDeleted(id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }
    }
}