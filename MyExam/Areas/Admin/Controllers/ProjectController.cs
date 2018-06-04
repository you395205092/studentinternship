using Exam.Common;
using Exam.CommonMVC;
using Exam.DTO;
using Exam.IService;
using MyExam.App_Start;
using MyExam.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyExam.Areas.Admin.Controllers
{
    public class ProjectController : Controller
    {
        public IProjectService projectService { get; set; }
        public IProjectBMService projectBMService { get; set; }
        public IUserService userService { get; set; }
        public IStuPaperService stuPaperService { get; set; }
        public IWorkPostService workPostService { get; set; }
        public IStudentWorkPostService studentWorkPostService { get; set; }
        public IPaperService paperService { get; set; }


        public IBankClassService bankClassService { get; set; }
        // GET: Admin/Project

        [CheckPermission("Project.Index")]
        public ActionResult Index()
        {
            var data = projectService.GetAll();
            return View(data);
        }
        [CheckPermission("Project.Add")]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [CheckPermission("Project.Add")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(ProjectAddDTO dto)
        {
           
            /*
            string md5 = CommonHelper.CalcMD5(dto.fileUrl.InputStream);
            string ext = Path.GetExtension(dto.fileUrl.FileName);
            string path = "/UploadFile/image/" + DateTime.Now.ToString("yyyy/MM/dd") + "/" + md5 + ext;// /upload/2017/07/07/afadsfa.jpg
            //string thumbPath = "/UploadFile/image/" + DateTime.Now.ToString("yyyy/MM/dd") + "/" + md5 + "_thumb" + ext;
            string fullPath = HttpContext.Server.MapPath("~" + path);//d://22/upload/2017/07/07/afadsfa.jpg
           // string thumbFullPath = HttpContext.Server.MapPath("~" + thumbPath);
            new FileInfo(fullPath).Directory.Create();//尝试创建可能不存在的文件夹
            dto.fileUrl.SaveAs(fullPath);*/

            ProjectDTO dtoPro = new ProjectDTO();
            dtoPro.enTitle = dto.enTitle;
            dtoPro.Remark = dto.Remark;
            dtoPro.SubTitle = dto.SubTitle;
            dtoPro.Title = dto.Title;
            //dtoPro.PicUrl = path; 
            var data = projectService.Add(dtoPro);
            if (data > 0)
            {
                return RedirectToAction("Index");
                
            }
            else
            {
                return RedirectToAction("Add");
            }
        }
        [HttpGet]
        [CheckPermission("Project.Edit")]
        public ActionResult Edit(int id)
        {
            var data = projectService.GetById(id);
            return View(data);
        }
        [HttpPost]
        [CheckPermission("Project.Edit")]
        [ValidateInput(false)]
        public ActionResult Edit(ProjectDTO dto)
        {
            
            //dtoPro.PicUrl = path; 
            var data = projectService.Edit(dto);
            if (data > 0)
            {
                return Json(new AjaxResult { Status="ok"});
                //return Content("<script>alert('修改成功';window.location.href='/Admin/Project/')</script>");
                ///return RedirectToAction("Index");

            }
            else
            {
                return Json(new AjaxResult { Status = "error",ErrorMsg="修改失败" });
               // return Content("<script>alert('修改失败';window.location.href='/Admin/Project/')</script>");
                //return RedirectToAction("Index");
            }
        }

        [CheckPermission("Project.Student")]
        public ActionResult Student(int id)
        {
            var userids= projectBMService.GetByUserId(id);
            List<UserDTO> lst = new List<UserDTO>();
            foreach (var item in userids)
            {
                var data= userService.GetById(item);
                lst.Add(data);
            }
            ViewBag.ProjectId = id;


            //data.ExamScore= 
            return View(lst.ToArray());
        }

        public ActionResult StudentWorkPost(int userid,int projectId)
        {
            var user = userService.GetById(userid);
            if (user.State != 3)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "该学生还没参加考试，或者已经分配过岗位！" });
            }
            int score = user.ExamScore.Value;
            var workPosts=workPostService.GetAll().Where(e => e.ProjectId == projectId && e.CountNum > 0 && e.Score <= score);
            if (workPosts.Count()<=0)
            {
                return Content("<script>alert('分配失败，没有岗位可能选择！');window.location.href='/Admin/Project/Student/" + projectId + "'</script>");
            }
            foreach (var workPost in workPosts)
            {
                studentWorkPostService.Add(userid, workPost.Id);
            }

            userService.UpdateState(userid, 4);

            return Content("<script>alert('分配成功！');window.location.href='/Admin/Project/Student/" + projectId + "'</script>");
        }

        public ActionResult StuPaper(int id)
        {
            var papers= paperService.GetAll().OrderByDescending(e=>e.CreateDateTime).FirstOrDefault();
            var data= stuPaperService.GetAll(papers.Id, id);
            ViewBag.zf = data.Sum(e => e.Score);
            return View(data);
        }

        /*
        [CheckPermission("Project.StudentWorkPost")]
        [HttpGet]
        public ActionResult StudentWorkPost(int userid,int projectId)
        {
            ViewData["UserName"] = userid;
            ViewData["TrueName"] = userService.GetById(userid).UserName;
            var data = workPostService.GetAll().Where(e=>e.ProjectId==projectId && e.CountNum>0).ToArray();
            return View(data);
        }

        [CheckPermission("Project.StudentWorkPost")]
        [HttpPost]
        public ActionResult StudentWorkPost(int userid, int[] ids)
        {
            if (ids.Length !=4)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "只能和必须选择四个岗位！" });
            }
            if (userService.GetById(userid).State !=3)
            {
                return Json(new AjaxResult { Status = "error",ErrorMsg="该学生还没参加考试，或者已经分配过岗位！" });
            }

            foreach (var id in ids)
            {
                studentWorkPostService.Add(userid, id);
            }
            userService.UpdateState(userid, 4);
            return Json(new AjaxResult { Status = "ok" });
        }*/
    }
}