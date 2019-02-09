using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.IO;
using DevExpress.ExpressApp.EF.Updating;
using DevExpress.Persistent.BaseImpl.EF;
using SBD.GL.Module.BusinessObjects.Accounts;
using SBD.GL.Module.BusinessObjects.Imports;
using SBD.GL.Module.BusinessObjects.Transactions;
using SQLite.CodeFirst;

namespace SBD.GL.Module.BusinessObjects
{
    public class GLDbContext : DbContext
    {
        public GLDbContext(String connectionString)
            : base(new SQLiteConnection() { ConnectionString = MyConnectionString() }, true)
        {
           // Database.SetInitializer(new GlDBInitializer());
           // this.Configuration.ProxyCreationEnabled = false;
        }

        public static string MyConnectionString()
        {
            return SiteCache.Instance.ConnectionString;
           
        }

        public GLDbContext(DbConnection connection)
            : base(connection, false)
        {
        }

        public GLDbContext(ObjectContext objectContext) : base(objectContext,false)
        {
        }

        public GLDbContext()
            : base(new SQLiteConnection()
            {
                ConnectionString =MyConnectionString()
            }, true)
        {


        }
    //    public DbSet<SimpleAccount> SimpleAccounts { get; set; }
        public DbSet<ModuleInfo> ModulesInfo { get; set; }
        public DbSet<Analysis> Analysis { get; set; }
     //   public DbSet<H2Category> H2Categories { get; set; }
        public DbSet<ReportDataV2> ReportDataV2 { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<GstCategory> GstCategories { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<TranHeader> TranHeaders { get; set; }
        public DbSet<GLCategory> GLCategories { get; set; }
        public DbSet<BankImport> BankImports { get; set; }
        public DbSet<BankImportLine> BankImportLines { get; set; }
        public DbSet<BankImportRule> BankImportRules { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<GLDbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);


            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Account>().Property(x => x.Parent_Id).IsOptional();

            modelBuilder.Entity<Account>().HasOptional(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.Parent_Id);

         //   modelBuilder.Entity<Transaction>().HasKey(x => x.Id).Property(x => x.TranHeader_Id).IsRequired();


            modelBuilder.Entity<TranHeader>()
                .HasMany(x=>x.Transactions)
                .WithRequired(x=>x.TranHeader)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<BankImport>()
                .HasMany(x=> x.Lines)
                .WithRequired(x=>x.BankImport)
                .WillCascadeOnDelete(true);

          //  modelBuilder.Entity<BankImportLine>().HasOptional(x => x.MatchingHeader);




            modelBuilder.Entity<H2Category>().HasMany(x => x.Children).WithOptional(x => x.Parent);


            base.OnModelCreating(modelBuilder);

        }

        public class GlDBInitializer : CreateDatabaseIfNotExists<GLDbContext>
        {
            protected override void Seed(GLDbContext context)
            {
                
                base.Seed(context);
            }

           

        }
    }
}