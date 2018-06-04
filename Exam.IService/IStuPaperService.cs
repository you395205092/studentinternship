using Exam.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.IService
{
    public interface IStuPaperService:IServiceSupport
    {
        void Add(StuPaperDTO[] dto);
        StuPaperShowDTO[] GetAll(int PaperId, int UserId);
        int GetScore(int PaperId, int UserId);
    }
}
