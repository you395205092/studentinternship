using Exam.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.DTO;
using Exam.Service.Entities;

namespace Exam.Service
{
    public class ProjectService : IProjectService
    {
        public int Add(ProjectDTO dto)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                ProjectEntity ef = new ProjectEntity();
                ef.Title = dto.Title;
                ef.enTitle = dto.enTitle;
                ef.SubTitle = dto.SubTitle;
                ef.Remark = dto.Remark;
                ef.PicUrl = dto.PicUrl;
                ctx.Projects.Add(ef);
                ctx.SaveChanges();
                return ef.Id;
            }
        }

        public ProjectDTO[] GetAll()
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<ProjectEntity> bs = new BaseService<ProjectEntity>(ctx);
                return bs.GetAll().ToList().Select(e=>ToDTO(e)).ToArray();
            }
        }

        public ProjectDTO ToDTO(ProjectEntity ef)
        {
            ProjectDTO dto = new ProjectDTO();
            dto.Id = ef.Id;
            dto.CreateDateTime = ef.CreateDateTime;
            dto.enTitle = ef.enTitle;
            dto.Remark = ef.Remark;
            dto.SubTitle = ef.SubTitle;
            dto.Title = ef.Title;
            dto.PicUrl = ef.PicUrl;
            return dto;
        }

        public ProjectDTO GetById(int id)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<ProjectEntity> bs = new BaseService<ProjectEntity>(ctx);
                var data= bs.GetById(id);
                return ToDTO(data);
            }
        }

        public void MarkDeleted(int id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<ProjectEntity> bs = new BaseService<ProjectEntity>(ctx);
                bs.MarkDeleted(id);
            }
        }

        public int Edit(ProjectDTO dto)
        {
            using (MyDbContext ctx = new MyDbContext())
            {

                BaseService<ProjectEntity> bs = new BaseService<ProjectEntity>(ctx);
                var data= bs.GetById(dto.Id);
                if (data ==null)
                {
                    throw new ArgumentNullException("异常" + dto.Id);
                }
                data.Title = dto.Title;
                data.enTitle = dto.enTitle;
                data.SubTitle = dto.SubTitle;
                data.Remark = dto.Remark;
                data.PicUrl = dto.PicUrl;
                ctx.SaveChanges();
                return data.Id;
            }
        }
    }
}
