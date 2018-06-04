using Exam.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.DTO;
using Exam.Service.Entities;
using System.Data.Entity;

namespace Exam.Service
{
    class WorkPostService : IWorkPostService
    {
        public int Add(WorkPostDTO dto)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                WorkPostEntity ef = new WorkPostEntity();
                ef.Position = dto.Position;
                ef.Address = dto.Address;
                ef.CountNum = dto.CountNum;
                ef.Email = dto.Email;
                ef.Manager = dto.Manager;
                ef.Wage = dto.Wage;
                ef.ProjectId = dto.ProjectId;
                ef.Score = dto.Score;
                ctx.WorkPosts.Add(ef);
                ctx.SaveChanges();
                return ef.Id;
            }
        }

        public WorkPostDTO[] GetAll()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<WorkPostEntity> bs = new BaseService<WorkPostEntity>(ctx);

                var data = bs.GetAll().ToList().Select(e=>ToDTO(e));

                return data.ToArray();
            }
        }

        public WorkPostDTO GetById(int id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<WorkPostEntity> bs = new BaseService<WorkPostEntity>(ctx);

                var data = bs.GetAll().SingleOrDefault(h => h.Id == id);

                return ToDTO(data);
            }
        }

        public WorkPostDTO[] GetNumNotEmpty()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<WorkPostEntity> bs = new BaseService<WorkPostEntity>(ctx);

                var data = bs.GetAll().Where(e=>e.CountNum>0).ToList().Select(e => ToDTO(e));

                return data.ToArray();
            }
        }

        public void MarkDeleted(int id)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<WorkPostEntity> bs = new BaseService<WorkPostEntity>(ctx);
                bs.MarkDeleted(id);
            }
        }

        public SearchWorkPostResult Search(SearchOpt options)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<WorkPostEntity> bs = new BaseService<WorkPostEntity>(ctx);
                var items = bs.GetAll();
                items.Include(e => e.Project);
                if (!string.IsNullOrEmpty(options.Keyword))
                {
                    items = items.Where(e => e.Position == options.Keyword);
                }
                var totalCount = items.LongCount();
                items = items.Skip((options.CurrentIndex - 1) * options.PageSize)
                    .Take(options.PageSize);
                SearchWorkPostResult searchResult = new SearchWorkPostResult();
                searchResult.totalCount = totalCount;
                searchResult.result = items.ToList().Select(e => ToDTO(e)).ToArray();

                return searchResult;
            }
        }

        public WorkPostDTO ToDTO(WorkPostEntity ef)
        {
            WorkPostDTO dto = new WorkPostDTO();
            dto.Address = ef.Address;
            dto.CountNum = ef.CountNum;
            dto.CreateDateTime = ef.CreateDateTime;
            dto.Email = ef.Email;
            dto.Id = ef.Id;
            dto.Manager = ef.Manager;
            dto.Position = ef.Position;
            dto.Wage = ef.Wage;
            dto.ProjectId = ef.ProjectId;
            dto.ProjectTitle = ef.Project.Title;
            dto.Score = ef.Score;

            return dto;
        }

        public UserDTO ToUserDTO(UserEntity ef)
        {
            UserDTO dto = new UserDTO();

            dto.BirthDay = ef.BirthDay;
            dto.Contact = ef.Contact;
            dto.CreateDateTime = ef.CreateDateTime;
            dto.Email = ef.Email;
            dto.EnglishLevel = ef.EnglishLevel;
            dto.Id = ef.Id;
            dto.Sex = ef.Sex;
            dto.UserName = ef.UserName;
            dto.TrueName = ef.TrueName;
            dto.CardId = ef.CardId;
            return dto;
        }

        public int UpdateNum(int id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<WorkPostEntity> bs = new BaseService<WorkPostEntity>(ctx);

                var data = bs.GetAll().SingleOrDefault(h => h.Id == id);
                
                if (data.CountNum <= 0)
                {
                    throw new Exception("该岗位余数为0");
                }
                ctx.SaveChanges();
                return data.Id;
            }
        }
    }
}
