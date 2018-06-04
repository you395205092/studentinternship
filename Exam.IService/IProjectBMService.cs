using Exam.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.IService
{
    public interface IProjectBMService:IServiceSupport
    {
        int Add(int userId,int ProjectId);
        bool IsExist(int userId, int ProjectId);
        int[] GetByUserId(int ProjectId);
    }
}
