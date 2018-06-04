using Exam.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.IService
{
    public interface IBankService:IServiceSupport
    {
        int Add(BankDTO dto);
        int Edit(BankDTO dto);
        BankDTO[] GetAll();
        BankDTO[] GetByClassId(int id);
        BankDTO GetById(int id);
        void MarkDeleted(int id);

        SearchBankResult Search(SearchBankOpt options);
    }
}
