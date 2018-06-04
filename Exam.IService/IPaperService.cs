using Exam.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.IService
{
    public interface IPaperService:IServiceSupport
    {
        int AddPaper(PaperDTO dto);
        BankDTO[] GetPaperSJ(int id,int x,int classId);
        PaperDTO GetPaperById(int id);
        PaperDTO[] GetAll();
        void MarkDeleted(int id);

    }
}
