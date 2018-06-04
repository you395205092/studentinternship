using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DTO
{
    public class PaperDTO:BaseDTO
    {
        public string Title { get; set; }
        public string PCount { get; set; }
        public string PScore { get; set; }
        public int ExamTime { get; set; }
        public int ZScore { get; set; }
    }
}
