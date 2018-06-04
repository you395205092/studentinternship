using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyExam.Areas.Admin.Models
{
    public class PaperAddModel
    {
        public string Title { get; set; }
        public int[] PCount { get; set; }
        public int[] PScore { get; set; }
        public int ExamTime { get; set; }
        public int ZScore { get; set; }
    }
}