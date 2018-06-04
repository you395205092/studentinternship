using Exam.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.IService
{
    public interface IAdminService:IServiceSupport
    {
        int Add(string adminName,string password,string trueName);

        AdminDTO[] GetAll();
        AdminDTO GetById(int id);

        bool CheckLogin(string adminName, string password);

        void MarkDeleted(int id);
        bool Edit(int id,string passWord);


    }
}
