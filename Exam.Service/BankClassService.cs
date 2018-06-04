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
    public class BankClassService : IBankClassService
    {
        public int Add(string title)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BankClassEntity ef = new BankClassEntity();
                ef.Title = title;
                ctx.BankClasses.Add(ef);
                ctx.SaveChanges();
                return ef.Id;
            }
        }
        public BankClassDTO ToDTO(BankClassEntity ef)
        {
            BankClassDTO dto = new BankClassDTO();
            dto.Id = ef.Id;
            dto.CreateDateTime = ef.CreateDateTime;
            dto.Title = ef.Title;
            return dto;
        }
        public BankClassDTO[] GetAll()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<BankClassEntity> bs = new BaseService<BankClassEntity>(ctx);
                return bs.GetAll().ToList().Select(e => ToDTO(e)).ToArray();
            }
        }



        public BankClassDTO GetById(int id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<BankClassEntity> bs = new BaseService<BankClassEntity>(ctx);
                return ToDTO(bs.GetById(id));
            }
        }

        public void MarkDeleted(int id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<BankClassEntity> bs = new BaseService<BankClassEntity>(ctx);
                bs.MarkDeleted(id);
            }
        }

        public int Edit(int id, string title)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<BankClassEntity> bs = new BaseService<BankClassEntity>(ctx);
                var data= bs.GetById(id);
                if (data==null)
                {
                    return 0;
                }
                else
                {
                    data.Title = title;
                    ctx.SaveChanges();
                }
                return data.Id;
            }
        }
    }
}
