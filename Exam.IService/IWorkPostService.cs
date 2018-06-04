using Exam.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.IService
{
    public interface IWorkPostService:IServiceSupport
    {
        SearchWorkPostResult Search(SearchOpt options);
        int Add(WorkPostDTO dto);
        WorkPostDTO GetById(int id);
        WorkPostDTO[] GetAll();
        WorkPostDTO[] GetNumNotEmpty();
        void MarkDeleted(int id);
        int UpdateNum(int id);
    }
}
