using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.Entities
{
    public class StuPaperEntity: BaseEntity
    {
        public int UserId { get; set; }
        public virtual UserEntity User { get; set; }
        public int PaperId { get; set; }
        public virtual PaperEntity Paper { get; set; }
        public int BankId { get; set; }
        public virtual BankEntity Bank { get; set; }
        public string Answer { get; set; }
        public int Score { get; set; }

    }
}
