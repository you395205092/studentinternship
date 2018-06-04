using Exam.DTO;
using Exam.IService;
using Exam.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service
{
    public class ProjectBMService : IProjectBMService
    {
        public int Add(int userId, int ProjectId)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                ProjectBMEntity ef = new ProjectBMEntity();
                ef.UserId = userId;
                ef.ProjectId = ProjectId;
                ctx.ProjectBMs.Add(ef);
                ctx.SaveChanges();
                return ef.Id;

            }
        }

        public int[]  GetByUserId(int ProjectId)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<ProjectBMEntity> bs = new BaseService<ProjectBMEntity>(ctx);
                var data = bs.GetAll().Where(e => e.ProjectId == ProjectId).ToList().Select(e=>e.UserId);
                return data.ToArray();
            }
        }


        public bool IsExist(int userId, int ProjectId)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<ProjectBMEntity> bs = new BaseService<ProjectBMEntity>(ctx);
                return bs.GetAll().Any(e => e.UserId == userId && e.ProjectId == e.ProjectId);
            }
        }

    }
}
