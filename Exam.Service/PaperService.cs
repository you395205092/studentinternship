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
    public class PaperService : IPaperService
    {
        public IBankService bankService { get; set; }
        public IBankClassService bankClassService { get; set; }


        public int AddPaper(PaperDTO dto)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                PaperEntity ef = new PaperEntity();
                ef.Title = dto.Title;
                ef.PCount = dto.PCount;
                ef.PScore = dto.PScore;
                ef.ExamTime = dto.ExamTime;
                ef.ZScore = dto.ZScore;
                ctx.Papers.Add(ef);
                ctx.SaveChanges();
                return ef.Id;
                
            }
        }

        public PaperDTO[] GetAll()
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<PaperEntity> bs = new BaseService<PaperEntity>(ctx);
                var data= bs.GetAll();
                return data.ToList().Select(e => ToDTO(e)).ToArray();
            }
        }

        public PaperDTO ToDTO(PaperEntity ef)
        {
            PaperDTO dto = new PaperDTO();
            dto.Id = ef.Id;
            dto.ExamTime = ef.ExamTime;
            dto.CreateDateTime = ef.CreateDateTime;
            dto.PCount = ef.PCount;
            dto.PScore = ef.PScore;
            dto.Title = ef.Title;
            dto.ZScore = ef.ZScore;
            return dto;
        }
        public PaperDTO GetPaperById(int id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<PaperEntity> bs = new BaseService<PaperEntity>(ctx);
                var data = bs.GetById(id);
                return ToDTO(data);
            }
        }
        public BankDTO[] GetPaperSJ(int id,int x,int classId)
        {      

            var data=bankService.GetByClassId(classId);
            var howti = Convert.ToInt16(GetPaperById(id).PCount.Split(',')[x]);

            Random rd = new Random();
            List<BankDTO> lst = new List<BankDTO>();
            
            int len = data.Length;
            if (data.Length>= howti)
            {
                len = howti;
            }
            List<int> lstyj = new List<int>();
            var num=0;
            for (int i = 0; i < len; i++)
            {
                num = rd.Next(0, data.Length);
                if (!lstyj.Contains(num))
                {
                    lst.Add(data[num]);
                    lstyj.Add(num);
                }
                else
                {
                    i--;
                }

            }
            return lst.ToArray();
        }

        public void MarkDeleted(int id)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<PaperEntity> bs = new BaseService<PaperEntity>(ctx);
                bs.MarkDeleted(id);
            }
        }
    }
}
