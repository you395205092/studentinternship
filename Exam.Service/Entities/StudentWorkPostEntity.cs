using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.Entities
{
    public class StudentWorkPostEntity:BaseEntity
    {
        public int UserId { get; set; }
        public int WorkPostId { get; set; }
        /// <summary>
        /// 1选中了该岗位
        /// </summary>
        public int? State { get; set; }
    }
}
