using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DTO
{
    public class SearchBankOpt
    {
        public long? ClassId { get; set; }
        public string Keyword { get; set; }
        public int PageSize { get; set; }//每页数据条数
        public int CurrentIndex { get; set; }//当前页码（页码从1开始）
    }
}
