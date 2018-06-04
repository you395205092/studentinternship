using Exam.Common;
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
    public class UserService : IUserService
    {
        public IWorkPostService workPostService { get; set; }
        public IProjectService projectService { get; set; }

        public bool IsExist(string userName)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<UserEntity> bs
                    = new BaseService<UserEntity>(ctx);
                var user = bs.GetAll().Any(u => u.UserName == userName);
                return user;

            }
        }
        public int AddUser(UserAddDTO dto)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<UserEntity> bs = new BaseService<UserEntity>(ctx);
                bool exists = bs.GetAll().Any(e => e.UserName == dto.UserName);
                if (exists)
                {
                    throw new ArgumentException("用户名已经存在" + dto.UserName);
                }
                UserEntity ef = new UserEntity();
                ef.UserName = dto.UserName;
                ef.TrueName = dto.TrueName;
                ef.CardId = dto.CardId;
                string Salt = CommonHelper.CreateVerifyCode(5);
                ef.PasswordSalt = Salt;
                ef.PasswordHash = CommonHelper.CalcMD5(Salt + dto.PassWord);
                ef.Sex = dto.Sex;
                ef.BirthDay = dto.BirthDay;
                ef.EnglishLevel = dto.EnglishLevel;
                ef.Email = dto.Email;
                ef.Contact = dto.Contact;
                ef.CreateDateTime = DateTime.Now;
                ef.State = 1;
                ctx.Users.Add(ef);
                ctx.SaveChanges();
                return ef.Id;

            }
        }

        public bool CheckLogin(string userName, string password)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<UserEntity> bs
                    = new BaseService<UserEntity>(ctx);
                var user = bs.GetAll().SingleOrDefault(u => u.UserName == userName);
                if (user == null)
                {
                    return false;

                }
                else
                {
                    string DbHash = user.PasswordHash;
                    string userHash = CommonHelper.CalcMD5(user.PasswordSalt + password);
                    return DbHash.ToUpper() == userHash.ToUpper();
                }

            }
        }

        public UserDTO ToDTO(UserEntity ef)
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
            dto.WorkPostId = ef.WorkPostId;
            dto.State = ef.State;
            dto.ExamScore = ef.ExamScore;
            if (ef.WorkPostId !=null)
            {
                dto.Address = workPostService.GetById(ef.WorkPostId.Value).Address;
                dto.Position = workPostService.GetById(ef.WorkPostId.Value).Position;
                dto.ProjectName = projectService.GetById(workPostService.GetById(ef.WorkPostId.Value).ProjectId).Title;
            }

            return dto;
        }
        public UserDTO[] GetAll()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<UserEntity> bs = new BaseService<UserEntity>(ctx);
                var data = bs.GetAll();

                return data.ToList().Select(e => ToDTO(e)).ToArray();
            }

        }

        public UserDTO GetById(int id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<UserEntity> bs = new BaseService<UserEntity>(ctx);
                var data = bs.GetById(id);
                if (data == null)
                {
                    return null;
                }
                return ToDTO(data);
            }
        }

        public void MarkDeleted(int id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<UserEntity> bs = new BaseService<UserEntity>(ctx);
                bs.MarkDeleted(id);
            }
        }

        public SearchUserResult Search(SearchOpt options)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<UserEntity> bs = new BaseService<UserEntity>(ctx);
                var items = bs.GetAll();
                if (!string.IsNullOrWhiteSpace(options.Keyword))
                {
                    items.Where(e => e.UserName.Contains(options.Keyword));
                }

                SearchUserResult res = new SearchUserResult();
                res.totalCount = items.LongCount();
                res.result = items.ToList().Select(e => ToDTO(e)).ToArray();
                return res;

            }
        }

        public int Edit(UserAddDTO dto)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<UserEntity> bs = new BaseService<UserEntity>(ctx);
                var data = bs.GetById(dto.Id);
                if (data == null)
                {
                    throw new ArgumentException("找不到该学生" + dto.Id);
                }
                data.TrueName = dto.TrueName;
                data.Sex = dto.Sex;
                data.BirthDay = dto.BirthDay;
                data.CardId = dto.CardId;
                data.Contact = dto.Contact;
                data.Email = dto.Email;
                data.EnglishLevel = dto.EnglishLevel;
                data.ExamScore = dto.ExamScore;
                data.PasswordHash = CommonHelper.CalcMD5(data.PasswordSalt + dto.PassWord);
                data.State = dto.State;
                ctx.SaveChanges();
                return data.Id;
            }
        }

        public UserDTO[] GetUserByPostId(int id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<WorkPostEntity> bs = new BaseService<WorkPostEntity>(ctx);
                var data=bs.GetById(id);
                BaseService<UserEntity> bsUser = new BaseService<UserEntity>(ctx);
                return bsUser.GetAll().Where(e => e.WorkPostId == data.Id).ToList().Select(e => ToDTO(e)).ToArray();
            }
        }
        public UserDTO[] GetUserIsWork()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<UserEntity> bs = new BaseService<UserEntity>(ctx);
                return bs.GetAll().Where(e => e.WorkPostId == null).ToList().Select(e=>ToDTO(e)).ToArray();
                
                
            }
        }

        public int UpdateState(int userId, int state)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<UserEntity> bs = new BaseService<UserEntity>(ctx);
                var data = bs.GetById(userId);
                if (data == null)
                {
                    throw new ArgumentException("找不到该学生" + userId);
                }
                
                data.State = state;
                ctx.SaveChanges();
                return data.Id;
            }
        }

        public int UpdateExamScore(int userId, int score)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<UserEntity> bs = new BaseService<UserEntity>(ctx);
                var data = bs.GetById(userId);
                if (data == null)
                {
                    throw new ArgumentException("找不到该学生" + userId);
                }

                data.ExamScore = score;
                ctx.SaveChanges();
                return data.Id;
            }
        }

        public int UpdateWorkPost(int userId, int workPostId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<UserEntity> bs = new BaseService<UserEntity>(ctx);
                var data = bs.GetById(userId);
                if (data == null)
                {
                    throw new ArgumentException("找不到该学生" + userId);
                }

                data.WorkPostId = workPostId;
                ctx.SaveChanges();
                return data.Id;
            }
        }
    }
}
