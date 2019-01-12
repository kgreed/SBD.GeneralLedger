namespace SBD.GL.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tran : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Transactions", new[] { "BankId" });
            DropColumn("dbo.Transactions", "BankId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "BankId", c => c.String(maxLength: 20));
            CreateIndex("dbo.Transactions", "BankId", unique: true);
        }
    }
}
