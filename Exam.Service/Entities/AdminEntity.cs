using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.Entities
{
    public class AdminEntity:BaseEntity
    {
        public string AdminName { get; set; }
        public string TrueName { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public int LoginTimes { get; set; }
        public DateTime? LastDateTime { get; set; }
        public string LoginIP { get; set; }

    }
}
