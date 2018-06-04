using Exam.IService;
using Exam.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.DTO;

namespace Exam.Service
{
    public class UserDataMoreService : IUserDataMoreService
    {
        public int Add(int userId,string Profile, string PassportPic)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                UserDataMoreEntity ef = new UserDataMoreEntity();
                ef.UserId = userId;
                ef.Profile = Profile;
                ef.PassportPic = PassportPic;
                ctx.UserDataMores.Add(ef);
                ctx.SaveChanges();
                return ef.Id;
            }
        }

        public int AdminAdd(int userId, string DSUrl)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<UserDataMoreEntity> bs = new BaseService<UserDataMoreEntity>(ctx);
                var data = bs.GetAll().Where(e => e.UserId == userId).FirstOrDefault();
                if (data == null)
                {
                    throw new NullReferenceException("找不到userId" + userId);
                }
                data.DSUrl = DSUrl;
                ctx.SaveChanges();
                return data.Id;
            }
        }

        public int Edit(int id,string Profile, string PassportPic)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<UserDataMoreEntity> bs = new BaseService<UserDataMoreEntity>(ctx);
                var data=bs.GetById(id);
                if (data==null)
                {
                    throw new NullReferenceException("找不到id" + id);
                }
                data.Profile = Profile;
                data.PassportPic = PassportPic;
                ctx.SaveChanges();
                return data.Id;
            }
            

        }

        public UserDataMoreDTO GetByUserId(int userId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<UserDataMoreEntity> bs = new BaseService<UserDataMoreEntity>(ctx);
                var data= bs.GetAll().Where(e => e.UserId == userId).FirstOrDefault();
                return ToDTO(data);
            }
        }

        public UserDataMoreDTO ToDTO(UserDataMoreEntity ef)
        {
            UserDataMoreDTO dto = new UserDataMoreDTO();
            dto.CreateDateTime = ef.CreateDateTime;
            dto.DSUrl = ef.DSUrl;
            dto.Id = ef.Id;
            dto.PassportPic = ef.PassportPic;
            dto.Profile = ef.Profile;
            dto.UserId = ef.UserId;
            return dto;
        }
    }
}
