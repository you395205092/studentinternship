using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Exam.Service.Entities;

namespace Exam.Service.ModelConfig
{
    class BankConfig: EntityTypeConfiguration<BankEntity>
    {
        public BankConfig()
        {
            ToTable("T_Banks");
            HasRequired(h => h.BankClass).WithMany().HasForeignKey(h => h.ClassId).WillCascadeOnDelete(false);
            Property(e => e.Subject).HasMaxLength(255).IsRequired();
            Property(e => e.ItemA).HasMaxLength(100).IsRequired();
            Property(e => e.ItemB).HasMaxLength(100).IsRequired();
            Property(e => e.ItemC).HasMaxLength(100);
            Property(e => e.ItemD).HasMaxLength(100);
            Property(e => e.ItemE).HasMaxLength(100);
        }
    }
}
