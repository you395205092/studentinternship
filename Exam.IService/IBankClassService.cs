using Exam.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.IService
{
    public interface IBankClassService:IServiceSupport
    {
        int Add(string title);
        int Edit(int id,string title);
        BankClassDTO[] GetAll();
        BankClassDTO GetById(int id);
        void MarkDeleted(int id);


    }
}
