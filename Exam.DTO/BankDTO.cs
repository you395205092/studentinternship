using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DTO
{
    public class BankDTO:BaseDTO
    {
        public string Subject { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ItemA { get; set; }
        public string ItemB { get; set; }
        public string ItemC { get; set; }
        public string ItemD { get; set; }
        public string ItemE { get; set; }
        public string Answer { get; set; }
    }
}
