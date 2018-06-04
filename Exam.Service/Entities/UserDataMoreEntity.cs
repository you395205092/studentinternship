using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.Entities
{
    public class UserDataMoreEntity:BaseEntity
    {
        public int UserId { get; set; }
        /// <summary>
        /// 个人简介
        /// </summary>
        public string Profile { get; set; }
        /// <summary>
        /// 护照
        /// </summary>
        public string PassportPic { get; set; }
        /// <summary>
        /// DS表
        /// </summary>
        public string DSUrl { get; set; }
    }
}
