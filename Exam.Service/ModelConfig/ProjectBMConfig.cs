using Exam.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.ModelConfig
{
    class ProjectBMConfig : EntityTypeConfiguration<ProjectBMEntity>
    {
        public ProjectBMConfig()
        {
            ToTable("T_ProjectBMs");

        }
    }
}
