namespace SBD.GL.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TransactionHeader2", "Card_Id", "dbo.Cards");
            DropIndex("dbo.TransactionHeader2", new[] { "Card_Id" });
            DropColumn("dbo.TransactionHeader2", "Date");
            DropColumn("dbo.TransactionHeader2", "Card_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TransactionHeader2", "Card_Id", c => c.Int());
            AddColumn("dbo.TransactionHeader2", "Date", c => c.DateTime(nullable: false));
            CreateIndex("dbo.TransactionHeader2", "Card_Id");
            AddForeignKey("dbo.TransactionHeader2", "Card_Id", "dbo.Cards", "Id");
        }
    }
}
