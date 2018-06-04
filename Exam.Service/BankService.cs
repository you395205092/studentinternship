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
    public class BankService : IBankService
    {
        public int Add(BankDTO dto)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BankEntity ef = new BankEntity();
                ef.Answer = dto.Answer;
                ef.ClassId = dto.ClassId;
                ef.ItemA = dto.ItemA;
                ef.ItemB = dto.ItemB;
                ef.ItemC = dto.ItemC;
                ef.ItemD = dto.ItemD;
                ef.ItemE = dto.ItemE;
                ef.Subject = dto.Subject;
                ctx.Banks.Add(ef);
                ctx.SaveChanges();
                return ef.Id;
            }
        }

        public int Edit(BankDTO dto)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<BankEntity> bs = new BaseService<BankEntity>(ctx);
                var data = bs.GetById(dto.Id);
                if (data == null)
                {
                    return 0;
                }
                data.Answer = dto.Answer;
                data.ClassId = dto.ClassId;
                data.ItemA = dto.ItemA;
                data.ItemB = dto.ItemB;
                data.ItemC = dto.ItemC;
                data.ItemD = dto.ItemD;
                data.ItemE = dto.ItemE;
                data.Subject = dto.Subject;
                ctx.SaveChanges();
                return data.Id;
            }
        }

        public BankDTO[] GetAll()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<BankEntity> bs = new BaseService<BankEntity>(ctx);
                var data = bs.GetAll();
                return data.ToList().Select(e => ToDTO(e)).ToArray();
            }
        }


        public BankDTO[] GetByClassId(int id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<BankEntity> bs = new BaseService<BankEntity>(ctx);
                var data = bs.GetAll().Where(p=>p.ClassId==id);
                return data.ToList().Select(e => ToDTO(e)).ToArray();
            }
        }



        public BankDTO ToDTO(BankEntity ef)
        {
            BankDTO dto = new BankDTO();
            dto.Answer = ef.Answer;
            dto.ClassId = ef.ClassId;
            dto.ClassName = ef.BankClass.Title;
            dto.CreateDateTime = ef.CreateDateTime;
            dto.Id = ef.Id;
            dto.ItemA = ef.ItemA;
            dto.ItemB = ef.ItemB;
            dto.ItemC = ef.ItemC;
            dto.ItemD = ef.ItemD;
            dto.ItemE = ef.ItemE;
            dto.Subject = ef.Subject;
            return dto;
        }
        public BankDTO GetById(int id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<BankEntity> bs = new BaseService<BankEntity>(ctx);

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
                BaseService<BankEntity> bs = new BaseService<BankEntity>(ctx);
                bs.MarkDeleted(id);
            }
        }

        public SearchBankResult Search(SearchBankOpt options)
        {
            using (MyDbContext ctx = new MyDbContext())
            {



                BaseService<BankEntity> bs = new BaseService<BankEntity>(ctx);


                var data = bs.GetAll();

                if (options.ClassId != null && options.ClassId != 9999)
                {
                    data = data.Where(t => t.ClassId == options.ClassId);
                }
                if (!string.IsNullOrEmpty(options.Keyword))
                {
                    data = data.Where(t => t.Subject.Contains(options.Keyword));
                }
                long totalCount = data.LongCount();//总搜索结果条数

                data = data.Include(e => e.BankClass).OrderByDescending(t => t.CreateDateTime).Skip((options.CurrentIndex - 1) * options.PageSize)
                    .Take(options.PageSize);
                SearchBankResult res = new SearchBankResult();
                res.totalCount = totalCount;

                List<BankDTO> banks = new List<BankDTO>();

                res.result = data.ToList().Select(e => ToDTO(e)).ToArray();

                BaseService<BankClassEntity> bs_bankclass = new BaseService<BankClassEntity>(ctx);

                var bankClasses = bs_bankclass.GetAll().ToList().Select(e => ToDTOClass(e)).ToArray();
                res.BankClass = bankClasses;
                return res;

            }
        }

        public BankClassDTO ToDTOClass(BankClassEntity ef)
        {
            BankClassDTO dto = new BankClassDTO();
            dto.CreateDateTime = ef.CreateDateTime;
            dto.Id = ef.Id;
            dto.Title = ef.Title;
            return dto;
        }
    }
}
