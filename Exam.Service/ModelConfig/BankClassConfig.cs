using Exam.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.ModelConfig
{
    class BankClassConfig : EntityTypeConfiguration<BankClassEntity>
    {
        public BankClassConfig()
        {
            ToTable("T_BankClass");
            Property(e => e.Title).HasMaxLength(50).IsRequired();
        }
    }
}
