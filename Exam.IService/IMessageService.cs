using Exam.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.IService
{
    public interface IMessageService:IServiceSupport
    {
        int Add(MessageDTO dto);
        int Edit(int id, string ReplyContent);
        MessageDTO[] GetByUserId(int userId);
        MessageDTO GetById(int Id);
        MessageDTO[] GetAll();
    }
}
