using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DTO
{
    public class AdminDTO: BaseDTO
    {
        public string AdminName { get; set; }
        public string TrueName { get; set; }
        public int LoginTimes { get; set; }
        public DateTime? LastDateTime { get; set; }
        public string LoginIP { get; set; }
    }
}
