using Exam.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.DTO;
using Exam.Service.Entities;
using Exam.Common;

namespace Exam.Service
{
    class AdminService : IAdminService
    {
        public int Add(string adminName, string password,string TrueName)
        {
            AdminEntity admin = new AdminEntity();
            admin.AdminName = adminName;
            admin.TrueName = TrueName;
            string salt = CommonHelper.CreateVerifyCode(5);//盐
            admin.PasswordSalt = salt;
            //Md5(盐+用户密码)
            string pwdHash = CommonHelper.CalcMD5(salt + password);
            admin.PasswordHash = pwdHash;

            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminEntity> bs
                    = new BaseService<AdminEntity>(ctx);
                bool exists = bs.GetAll().Any(u => u.AdminName == adminName);
                if (exists)
                {
                    throw new ArgumentException("用户名已经存在" + adminName);
                }
                ctx.Admins.Add(admin);
                ctx.SaveChanges();
                return admin.Id;
            }
        }

        public bool CheckLogin(string adminName, string password)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminEntity> bs
                    = new BaseService<AdminEntity>(ctx);
                var admin = bs.GetAll().SingleOrDefault(u => u.AdminName == adminName);
                if (admin==null)
                {
                    return false;
                    
                }
                else
                {
                    string DbHash = admin.PasswordHash;
                    string adminHash = CommonHelper.CalcMD5(admin.PasswordSalt + password);
                   
                    return DbHash.ToUpper() == adminHash.ToUpper();
                }

            }
        }

        public AdminDTO[] GetAll()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminEntity> bs = new BaseService<AdminEntity>(ctx);
                return bs.GetAll().ToList().Select(e => ToDTO(e)).ToArray();
            }
        }

        public AdminDTO ToDTO(AdminEntity ef)
        {
            AdminDTO dto = new AdminDTO();
            dto.AdminName = ef.AdminName;
            dto.TrueName = ef.TrueName;
            dto.CreateDateTime = ef.CreateDateTime;
            dto.Id = ef.Id;
            dto.LastDateTime = ef.LastDateTime;
            dto.LoginIP = ef.LoginIP;
            dto.LoginTimes = ef.LoginTimes;
            return dto;
        }

        public AdminDTO GetById(int id)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<AdminEntity> bs = new BaseService<AdminEntity>(ctx);
                var admin = bs.GetById(id);
                if (admin == null)
                {
                    return null;
                }
                return ToDTO(admin);

            }
        }

        public void MarkDeleted(int id)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<AdminEntity> bs = new BaseService<AdminEntity>(ctx);
                bs.MarkDeleted(id);
            }
        }

        public bool Edit(int id,string passWord)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<AdminEntity> bs = new BaseService<AdminEntity>(ctx);
                var admin= bs.GetById(id);
                if (admin==null)
                {
                    return false;
                }
                else
                {
                    string salt = admin.PasswordSalt;
                    admin.PasswordHash = CommonHelper.CalcMD5(salt + passWord);
                    ctx.SaveChanges();
                    return true;
                }
                
            }
        }
    }
}
