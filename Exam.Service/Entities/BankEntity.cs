using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.Entities
{
    public class BankEntity:BaseEntity
    {
        public string Subject { get; set; }
        public int ClassId { get; set; }
        public virtual BankClassEntity BankClass { get; set; }
        public string ItemA { get; set; }
        public string ItemB { get; set; }
        public string ItemC { get; set; }
        public string ItemD { get; set; }
        public string ItemE { get; set; }
        public string Answer { get; set; }
       
    }
}
