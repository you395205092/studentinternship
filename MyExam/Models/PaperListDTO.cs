using Exam.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyExam.Models
{
    public class PaperListDTO
    {
        public PaperDTO[] paperdto { get; set; }

        public int[] Score { get; set; }
    }
}