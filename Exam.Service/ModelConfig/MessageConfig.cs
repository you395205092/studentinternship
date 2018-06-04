using Exam.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.ModelConfig
{
    class MessageConfig:EntityTypeConfiguration<MessageEntity>
    {
        public MessageConfig()
        {
            ToTable("T_Messages");
            Property(e => e.UserId).IsRequired();
            Property(e => e.Title).HasMaxLength(100).IsRequired();
            Property(e => e.MesContent).HasMaxLength(100).IsRequired();
        }
    }
}
