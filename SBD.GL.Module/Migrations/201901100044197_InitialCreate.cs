namespace SBD.GL.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Header = c.Boolean(nullable: false),
                        Code = c.String(maxLength: 60),
                        Parent_Id = c.Int(),
                        Notes = c.String(),
                        OpeningBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category = c.Int(nullable: false),
                        GstCategory_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GstCategories", t => t.GstCategory_Id)
                .ForeignKey("dbo.Accounts", t => t.Parent_Id)
                .Index(t => t.Code, unique: true)
                .Index(t => t.Parent_Id)
                .Index(t => t.GstCategory_Id);
            
            CreateTable(
                "dbo.GstCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 60),
                        Percent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.Analyses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Criteria = c.String(),
                        ObjectTypeName = c.String(),
                        DimensionPropertiesString = c.String(),
                        PivotGridSettingsContent = c.Binary(),
                        ChartSettingsContent = c.Binary(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 60),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.H2Category",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Parent_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.H2Category", t => t.Parent_ID)
                .Index(t => t.Parent_ID);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 60),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.ModuleInfoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Version = c.String(),
                        AssemblyFileName = c.String(),
                        IsMain = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ReportDataV2",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DataTypeName = c.String(),
                        IsInplaceReport = c.Boolean(nullable: false),
                        PredefinedReportTypeName = c.String(),
                        Content = c.Binary(),
                        DisplayName = c.String(),
                        ParametersObjectTypeName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 60),
                        Value = c.String(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.TransactionHeader2",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Notes = c.String(),
                        Card_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cards", t => t.Card_Id)
                .Index(t => t.Card_Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BankId = c.String(maxLength: 20),
                        Memo = c.String(),
                        CreditAccount_Id = c.Int(nullable: false),
                        DebitAccount_Id = c.Int(nullable: false),
                        job_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.CreditAccount_Id)
                .ForeignKey("dbo.Accounts", t => t.DebitAccount_Id)
                .ForeignKey("dbo.Jobs", t => t.job_Id)
                .Index(t => t.BankId, unique: true)
                .Index(t => t.CreditAccount_Id)
                .Index(t => t.DebitAccount_Id)
                .Index(t => t.job_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "job_Id", "dbo.Jobs");
            DropForeignKey("dbo.Transactions", "DebitAccount_Id", "dbo.Accounts");
            DropForeignKey("dbo.Transactions", "CreditAccount_Id", "dbo.Accounts");
            DropForeignKey("dbo.TransactionHeader2", "Card_Id", "dbo.Cards");
            DropForeignKey("dbo.H2Category", "Parent_ID", "dbo.H2Category");
            DropForeignKey("dbo.Accounts", "Parent_Id", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "GstCategory_Id", "dbo.GstCategories");
            DropIndex("dbo.Transactions", new[] { "job_Id" });
            DropIndex("dbo.Transactions", new[] { "DebitAccount_Id" });
            DropIndex("dbo.Transactions", new[] { "CreditAccount_Id" });
            DropIndex("dbo.Transactions", new[] { "BankId" });
            DropIndex("dbo.TransactionHeader2", new[] { "Card_Id" });
            DropIndex("dbo.Settings", new[] { "Name" });
            DropIndex("dbo.Jobs", new[] { "Name" });
            DropIndex("dbo.H2Category", new[] { "Parent_ID" });
            DropIndex("dbo.Cards", new[] { "Name" });
            DropIndex("dbo.GstCategories", new[] { "Code" });
            DropIndex("dbo.Accounts", new[] { "GstCategory_Id" });
            DropIndex("dbo.Accounts", new[] { "Parent_Id" });
            DropIndex("dbo.Accounts", new[] { "Code" });
            DropTable("dbo.Transactions");
            DropTable("dbo.TransactionHeader2");
            DropTable("dbo.Settings");
            DropTable("dbo.ReportDataV2");
            DropTable("dbo.ModuleInfoes");
            DropTable("dbo.Jobs");
            DropTable("dbo.H2Category");
            DropTable("dbo.Cards");
            DropTable("dbo.Analyses");
            DropTable("dbo.GstCategories");
            DropTable("dbo.Accounts");
        }
    }
}
