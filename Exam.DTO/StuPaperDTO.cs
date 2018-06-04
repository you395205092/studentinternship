using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DTO
{
    public class StuPaperDTO : BaseDTO
    {
        public int UserId { get; set; }
        public int PaperId { get; set; }
        public int BankId { get; set; }
        public string Answer { get; set; }
        public int Score {get;set;}
    }
}
