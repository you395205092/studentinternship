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
    public class StuWorkPostController : Controller
    {
        public IStudentWorkPostService studentWorkPostService { get; set; }
        public IUserService userService { get; set; }
        public IUserDataMoreService userDataMoreService { get; set; }
        public IWorkPostService workPostService { get; set; }

        // GET: Admin/StuWorkPost

        [CheckPermission("StuWorkPost.Index")]
        public ActionResult Index()
        {
            var ids= studentWorkPostService.GetUserIdByState();
            List<UserMoreDTO> lst = new List<UserMoreDTO>();
            foreach (var id in ids)
            {
                var dto = userService.GetById(id);
                if (dto !=null)
                {
                    UserMoreDTO moredto = new UserMoreDTO();
                    moredto.BirthDay = dto.BirthDay;
                    moredto.CardId = dto.CardId;
                    moredto.Contact = dto.Contact;
                    moredto.CreateDateTime = dto.CreateDateTime;
                    moredto.Email = dto.Email;
                    moredto.EnglishLevel = dto.EnglishLevel;
                    moredto.ExamScore = dto.ExamScore;
                    moredto.Id = dto.Id;
                    moredto.Position = dto.Position;
                    moredto.Sex = dto.Sex;
                    moredto.State = dto.State;
                    moredto.TrueName = dto.TrueName;
                    moredto.UserName = dto.UserName;
                    moredto.WorkPostId = dto.WorkPostId;
                    moredto.WorkPostName = dto.Position;
                    moredto.Profile = userDataMoreService.GetByUserId(id).Profile;
                    moredto.PassportPic = userDataMoreService.GetByUserId(id).PassportPic;
                    moredto.DSUrl = userDataMoreService.GetByUserId(id).DSUrl;
                    lst.Add(moredto);
                }
                
            }
            var data = lst.ToArray();
            return View(data);
        }
        [CheckPermission("UpdateFile.UpdateFile")]
        [HttpGet]
        public ActionResult UpdateFile(int id)
        {
            ViewBag.username = id;
            return View();
        }
        [CheckPermission("UpdateFile.UpdateFile")]
        [HttpPost]
        public ActionResult UpdateFile(int userId,HttpPostedFileBase dsUrl)
        {
            string md5 = CommonHelper.CalcMD5(dsUrl.InputStream);
            string ext = Path.GetExtension(dsUrl.FileName);
            string path = "/UploadFile/file/" + DateTime.Now.ToString("yyyy/MM/dd") + "/" + md5 + ext;// /upload/2017/07/07/afadsfa.jpg
            //string thumbPath = "/UploadFile/image/" + DateTime.Now.ToString("yyyy/MM/dd") + "/" + md5 + "_thumb" + ext;
            string fullPath = HttpContext.Server.MapPath("~" + path);//d://22/upload/2017/07/07/afadsfa.jpg
                                                                     // string thumbFullPath = HttpContext.Server.MapPath("~" + thumbPath);
            new FileInfo(fullPath).Directory.Create();//尝试创建可能不存在的文件夹
            dsUrl.SaveAs(fullPath);

            int h1= userDataMoreService.AdminAdd(userId, path);
            if (h1>0)
            {
                return Content(CommonHelper.JumpAlert("上传成功", "/Admin/StuWorkPost"));
            }
            else
            {
                return Content(CommonHelper.JumpAlert("上传失败", "/Admin/StuWorkPost"));
            }
        }
    }
}