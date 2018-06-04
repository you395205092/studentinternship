using Exam.Service.Entities;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service
{
    public class MyDbContext: DbContext
    {
        private static ILog log = LogManager.GetLogger(typeof(MyDbContext));

        public MyDbContext() : base("name=connstr")
        //name=conn1表示使用连接字符串中名字为conn1的去连接数据库
        {
            //Database.SetInitializer<MyDbContext>(null);
            this.Database.Log = (sql) =>
            {
                log.DebugFormat("EF执行SQL：{0}", sql);
            };
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<AdminEntity> Admins { get; set; }
        public DbSet<BankClassEntity> BankClasses { get; set; }
        public DbSet<BankEntity> Banks { get; set; }
        public DbSet<PaperEntity> Papers { get; set; }
        public DbSet<StuPaperEntity> StuPapers { get; set; }
        public DbSet<StuPaperLogEntity> StuPaperLog { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<WorkPostEntity> WorkPosts { get; set; }
        public DbSet<ProjectBMEntity> ProjectBMs { get; set; }
        public DbSet<StudentWorkPostEntity> StudentWorkPosts { get; set; }
        public DbSet<UserDataMoreEntity> UserDataMores { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }
    }
}
