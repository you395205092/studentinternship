using Exam.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.ModelConfig
{
    class StuPaperLogConfig: EntityTypeConfiguration<StuPaperLogEntity>
    {
        public StuPaperLogConfig()
        {
            ToTable("T_StuPaperLogs");
            Property(e => e.BankLog).HasMaxLength(500).IsRequired();
        }
    }
}
