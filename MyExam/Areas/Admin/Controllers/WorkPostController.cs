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
    public class WorkPostController : Controller
    {
        public IWorkPostService workPostService { get; set; }
        public IUserService userService { get; set; }
        public IProjectService projectService { get; set; }
        // GET: Admin/WorkPost
        [CheckPermission("WorkPost.Index")]
        public ActionResult Index(string keyword, int pageIndex = 1)
        {
            SearchOpt opt = new SearchOpt();
            opt.CurrentIndex = pageIndex;
            opt.Keyword = keyword;
            opt.PageSize = 10;
            ViewBag.pageIndex = pageIndex;
            ViewBag.keyword = keyword;

            var data = workPostService.Search(opt);

            return View(data);
        }
        [CheckPermission("WorkPost.Add")]
        [HttpGet]
        public ActionResult Add()
        {
            var data = projectService.GetAll();
            return View(data);
        }
        [CheckPermission("WorkPost.Add")]
        public ActionResult Add(WorkPostDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    ErrorMsg = MVCHelper.GetValidMsg(ModelState)
                });
            }

            var data = workPostService.Add(dto);

            if (data > 0)
            {
                return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "添加失败！" });
            }
        }
        [CheckPermission("WorkPost.StuList")]
        public ActionResult StuList(int id)
        {
            var data = userService.GetUserByPostId(id);
            return View(data);
        }
        [CheckPermission("WorkPost.StuPostList")]
        public ActionResult StuPostList()
        {

            var data= userService.GetAll().Where(e=>e.WorkPostId !=null);
            return View();
            
        }
        [CheckPermission("WorkPost.Delete")]
        public ActionResult Delete(int id)
        {
            workPostService.MarkDeleted(id);
            return Json(new AjaxResult { Status = "ok" });
        }
        [CheckPermission("WorkPost.Delete")]
        public ActionResult BatchDelete(int[] Ids)
        {
            foreach (var id in Ids)
            {
                workPostService.MarkDeleted(id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }

    }
}