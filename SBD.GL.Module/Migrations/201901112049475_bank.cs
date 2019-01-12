namespace SBD.GL.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bank : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "DefaultAccount_Id", c => c.Int());
            CreateIndex("dbo.Cards", "DefaultAccount_Id");
            AddForeignKey("dbo.Cards", "DefaultAccount_Id", "dbo.Accounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "DefaultAccount_Id", "dbo.Accounts");
            DropIndex("dbo.Cards", new[] { "DefaultAccount_Id" });
            DropColumn("dbo.Cards", "DefaultAccount_Id");
        }
    }
}
