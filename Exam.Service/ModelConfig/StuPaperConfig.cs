using Exam.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.ModelConfig
{
    class StuPaperConfig: EntityTypeConfiguration<StuPaperEntity>
    {
        public StuPaperConfig()
        {
            ToTable("T_StuPapers");
            HasRequired(u => u.User).WithMany().HasForeignKey(u => u.UserId).WillCascadeOnDelete(false);
            HasRequired(u => u.Bank).WithMany().HasForeignKey(u => u.BankId).WillCascadeOnDelete(false);
            HasRequired(u => u.Paper).WithMany().HasForeignKey(u => u.PaperId).WillCascadeOnDelete(false);
        }
    }
}
