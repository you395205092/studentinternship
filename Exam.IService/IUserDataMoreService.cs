using Exam.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.IService
{
    public interface IUserDataMoreService:IServiceSupport
    {
        int Add(int userId,string Profile,string PassportPic);
        int Edit(int id, string Profile, string PassportPic);
        int AdminAdd(int userId, string DSUrl);
        UserDataMoreDTO GetByUserId(int userId);
    }
}
