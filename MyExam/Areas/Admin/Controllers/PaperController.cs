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
    public class PaperController : Controller
    {
        public IPaperService paperService { get; set; }
        public IBankClassService bankClassService { get; set; }
        // GET: Admin/Paper
        [CheckPermission("Paper.Index")]
        public ActionResult Index()
        {
            var data= paperService.GetAll();

            return View(data);
        }
        [CheckPermission("Paper.Add")]
        [HttpGet]
        public ActionResult Add()
        {
            var data=bankClassService.GetAll();
            return View(data);
        }

        [CheckPermission("Paper.Add")]
        [HttpPost]
        public ActionResult Add(PaperAddModel model)
        {
            PaperDTO dto = new PaperDTO();
            dto.ExamTime = model.ExamTime;
            dto.PCount = string.Join(",", model.PCount);
            dto.PScore = string.Join(",", model.PScore);
            dto.Title = model.Title;
            dto.ZScore = model.ZScore;

            var data = paperService.AddPaper(dto);
            if (data > 0)
            {
                return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "添加失败！" });
            }
        }

        [CheckPermission("Paper.Delete")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            paperService.MarkDeleted(id);

            return Json(new AjaxResult { Status = "ok" });
        }

    }
}