using Exam.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.ModelConfig
{
    class StudentWorkPostConfig : EntityTypeConfiguration<StudentWorkPostEntity>
    {
        public StudentWorkPostConfig()
        {
            ToTable("T_StudentWorkPosts");
        }
    }
}
