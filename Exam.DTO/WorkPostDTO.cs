using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DTO
{
    public class WorkPostDTO:BaseDTO
    {
        public string Position { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 经理
        /// </summary>
        public string Manager { get; set; }
        /// <summary>
        /// 时薪
        /// </summary>
        public string Wage { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// 余量
        /// </summary>
        public int CountNum { get; set; }
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public int Score { get; set; }


    }
}
