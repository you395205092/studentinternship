using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DTO
{
    public class SearchUserResult
    {
        public UserDTO[] result { get; set; }//当前页的数据
        public long totalCount { get; set; }//搜索的结果总条数
    }
}
