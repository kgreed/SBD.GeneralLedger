namespace SBD.GL.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bank2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Cards", name: "DefaultAccount_Id", newName: "DefaultCreditAccount_Id");
            RenameIndex(table: "dbo.Cards", name: "IX_DefaultAccount_Id", newName: "IX_DefaultCreditAccount_Id");
            AddColumn("dbo.Cards", "DefaultDebitAccount_Id", c => c.Int());
            CreateIndex("dbo.Cards", "DefaultDebitAccount_Id");
            AddForeignKey("dbo.Cards", "DefaultDebitAccount_Id", "dbo.Accounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "DefaultDebitAccount_Id", "dbo.Accounts");
            DropIndex("dbo.Cards", new[] { "DefaultDebitAccount_Id" });
            DropColumn("dbo.Cards", "DefaultDebitAccount_Id");
            RenameIndex(table: "dbo.Cards", name: "IX_DefaultCreditAccount_Id", newName: "IX_DefaultAccount_Id");
            RenameColumn(table: "dbo.Cards", name: "DefaultCreditAccount_Id", newName: "DefaultAccount_Id");
        }
    }
}
