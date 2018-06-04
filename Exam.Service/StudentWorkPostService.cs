using Exam.IService;
using Exam.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service
{
    class StudentWorkPostService : IStudentWorkPostService
    {
        public int Add(int userId, int WorkPostId)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                StudentWorkPostEntity ef = new StudentWorkPostEntity();
                ef.UserId = userId;
                ef.WorkPostId = WorkPostId;
                ef.State = 0; 
                ctx.StudentWorkPosts.Add(ef);
                ctx.SaveChanges();
                return ef.Id;
            }
        }

        public int[] GetUserIdByState()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<StudentWorkPostEntity> bs = new BaseService<StudentWorkPostEntity>(ctx);
                var data = bs.GetAll().Where(e => e.State == 1);
                return data.Select(e => e.UserId).ToArray();
            }
        }

        public int[] GetWorkPostIdByUserId(int userId)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<StudentWorkPostEntity> bs = new BaseService<StudentWorkPostEntity>(ctx);
                var data= bs.GetAll().Where(e=>e.UserId==userId);
                return data.Select(e=>e.WorkPostId).ToArray();
            }
        }

        public int UpdataState(int userId, int workPostId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<StudentWorkPostEntity> bs = new BaseService<StudentWorkPostEntity>(ctx);
                var data= bs.GetAll().Where(e => e.UserId == userId && e.WorkPostId == workPostId).FirstOrDefault();
                if (data==null)
                {
                    throw new NullReferenceException("找不到");
                }
                data.State = 1;
                ctx.SaveChanges();
                return data.Id;
            }
        }
    }
}
