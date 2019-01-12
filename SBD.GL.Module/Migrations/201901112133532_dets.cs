namespace SBD.GL.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dets : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Cards", new[] { "DefaultCreditAccount_Id" });
            DropIndex("dbo.Cards", new[] { "DefaultDebitAccount_Id" });
            DropForeignKey("dbo.Cards", "DefaultCreditAccount_Id", "dbo.Accounts");
            DropForeignKey("dbo.Cards", "DefaultDebitAccount_Id", "dbo.Accounts");

            DropColumn("dbo.Cards", "DefaultCreditAccount_Id");
            DropColumn("dbo.Cards", "DefaultDebitAccount_Id");

            AddColumn("dbo.TranHeaders", "LinkedAccount_Id", c => c.Int());
            CreateIndex("dbo.TranHeaders", "LinkedAccount_Id");
            AddForeignKey("dbo.TranHeaders", "LinkedAccount_Id", "dbo.Accounts", "Id");
   
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cards", "DefaultDebitAccount_Id", c => c.Int());
            AddColumn("dbo.Cards", "DefaultCreditAccount_Id", c => c.Int());
            DropForeignKey("dbo.TranHeaders", "LinkedAccount_Id", "dbo.Accounts");
            DropIndex("dbo.TranHeaders", new[] { "LinkedAccount_Id" });
            DropColumn("dbo.TranHeaders", "LinkedAccount_Id");
            CreateIndex("dbo.Cards", "DefaultDebitAccount_Id");
            CreateIndex("dbo.Cards", "DefaultCreditAccount_Id");
            AddForeignKey("dbo.Cards", "DefaultDebitAccount_Id", "dbo.Accounts", "Id");
            AddForeignKey("dbo.Cards", "DefaultCreditAccount_Id", "dbo.Accounts", "Id");
        }
    }
}
