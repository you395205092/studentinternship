﻿using Exam.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyExam.Areas.Admin.Models
{
    public class BankViewEditModel
    {
        public int Id { get; set; }
        public BankClassDTO[] BankClass { get; set; }
        public BankDTO Bank { get; set; }
    }
}