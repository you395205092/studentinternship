using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.Entities
{
    public class MessageEntity:BaseEntity
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string MesContent { get; set; }
        public string ReplyContent { get; set; }
        public DateTime? ReplayTime { get; set; }
    }
}
