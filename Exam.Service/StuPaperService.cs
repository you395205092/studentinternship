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
    public class StuPaperService : IStuPaperService
    {
        public IPaperService paperService { get; set; }
        public IBankService bankService { get; set; }
        public IBankClassService bankClassService { get; set; }
        public void Add(StuPaperDTO[] dto)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                


                List<StuPaperEntity> lst = new List<StuPaperEntity>();
                foreach (var item in dto)
                {
                    StuPaperEntity ef = new StuPaperEntity();
                    ef.Answer = item.Answer;
                    ef.BankId = item.BankId;
                    ef.PaperId = item.PaperId;
                    ef.UserId = item.UserId;
                    ef.Score = item.Score;
                    lst.Add(ef);
                }
                //ctx.StuPapers.Add(ef);
                ctx.StuPapers.AddRange(lst);
                ctx.SaveChanges();
            }
        }

        public StuPaperShowDTO[] GetAll(int PaperId, int UserId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<StuPaperEntity> bs = new BaseService<StuPaperEntity>(ctx);
                var data = bs.GetAll().Where(e => e.PaperId == PaperId && e.UserId == UserId);
                List<StuPaperShowDTO> lst = new List<StuPaperShowDTO>();
                foreach (var ef in data)
                {
                    StuPaperShowDTO dto = new StuPaperShowDTO();
                    var bankdto = bankService.GetById(ef.BankId);
                    dto.ItemA = bankdto.ItemA;
                    dto.ItemB = bankdto.ItemB;
                    dto.ItemC = bankdto.ItemC;
                    dto.ItemD = bankdto.ItemD;
                    dto.TrueAnswer = bankdto.Answer;
                    dto.Subject = bankdto.Subject;
                    dto.Title = bankClassService.GetById(bankdto.ClassId).Title;
                    dto.StuAnswer = ef.Answer;
                    dto.Score = ef.Score;
                    lst.Add(dto);
                }

                return lst.ToArray();


            }
        }
        public StuPaperShowDTO ToDTO(StuPaperEntity ef)
        {
            StuPaperShowDTO dto = new StuPaperShowDTO();
            var bankdto = bankService.GetById(ef.BankId);
            dto.ItemA = bankdto.ItemA;
            dto.ItemB = bankdto.ItemB;
            dto.ItemC = bankdto.ItemC;
            dto.ItemD = bankdto.ItemD;
            dto.TrueAnswer = dto.TrueAnswer;
            dto.Subject = bankdto.Subject;
            dto.Title = bankClassService.GetById(bankdto.Id).Title ;
            dto.StuAnswer = ef.Answer;
            dto.Score = ef.Score;
            return dto;
        }


        public int GetScore(int PaperId, int UserId)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<StuPaperEntity> bs = new BaseService<StuPaperEntity>(ctx);
                var data=bs.GetAll().Where(e => e.PaperId == PaperId && e.UserId == UserId);
                int score = 0;
                foreach (var item in data)
                {
                    score += item.Score;
                }
                return score;
            }
            
        }
    }
}
