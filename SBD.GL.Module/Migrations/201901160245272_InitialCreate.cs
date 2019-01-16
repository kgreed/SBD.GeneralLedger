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
                        Category_Id = c.Int(nullable: false),
                        GstCategory_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GLCategories", t => t.Category_Id)
                .ForeignKey("dbo.GstCategories", t => t.GstCategory_Id)
                .ForeignKey("dbo.Accounts", t => t.Parent_Id)
                .Index(t => t.Code, unique: true)
                .Index(t => t.Parent_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.GstCategory_Id);
            
            CreateTable(
                "dbo.GLCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.Int(nullable: false),
                        IsBalanceSheet = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.BankImportLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TranHeader_Id = c.Int(),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ref1 = c.String(),
                        Ref2 = c.String(),
                        Ref3 = c.String(),
                        Ref4 = c.String(),
                        Ref5 = c.String(),
                        Note = c.String(),
                        Account_Id = c.Int(),
                        BankImport_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .ForeignKey("dbo.BankImports", t => t.BankImport_Id)
                .ForeignKey("dbo.TranHeaders", t => t.TranHeader_Id)
                .Index(t => t.TranHeader_Id)
                .Index(t => t.Account_Id)
                .Index(t => t.BankImport_Id);
            
            CreateTable(
                "dbo.BankImports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        FileDate = c.DateTime(nullable: false),
                        ImportedAt = c.DateTime(nullable: false),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.TranHeaders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatementNumber = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Reference = c.String(),
                        Notes = c.String(),
                        Card_Id = c.Int(),
                        LinkedAccount_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cards", t => t.Card_Id)
                .ForeignKey("dbo.Accounts", t => t.LinkedAccount_Id)
                .Index(t => t.Card_Id)
                .Index(t => t.LinkedAccount_Id);
            
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
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Memo = c.String(),
                        TranHeader_Id = c.Int(nullable: false),
                        CreditAccount_Id = c.Int(nullable: false),
                        DebitAccount_Id = c.Int(nullable: false),
                        job_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.CreditAccount_Id)
                .ForeignKey("dbo.Accounts", t => t.DebitAccount_Id)
                .ForeignKey("dbo.Jobs", t => t.job_Id)
                .ForeignKey("dbo.TranHeaders", t => t.TranHeader_Id)
                .Index(t => t.TranHeader_Id)
                .Index(t => t.CreditAccount_Id)
                .Index(t => t.DebitAccount_Id)
                .Index(t => t.job_Id);
            
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
                "dbo.BankImportRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ref1 = c.String(),
                        Ref2 = c.String(),
                        Ref3 = c.String(),
                        Ref4 = c.String(),
                        Ref5 = c.String(),
                        RuleName = c.String(),
                        FromAccount_Id = c.Int(nullable: false),
                        ToAccount_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.FromAccount_Id)
                .ForeignKey("dbo.Accounts", t => t.ToAccount_Id)
                .Index(t => t.FromAccount_Id)
                .Index(t => t.ToAccount_Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.H2Category", "Parent_ID", "dbo.H2Category");
            DropForeignKey("dbo.BankImportRules", "ToAccount_Id", "dbo.Accounts");
            DropForeignKey("dbo.BankImportRules", "FromAccount_Id", "dbo.Accounts");
            DropForeignKey("dbo.BankImportLines", "TranHeader_Id", "dbo.TranHeaders");
            DropForeignKey("dbo.Transactions", "TranHeader_Id", "dbo.TranHeaders");
            DropForeignKey("dbo.Transactions", "job_Id", "dbo.Jobs");
            DropForeignKey("dbo.Transactions", "DebitAccount_Id", "dbo.Accounts");
            DropForeignKey("dbo.Transactions", "CreditAccount_Id", "dbo.Accounts");
            DropForeignKey("dbo.TranHeaders", "LinkedAccount_Id", "dbo.Accounts");
            DropForeignKey("dbo.TranHeaders", "Card_Id", "dbo.Cards");
            DropForeignKey("dbo.BankImportLines", "BankImport_Id", "dbo.BankImports");
            DropForeignKey("dbo.BankImports", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.BankImportLines", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "Parent_Id", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "GstCategory_Id", "dbo.GstCategories");
            DropForeignKey("dbo.Accounts", "Category_Id", "dbo.GLCategories");
            DropIndex("dbo.H2Category", new[] { "Parent_ID" });
            DropIndex("dbo.Settings", new[] { "Name" });
            DropIndex("dbo.BankImportRules", new[] { "ToAccount_Id" });
            DropIndex("dbo.BankImportRules", new[] { "FromAccount_Id" });
            DropIndex("dbo.Jobs", new[] { "Name" });
            DropIndex("dbo.Transactions", new[] { "job_Id" });
            DropIndex("dbo.Transactions", new[] { "DebitAccount_Id" });
            DropIndex("dbo.Transactions", new[] { "CreditAccount_Id" });
            DropIndex("dbo.Transactions", new[] { "TranHeader_Id" });
            DropIndex("dbo.Cards", new[] { "Name" });
            DropIndex("dbo.TranHeaders", new[] { "LinkedAccount_Id" });
            DropIndex("dbo.TranHeaders", new[] { "Card_Id" });
            DropIndex("dbo.BankImports", new[] { "Account_Id" });
            DropIndex("dbo.BankImportLines", new[] { "BankImport_Id" });
            DropIndex("dbo.BankImportLines", new[] { "Account_Id" });
            DropIndex("dbo.BankImportLines", new[] { "TranHeader_Id" });
            DropIndex("dbo.GstCategories", new[] { "Code" });
            DropIndex("dbo.Accounts", new[] { "GstCategory_Id" });
            DropIndex("dbo.Accounts", new[] { "Category_Id" });
            DropIndex("dbo.Accounts", new[] { "Parent_Id" });
            DropIndex("dbo.Accounts", new[] { "Code" });
            DropTable("dbo.H2Category");
            DropTable("dbo.Settings");
            DropTable("dbo.ReportDataV2");
            DropTable("dbo.ModuleInfoes");
            DropTable("dbo.BankImportRules");
            DropTable("dbo.Jobs");
            DropTable("dbo.Transactions");
            DropTable("dbo.Cards");
            DropTable("dbo.TranHeaders");
            DropTable("dbo.BankImports");
            DropTable("dbo.BankImportLines");
            DropTable("dbo.Analyses");
            DropTable("dbo.GstCategories");
            DropTable("dbo.GLCategories");
            DropTable("dbo.Accounts");
        }
    }
}
