using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DTO
{
    public class UserDTO:BaseDTO
    {
        public string UserName { get; set; }
        public string TrueName { get; set; }
        public int Sex { get; set; }
        public DateTime? BirthDay { get; set; }
        public string EnglishLevel { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string CardId { get; set; }
        public int? ExamScore { get; set; }
        public int? WorkPostId { get; set; }
        public string Position { get; set;  }
        public string ProjectName { get; set; }
        public string Address { get; set; }
        /// <summary>
        /// 0  1报名  2考试  3老师提供岗位 4已经选择岗位 5上传签证信息  6抵美  7回国
        /// </summary>
        public int? State { get; set; }

    }
}
