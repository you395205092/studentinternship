﻿using Exam.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.ModelConfig
{
    class ProjectConfig : EntityTypeConfiguration<ProjectEntity>
    {
        public ProjectConfig()
        {
            ToTable("T_Projects");
            Property(e => e.Title).HasMaxLength(50).IsRequired();
            Property(e => e.enTitle).HasMaxLength(50).IsRequired();
        }
    }
}
