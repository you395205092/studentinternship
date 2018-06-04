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
    public class UserController : Controller
    {
        public IUserService userService { get; set; }
        // GET: Admin/Student

        [CheckPermission("User.Index")]
        public ActionResult Index(string keyword, int pageIndex = 1)
        {
            SearchOpt opt = new SearchOpt();
            opt.CurrentIndex = pageIndex;
            opt.Keyword = keyword;
            opt.PageSize = 10;
            ViewBag.pageIndex = pageIndex;
            ViewBag.keyword = keyword;

            var data = userService.Search(opt);
            return View(data);
        }
        [CheckPermission("User.Add")]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [CheckPermission("User.Add")]
        [HttpPost]
        public ActionResult Add(UserAddDTO dto)
        {

            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    ErrorMsg = MVCHelper.GetValidMsg(ModelState)
                });
            }

            var data=userService.AddUser(dto);

            if (data > 0)
            {
                return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "添加失败！" });
            }
        }
        [CheckPermission("User.Edit")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = userService.GetById(id);
            return View(data);
        }
        [CheckPermission("User.Edit")]
        public ActionResult Edit(UserAddDTO dto)
        {
            var data = userService.Edit(dto);
            if (data > 0)
            {
                return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "修改失败！" });
            }
        }
        [CheckPermission("User.Delete")]
        public ActionResult Delete(int id)
        {
            userService.MarkDeleted(id);
            return Json(new AjaxResult { Status = "ok" });
        }
        [CheckPermission("User.Delete")]
        public ActionResult BatchDelete(int[] Ids)
        {
            foreach (var id in Ids)
            {
                userService.MarkDeleted(id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }
    }
}