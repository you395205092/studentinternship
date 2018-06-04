using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DTO
{
    public class StuPaperShowDTO
    {
        public string Subject { get; set; }
        public string Title { get; set; }
        public string ItemA { get; set; }
        public string ItemB { get; set; }
        public string ItemC { get; set; }
        public string ItemD { get; set; }
        public string TrueAnswer { get; set; }
        public string StuAnswer { get; set; }
        public int Score { get; set; }

    }
}
