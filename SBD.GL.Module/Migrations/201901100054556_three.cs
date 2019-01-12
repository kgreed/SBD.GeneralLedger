namespace SBD.GL.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class three : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "TranHeader_Id", c => c.Int());
            CreateIndex("dbo.Transactions", "TranHeader_Id");
            AddForeignKey("dbo.Transactions", "TranHeader_Id", "dbo.TranHeaders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "TranHeader_Id", "dbo.TranHeaders");
            DropIndex("dbo.Transactions", new[] { "TranHeader_Id" });
            DropColumn("dbo.Transactions", "TranHeader_Id");
        }
    }
}
