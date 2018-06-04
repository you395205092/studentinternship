using Exam.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.ModelConfig
{
    class UserConfig: EntityTypeConfiguration<UserEntity>
    {
        public UserConfig()
        {
            ToTable("T_Users");

            Property(e => e.UserName).HasMaxLength(50).IsRequired();
            Property(e => e.TrueName).HasMaxLength(50).IsRequired();
            Property(e => e.PasswordSalt).HasMaxLength(20).IsRequired().IsUnicode(false);
            Property(e => e.PasswordHash).HasMaxLength(100).IsRequired().IsUnicode(false);
            Property(e => e.CardId).HasMaxLength(100).IsRequired().IsUnicode(false);
            Property(e => e.EnglishLevel).HasMaxLength(100).IsUnicode(false);
            Property(e => e.Email).HasMaxLength(100).IsUnicode(false);
            Property(e => e.EnglishLevel).HasMaxLength(100).IsUnicode(false);
            HasRequired(r => r.WorkPosts).WithMany().HasForeignKey(r => r.WorkPostId).WillCascadeOnDelete(false);
        }
    }
}
