namespace SBD.GL.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankImportRules", "RuleName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BankImportRules", "RuleName");
        }
    }
}
