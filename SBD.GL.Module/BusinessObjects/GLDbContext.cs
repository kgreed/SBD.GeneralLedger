using System;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using DevExpress.ExpressApp.EF.Updating;
using DevExpress.Persistent.BaseImpl.EF;

namespace SBD.GL.Module.BusinessObjects
{
    public class GLDbContext : DbContext
    {
        public GLDbContext(String connectionString)
            : base(connectionString)
        {
        }

        public GLDbContext(DbConnection connection)
            : base(connection, false)
        {
        }

        public GLDbContext()
            : base("name=ConnectionString")
        {
        }

        public DbSet<ModuleInfo> ModulesInfo { get; set; }
        public DbSet<Analysis> Analysis { get; set; }
        // public DbSet<HCategory> HCategories { get; set; }
        public DbSet<H2Category> H2Categories { get; set; }
        public DbSet<ReportDataV2> ReportDataV2 { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Card> Cards { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Account>().Property(x => x.Parent_Id).IsOptional();

            modelBuilder.Entity<Account>().HasOptional(x=>x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.Parent_Id);



            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Account>()
            //    .HasOptional<Account>(u => u.Parent) // EF'll load Parent if any     
            //    .WithMany(u => u.Children);

        }
    }
}