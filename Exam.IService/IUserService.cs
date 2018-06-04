using Exam.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.IService
{
    public interface IUserService:IServiceSupport
    {
        int AddUser(UserAddDTO dto);
        bool IsExist(string userName);
        UserDTO[] GetAll();
        UserDTO GetById(int id);
        int Edit(UserAddDTO dto);
        bool CheckLogin(string userName, string password);
        void MarkDeleted(int id);
        SearchUserResult Search(SearchOpt options);

        UserDTO[] GetUserByPostId(int id);

        UserDTO[] GetUserIsWork();

        int UpdateState(int userId, int state);
        int UpdateExamScore(int userId, int score);
        int UpdateWorkPost(int userId, int workPostId);
    }
}
