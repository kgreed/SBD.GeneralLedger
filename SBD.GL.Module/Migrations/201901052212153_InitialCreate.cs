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
                        Category = c.Int(nullable: false),
                        Code = c.String(maxLength: 60),
                        Notes = c.String(),
                        OpeningBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Parent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Parent_Id)
                .Index(t => t.Code, unique: true)
                .Index(t => t.Parent_Id);
            
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
                        Name = c.String(maxLength: 450),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Parent_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HCategories", t => t.Parent_ID)
                .Index(t => t.Parent_ID);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 450),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BankId = c.String(maxLength: 20),
                        Memo = c.String(),
                        Card_Id = c.Int(),
                        CreditAccount_Id = c.Int(nullable: false),
                        DebitAccount_Id = c.Int(nullable: false),
                        job_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cards", t => t.Card_Id)
                .ForeignKey("dbo.Accounts", t => t.CreditAccount_Id)
                .ForeignKey("dbo.Accounts", t => t.DebitAccount_Id)
                .ForeignKey("dbo.Jobs", t => t.job_Id)
                .Index(t => t.BankId, unique: true)
                .Index(t => t.Card_Id)
                .Index(t => t.CreditAccount_Id)
                .Index(t => t.DebitAccount_Id)
                .Index(t => t.job_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "job_Id", "dbo.Jobs");
            DropForeignKey("dbo.Transactions", "DebitAccount_Id", "dbo.Accounts");
            DropForeignKey("dbo.Transactions", "CreditAccount_Id", "dbo.Accounts");
            DropForeignKey("dbo.Transactions", "Card_Id", "dbo.Cards");
            DropForeignKey("dbo.HCategories", "Parent_ID", "dbo.HCategories");
            DropForeignKey("dbo.Accounts", "Parent_Id", "dbo.Accounts");
            DropIndex("dbo.Transactions", new[] { "job_Id" });
            DropIndex("dbo.Transactions", new[] { "DebitAccount_Id" });
            DropIndex("dbo.Transactions", new[] { "CreditAccount_Id" });
            DropIndex("dbo.Transactions", new[] { "Card_Id" });
            DropIndex("dbo.Transactions", new[] { "BankId" });
            DropIndex("dbo.HCategories", new[] { "Parent_ID" });
            DropIndex("dbo.Accounts", new[] { "Parent_Id" });
            DropIndex("dbo.Accounts", new[] { "Code" });
            DropTable("dbo.Transactions");
            DropTable("dbo.ReportDataV2");
            DropTable("dbo.ModuleInfoes");
            DropTable("dbo.Jobs");
            DropTable("dbo.HCategories");
            DropTable("dbo.Cards");
            DropTable("dbo.Analyses");
            DropTable("dbo.Accounts");
        }
    }
}
