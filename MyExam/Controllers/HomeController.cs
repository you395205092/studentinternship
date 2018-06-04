using CaptchaGen;
using Exam.Common;
using Exam.CommonMVC;
using Exam.DTO;
using Exam.IService;
using Exam.Service;
using log4net;
using MyExam.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyExam.Controllers
{

    public class HomeController : Controller
    {
        private static ILog log = LogManager.GetLogger(typeof(HomeController));
        public IUserService userService { get; set; }
        public IBankService bankService { get; set; }
        public IBankClassService bankClassService { get; set; }
        public IPaperService paperService { get; set; }
        public IStuPaperService stuPaperService { get; set; }
        public IProjectService projectService { get; set; }
        public IProjectBMService projectBMService { get; set; }
        public IStudentWorkPostService studentWorkPostService { get; set; }
        public IWorkPostService workPostService { get; set; }
        public IUserDataMoreService userDataMoreService { get; set; }
        public IMessageService messageService { get; set; }
        // GET: Home
        public ActionResult Index()
        {
            var data=userService.GetAll();
            return View(data);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserAddDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    ErrorMsg = MVCHelper.GetValidMsg(ModelState)
                });
            }
            var yzm = (string)TempData["verifyCodeHome"];
            if (dto.Verify != yzm)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "验证码错误！" });
            }

            var isOk = userService.IsExist(dto.UserName);
            if (isOk)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "账号已存在！" });
            }

            var data = userService.AddUser(dto);

            if (data > 0)
            {
                return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "添加失败！" });
            }
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Login(string username, string password, string Verify)
        {
            

                if (!ModelState.IsValid)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    ErrorMsg = MVCHelper.GetValidMsg(ModelState)
                });
            }
            var yzm = (string)TempData["verifyCodeHome"];
            if (Verify != yzm)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "验证码错误！" });
            }

            if (userService.CheckLogin(username, password))
            {
                var user = userService.GetAll().FirstOrDefault(e => e.UserName == username);
                CommonHelper.WriteCookie("UserLogin", DESEncrypt.Encrypt(JsonConvert.SerializeObject(user)), 7200);
                return Json(new AjaxResult { Status = "ok" });

            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "账号密码错误！" });

            }
        }
        public ActionResult UserAbout()
        {
            var user = AdminHelper.GetUserId(HttpContext);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var data = userService.GetById(user.Id);
            return View(data);
        }
        public ActionResult Project()
        {
            var user = AdminHelper.GetUserId(HttpContext);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            if (userService.GetById(user.Id).State != 1)
            {
                return Content(CommonHelper.JumpAlertState(userService.GetById(user.Id).State.Value));
            }
            var data= projectService.GetAll();
            return View(data);
        }
        
        public ActionResult ProjectShow(int id)
        {
            var user = AdminHelper.GetUserId(HttpContext);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            var data=  projectService.GetById(id);
            return View(data);
        }
        [HttpPost]
        public ActionResult ProjectBM(int id)
        {
            var user = AdminHelper.GetUserId(HttpContext);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            
            projectBMService.Add(user.Id, id);
            userService.UpdateState(user.Id,2);

            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpGet]
        public ActionResult Academy()
        {
            var user = AdminHelper.GetUserId(HttpContext);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            if (userService.GetById(user.Id).State != 1)
            {
                return Content(CommonHelper.JumpAlertState(userService.GetById(user.Id).State.Value));
            }

            var data= projectService.GetAll();
            return View(data);
        }

        public int GetScore(int PaperId)
        {

            return stuPaperService.GetScore(PaperId,AdminHelper.GetUserId(HttpContext).Id);
        }

        public ActionResult PaperList()
        {
            var user = AdminHelper.GetUserId(HttpContext);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            if (userService.GetById(user.Id).State != 2)
            {
                return Content(CommonHelper.JumpAlertState(userService.GetById(user.Id).State.Value));
            }
            /*
            PaperListDTO data = new PaperListDTO();
            data.paperdto = paperService.GetAll(); ;
            var paperids = paperService.GetAll();
            List<int> lst = new List<int>();
            foreach (var item in paperids)
            {
                lst.Add(GetScore(item.Id));
            }
            data.Score = lst.ToArray();
            */
            var djg= paperService.GetAll().OrderByDescending(e => e.CreateDateTime).FirstOrDefault();
            //return Redirect("/Home/Paper/"+ djg.Id);
            return View(djg);
        }



        public ActionResult Paper(int id)
        {
            /*
            var score= stuPaperService.GetScore(id, AdminHelper.GetUserId(HttpContext).Id);
            if (score>0)
            {
                return Content("<script>alert('你已经考过了');location.href='/Home/PaperList'</script>");
            }*/
            //log.Debug("id" + id);

            var user = AdminHelper.GetUserId(HttpContext);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            if (userService.GetById(user.Id).State != 2)
            {
                return Content(CommonHelper.JumpAlertState(userService.GetById(user.Id).State.Value));
            }
            

           // var data1= paperService.GetPaperSJ(id,0);
            //var data2 = paperService.GetPaperSJ(id, 1);

            var xx= bankClassService.GetAll();
            List<BankDTO> lst = new List<BankDTO>();

            for (int i = 0; i < xx.Length; i++)
            {
                var datayou = paperService.GetPaperSJ(id, i,xx[i].Id);
                foreach (var item in datayou)
                {
                    lst.Add(item);
                }
            }
            
            var data = lst.ToArray();
            ViewBag.paperid = id;
            return View(data);
        }
        [HttpPost]
        public ActionResult PaperUpdate(string selectedIds,string answers,string classids,int paperid)
        {
            var user = AdminHelper.GetUserId(HttpContext);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            var ids = selectedIds.Split(',');
            var an = answers.Split(',');
            var classidyou = classids.Split(',');

            int zf = 0;
            List<StuPaperDTO> lst = new List<StuPaperDTO>();
            
            for (int i = 0; i < ids.Length; i++)
            {
                StuPaperDTO dto = new StuPaperDTO();
                dto.Answer = an[i];
                dto.BankId = Convert.ToInt32(ids[i]);
                dto.PaperId = paperid;
                if (classidyou[i]=="1")
                {
                    var fs = Convert.ToInt16(paperService.GetPaperById(paperid).PScore.Split(',')[0]);
                    if (dto.Answer==bankService.GetById(Convert.ToInt16(ids[i])).Answer)
                    {
                        dto.Score = fs;
                        zf += fs;
                    }
                    else
                    {
                        dto.Score = 0; 
                    }
                }
                else
                {
                    var fs = Convert.ToInt16(paperService.GetPaperById(paperid).PScore.Split(',')[1]);
                    if (dto.Answer == bankService.GetById(Convert.ToInt16(ids[i])).Answer)
                    {
                        dto.Score = fs;
                        zf += fs;
                    }
                    else
                    {
                        dto.Score = 0;
                    }
                }
                dto.UserId = AdminHelper.GetUserId(HttpContext).Id;
                lst.Add(dto);

            }
            stuPaperService.Add(lst.ToArray());

            userService.UpdateExamScore(user.Id, zf);
            userService.UpdateState(user.Id, 3);

            return Json(new AjaxResult { Status = "ok" });

        }
        public ActionResult UserIndex()
        {
            var user = AdminHelper.GetUserId(HttpContext);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            if (userService.GetById(user.Id).State != 8)
            {
                return Content(CommonHelper.JumpAlertState(userService.GetById(user.Id).State.Value));
            }

            return View();
        }

        public ActionResult WorkPost()
        {
            var user= AdminHelper.GetUserId(HttpContext);

            if (userService.GetById(user.Id).State != 3 && userService.GetById(user.Id).State != 4)
            {
                return Content(CommonHelper.JumpAlertState(userService.GetById(user.Id).State.Value));
            }
            ViewBag.ExamScore= userService.GetById(user.Id).ExamScore;

            var data=studentWorkPostService.GetWorkPostIdByUserId(user.Id);
            List<WorkPostDTO> lst = new List<WorkPostDTO>();
            foreach (var id in data)
            {
               var dto= workPostService.GetById(id);
                lst.Add(dto);
            }

            return View(lst.ToArray());
        }
        [HttpGet]
        public ActionResult WorkPostUpdate(int id)
        {
            var user = AdminHelper.GetUserId(HttpContext);
            int h1= studentWorkPostService.UpdataState(user.Id, id);

            var data = workPostService.GetById(id);
            if (data.CountNum<=0)
            {
                return Content("<script>alert('选择失败,岗位已满。');window.location.href='/Home/WorkPost'</script>");
            }
            if (h1>0)
            {
                userService.UpdateState(user.Id, 5);
                userService.UpdateWorkPost(user.Id, id);
                int h2=workPostService.UpdateNum(id);
                if (h2>0)
                {
                    //return Json(new AjaxResult { Status = "ok" });
                    return Content("<script>alert('选择成功。');window.location.href='/Home/UserDataMore'</script>");
                }
                else
                {
                    return Content("<script>alert('选择失败。');window.location.href='/Home/WorkPost'</script>");
                }
                
            }
            else
            {
                //return Json(new AjaxResult { Status = "error",Data="选择失败" });
                return Content("<script>alert('选择失败。');window.location.href='/Home/WorkPost'</script>");
            }
            
        }
        [HttpGet]
        public ActionResult UserDataMore()
        {
            var user = AdminHelper.GetUserId(HttpContext);
            if (userService.GetById(user.Id).State != 5)
            {
                return Content(CommonHelper.JumpAlertState(userService.GetById(user.Id).State.Value));
            }


            return View();
        }
        [HttpPost]
        public ActionResult UserDataMore(HttpPostedFileBase profile, HttpPostedFileBase passportPic)
        {
            var user= AdminHelper.GetUserId(HttpContext);
            if (user==null)
            {
                return RedirectToAction("Login");
            }
            string md5 = CommonHelper.CalcMD5(profile.InputStream);
            string ext = Path.GetExtension(profile.FileName);
            string path = "/UploadFile/file/" + DateTime.Now.ToString("yyyy/MM/dd") + "/" + md5 + ext;// /upload/2017/07/07/afadsfa.jpg
            //string thumbPath = "/UploadFile/image/" + DateTime.Now.ToString("yyyy/MM/dd") + "/" + md5 + "_thumb" + ext;
            string fullPath = HttpContext.Server.MapPath("~" + path);//d://22/upload/2017/07/07/afadsfa.jpg
                                                                     // string thumbFullPath = HttpContext.Server.MapPath("~" + thumbPath);
            new FileInfo(fullPath).Directory.Create();//尝试创建可能不存在的文件夹
            profile.SaveAs(fullPath);


            string md52 = CommonHelper.CalcMD5(passportPic.InputStream);
            string ext2 = Path.GetExtension(passportPic.FileName);
            string path2 = "/UploadFile/image/" + DateTime.Now.ToString("yyyy/MM/dd") + "/" + md52 + ext2;// /upload/2017/07/07/afadsfa.jpg
            //string thumbPath = "/UploadFile/image/" + DateTime.Now.ToString("yyyy/MM/dd") + "/" + md5 + "_thumb" + ext;
            string fullPath2 = HttpContext.Server.MapPath("~" + path2);//d://22/upload/2017/07/07/afadsfa.jpg
                                                                     // string thumbFullPath = HttpContext.Server.MapPath("~" + thumbPath);
            new FileInfo(fullPath2).Directory.Create();//尝试创建可能不存在的文件夹
            passportPic.SaveAs(fullPath2);

            int h1= userDataMoreService.Add(user.Id,path, path2);
            if (h1 > 0)
            {
                userService.UpdateState(user.Id, 6);
                //return Json(new AjaxResult { Status = "ok" });
                return Content("<script>alert('上传成功。');window.location.href='/Home/IsUsa'</script>");
            }
            else
            {
                //return Json(new AjaxResult { Status = "error",Data="选择失败" });
                return Content("<script>alert('上传失败。');window.location.href='/Home/UserDataMore'</script>");
            }

        }
        public ActionResult DownLoad()
        {
            var user = AdminHelper.GetUserId(HttpContext);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            if (userService.GetById(user.Id).State != 6 )
            {
                return Content(CommonHelper.JumpAlertState(userService.GetById(user.Id).State.Value));
            }
            var data = userDataMoreService.GetByUserId(user.Id);
           
            if (string.IsNullOrEmpty(data.DSUrl))
            {
                return Content("<script>alert('请等待老师上传DS文件！');window.location.href='/Home/IsUsa'</script>");
            }
            userService.UpdateState(user.Id, 7);
            var kzm=data.DSUrl.Split('.')[1];
            return File(data.DSUrl, "application/ms-word","ds表格.doc");
        }
        [HttpGet]
        public ActionResult IsUsa()
        {
            var user = AdminHelper.GetUserId(HttpContext);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            if (userService.GetById(user.Id).State != 6  && userService.GetById(user.Id).State != 7)
            {
                return Content(CommonHelper.JumpAlertState(userService.GetById(user.Id).State.Value));
            }
            ViewBag.State = userService.GetById(user.Id).State;
            var data= userDataMoreService.GetByUserId(user.Id);
            return View(data);
        }

       
        [HttpPost]
        public ActionResult IsUsaOK()
        {
            var user = AdminHelper.GetUserId(HttpContext);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            if (userService.GetById(user.Id).State != 7)
            {
                return Content("<script>alert('请先下载DS文件！');window.location.href='/Home/IsUsa'</script>");
            }
            userService.UpdateState(user.Id, 8);
            return Content("<script>alert('您已经抵达美国，如有问题留言咨询');window.location.href='/Home/Message'</script>");
        }

        [HttpGet]
        public ActionResult Message()
        {
            var user = AdminHelper.GetUserId(HttpContext);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            if (userService.GetById(user.Id).State != 8)
            {
                return Content(CommonHelper.JumpAlertState(userService.GetById(user.Id).State.Value));
            }
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Message(MessageDTO dto)
        {
            var user = AdminHelper.GetUserId(HttpContext);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            dto.UserId = user.Id;
            var h1 = messageService.Add(dto);
            if (h1 > 0)
            {
                //return Json(new AjaxResult { Status = "ok" });
                return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                //return Json(new AjaxResult { Status = "error",Data="选择失败" });
                return Json(new AjaxResult { Status = "error", Data = "留言失败" });
            }

        }
        public ActionResult MessageList()
        {
            var user = AdminHelper.GetUserId(HttpContext);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            if (userService.GetById(user.Id).State != 8)
            {
                return Content(CommonHelper.JumpAlertState(userService.GetById(user.Id).State.Value));
            }


            var data = messageService.GetByUserId(user.Id);
            return View(data);
        }
        public ActionResult MessageShow(int id)
        {
            var user = AdminHelper.GetUserId(HttpContext);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            if (userService.GetById(user.Id).State != 8)
            {
                return Content(CommonHelper.JumpAlertState(userService.GetById(user.Id).State.Value));
            }
            var data= messageService.GetById(id);
            return View(data);
        }
        public ActionResult BackHomeOk()
        {
            var user = AdminHelper.GetUserId(HttpContext);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            if (userService.GetById(user.Id).State != 8)
            {
                return Content(CommonHelper.JumpAlertState(userService.GetById(user.Id).State.Value));
            }
            userService.UpdateState(user.Id, 9);
            return RedirectToAction("BackHome");
        }
        public ActionResult BackHome()
        {
            return View();
        }

        public ActionResult CreateVerifyCode()
        {
            string verifyCode = CommonHelper.CreateVerifyCode(4);
            TempData["verifyCodeHome"] = verifyCode;
            //Session["verifyCode"] = verifyCode;
            /*
            using (MemoryStream ms = ImageFactory.GenerateImage(verifyCode, 60, 100, 20, 6))
            {
                return File(ms, "image/jpeg");
            }*/
            ImageFactory.BackgroundColor = Color.AliceBlue;
            MemoryStream ms = ImageFactory.GenerateImage(verifyCode, 60, 100, 20, 6);
            return File(ms, "image/jpeg");
        }

        public static string GetState(int? id)
        {
            string str = "";
            //0  1报名  2考试  3老师提供岗位 4已经选择岗位 5上传签证信息  6抵美  7回国
            switch (id)
            {
                case 2:
                    str = "已报名";
                    break;
                case 3:
                    str = "已经完成测试";
                    break;
                case 4:
                    str = "老师已分配岗位";
                    break;
                case 5:
                    str = "已经选择岗位";
                    break;
                case 6:
                    str = "已经选择签证";
                    break;
                case 7:
                    str = "您已经下载DS表";
                    break;
                case 8:
                    str = "您已经抵达美国";
                    break;
                case 9:
                    str = "您已经回国";
                    break;
                default:
                    str = "刚注册";
                    break;
            }
            return str;
        }
        
    }
}