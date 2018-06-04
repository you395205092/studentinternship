using Exam.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.ModelConfig
{
    class PaperConfig:EntityTypeConfiguration<PaperEntity>
    {
        public PaperConfig()
        {
            ToTable("T_Papers");
            Property(e => e.Title).HasMaxLength(50).IsRequired();
            Property(e => e.PCount).HasMaxLength(250).IsRequired();
            Property(e => e.PScore).HasMaxLength(250).IsRequired();
        }
    }
}
