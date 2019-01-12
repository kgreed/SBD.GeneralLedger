namespace SBD.GL.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class card : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TranHeaders", "Card_Id", c => c.Int());
            CreateIndex("dbo.TranHeaders", "Card_Id");
            AddForeignKey("dbo.TranHeaders", "Card_Id", "dbo.Cards", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TranHeaders", "Card_Id", "dbo.Cards");
            DropIndex("dbo.TranHeaders", new[] { "Card_Id" });
            DropColumn("dbo.TranHeaders", "Card_Id");
        }
    }
}
