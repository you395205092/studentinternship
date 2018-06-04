using Exam.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.ModelConfig
{
    class WorkPostConfig : EntityTypeConfiguration<WorkPostEntity>
    {
        public WorkPostConfig()
        {
            ToTable("T_WorkPosts");
            Property(e => e.Position).HasMaxLength(50).IsRequired();
            Property(e => e.Address).HasMaxLength(100).IsRequired();
            Property(e => e.Manager).HasMaxLength(50).IsRequired();
            Property(e => e.Wage).HasMaxLength(50).IsRequired();
            Property(e => e.Email).HasMaxLength(50);
            HasRequired(h => h.Project).WithMany().HasForeignKey(h => h.ProjectId).WillCascadeOnDelete(false);

            /*
            HasMany(r => r.Users).WithMany(p => p.WorkPosts).Map(m => m.ToTable("T_UserWorkPosts")
            .MapLeftKey("UserId").MapRightKey("WorkPostId"));*/

        }
    }
}
