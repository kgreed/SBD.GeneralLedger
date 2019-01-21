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
using DevExpress.ExpressApp.EF.Updating;
using DevExpress.Persistent.BaseImpl.EF;
using SQLite.CodeFirst;

namespace SBD.GL.Module.BusinessObjects
{
    public class GLDbContext : DbContext
    {
        public GLDbContext(String connectionString)
            : base(new SQLiteConnection() { ConnectionString = connectionString }, true)
        {
           // Database.SetInitializer(new GlDBInitializer());
           // this.Configuration.ProxyCreationEnabled = false;
        }

        public GLDbContext(DbConnection connection)
            : base(connection, false)
        {
        }

        public GLDbContext()
            : base(new SQLiteConnection()
            {
                ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"]
                    .ConnectionString
            }, true)
        {


        }

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

            modelBuilder.Entity<Transaction>().HasKey(x => x.Id).Property(x => x.TranHeader_Id).IsRequired();




            modelBuilder.Entity<BankImportLine>()
                .HasOptional(x => x.MatchingHeader);




            modelBuilder.Entity<H2Category>().HasMany(x => x.Children).WithOptional(x => x.Parent);


            base.OnModelCreating(modelBuilder);

        }

        public class GlDBInitializer : CreateDatabaseIfNotExists<GLDbContext>
        {
            protected override void Seed(GLDbContext context)
            {
                var balanceSheetGst = context.GstCategories.Add(new GstCategory { Code = "N-T", Percent = 0 });
                var pandlGst = context.GstCategories.Add(new GstCategory { Code = "GST", Percent = 10 });

                for (int i = (int)GLCategoryEnum.Asset; i <= (int)GLCategoryEnum.OtherIncome; i++)
                {
                    var IsBalSheet = IsBalanceSheet(i);
                    var cat = context.GLCategories.Add(new GLCategory { Category = i, IsBalanceSheet = IsBalSheet });
                    var gstCategory = IsBalSheet ? balanceSheetGst : pandlGst;
                    var account = new Account { Code = $"0{i}", Category = cat, GstCategory = gstCategory };
                    var child1 = new Account { Code = $"0{i}-0100", Category = cat, GstCategory = gstCategory, Parent = account };
                    account.Children.Add(child1);
                    var child2 = new Account { Code = $"0{i}-0200", Category = cat, GstCategory = gstCategory, Parent = account };
                    account.Children.Add(child2);
                    var child3 = new Account { Code = $"0{i}-0300", Category = cat, GstCategory = gstCategory, Parent = account };
                    account.Children.Add(child3);

                    context.Accounts.Add(account);
                    context.Accounts.Add(child1);
                    context.Accounts.Add(child2);
                    context.Accounts.Add(child3);

                }
                base.Seed(context);
            }

            private bool IsBalanceSheet(int i)
            {
                var cat = (GLCategoryEnum)i;
                return cat == GLCategoryEnum.Asset
                       || cat == GLCategoryEnum.Liability
                       || cat == GLCategoryEnum.Equity;
            }

        }
    }
}