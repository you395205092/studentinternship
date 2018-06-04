using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyExam.Areas.Admin.Models
{
    public class ProjectAddDTO
    {
        public string Title { get; set; }
        public string enTitle { get; set; }
        public string SubTitle { get; set; }
        public string Remark { get; set; }
        public string PicUrl { get; set; }
        public HttpPostedFileBase fileUrl { get; set; }
    }
}