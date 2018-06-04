using Exam.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.ModelConfig
{
    class AdminConfig:EntityTypeConfiguration<AdminEntity>
    {
        public AdminConfig()
        {
            ToTable("T_Admins");
            Property(e => e.AdminName).HasMaxLength(50).IsRequired();
            Property(e => e.PasswordSalt).HasMaxLength(20).IsRequired().IsUnicode(false);
            Property(e => e.PasswordHash).HasMaxLength(100).IsRequired().IsUnicode(false);
        }
    }
}
