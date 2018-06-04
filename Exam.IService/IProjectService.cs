using Exam.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.IService
{
    public interface IProjectService:IServiceSupport
    {
        int Add(ProjectDTO dto);
        int Edit(ProjectDTO dto);
        ProjectDTO[] GetAll();
        ProjectDTO GetById(int id);
        void MarkDeleted(int id);
    }
}
