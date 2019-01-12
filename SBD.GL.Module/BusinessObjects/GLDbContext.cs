using System;
using System.Collections.Generic;
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
            Database.SetInitializer(new GlDBInitializer());
           // this.Configuration.ProxyCreationEnabled = false;
        }

        public GLDbContext(DbConnection connection)
            : base(connection, false)
        {
        }

        public GLDbContext()
            : base("name=ConnectionString")
        {
            Database.SetInitializer(new GlDBInitializer());
        }

        public DbSet<ModuleInfo> ModulesInfo { get; set; }
        public DbSet<Analysis> Analysis { get; set; }
        public DbSet<H2Category> H2Categories { get; set; }
        public DbSet<ReportDataV2> ReportDataV2 { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<GstCategory> GstCategories { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<TranHeader> TranHeaders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Account>().Property(x => x.Parent_Id).IsOptional();

            modelBuilder.Entity<Account>().HasOptional(x=>x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.Parent_Id);

            modelBuilder.Entity<Transaction>().HasKey(x => x.Id).Property(x => x.TranHeader_Id).IsRequired();

            //modelBuilder.Entity<TranHeader>()
            //    .HasMany(x => x.Transactions)
            //    .WithRequired(x => x.TranHeader)
            //    .HasForeignKey(x => x.TranHeader_Id).WillCascadeOnDelete(true);


          

            modelBuilder.Entity<H2Category>().HasMany(x => x.Children).WithOptional(x => x.Parent);
         

            base.OnModelCreating(modelBuilder);

        }

        public class GlDBInitializer : CreateDatabaseIfNotExists<GLDbContext>
        {
            protected override void Seed(GLDbContext context)
            {
                var balanceSheetGST =new GstCategory {Code = "N-T", Percent = 0};

                var gstCategories = new List<GstCategory>
                {
                    new GstCategory {Code = "GST", Percent = 10},
                    balanceSheetGST
                };
                context.GstCategories.AddRange(gstCategories);           


                var accounts = new List<Account>
                {
                    new Account {Code = "01", GlCategory = GLCategoryEnum.Asset,GstCategory = balanceSheetGST},
                    new Account {Code = "02", GlCategory = GLCategoryEnum.Liability,GstCategory = balanceSheetGST},
                };


                context.Accounts.AddRange(accounts);

                base.Seed(context);
            }
        }
    }
}