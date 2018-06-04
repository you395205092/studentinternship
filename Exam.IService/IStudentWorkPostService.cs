using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.IService
{
    public interface IStudentWorkPostService:IServiceSupport
    {
        int Add(int userId, int WorkPostId);
        int[] GetWorkPostIdByUserId(int userId);
        int UpdataState(int userId, int workPostId);
        int[] GetUserIdByState();
    }
}
