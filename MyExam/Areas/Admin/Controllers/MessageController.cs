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
    public class MessageController : Controller
    {
        public IMessageService messageService { get; set; }
        // GET: Admin/Message
        [CheckPermission("Message.Index")]
        public ActionResult Index()
        {
            var data= messageService.GetAll();
            return View(data);
        }

        [CheckPermission("Message.Show")]
        [HttpGet]
        public ActionResult Show(int id)
        {
            var data= messageService.GetById(id);
            return View(data);
        }
        [CheckPermission("Message.Show")]
        [HttpPost]
        public ActionResult Show(int id,string ReplyContent)
        {
            messageService.Edit(id, ReplyContent);
            return Json(new AjaxResult { Status = "ok" });

        }
    }
}