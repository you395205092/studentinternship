using Exam.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.ModelConfig
{
    class UserDataMoreConfig : EntityTypeConfiguration<UserDataMoreEntity>
    {
        public UserDataMoreConfig()
        {
            ToTable("T_UserDataMores");
           
        }
    }
}
