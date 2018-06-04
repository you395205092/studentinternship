using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DTO
{
    public class ProjectDTO:BaseDTO
    {
        public string Title { get; set; }
        public string enTitle { get; set; }
        public string SubTitle { get; set; }
        public string Remark { get; set; }
        public string PicUrl { get; set; }
        
    }
}
