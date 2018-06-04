using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.Entities
{
    public class PaperEntity:BaseEntity
    {
        public string Title { get; set; }
        public string PCount { get; set; }
        public string PScore { get; set; }
        public int ExamTime { get; set; }
        public int ZScore { get; set; }

    }
}
