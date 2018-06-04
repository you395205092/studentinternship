using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.Entities
{
    public class ProjectBMEntity:BaseEntity
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }
}
