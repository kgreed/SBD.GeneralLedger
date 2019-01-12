namespace SBD.GL.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ef : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transactions", "TranHeader_Id", "dbo.TranHeaders");
            AddForeignKey("dbo.Transactions", "TranHeader_Id", "dbo.TranHeaders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "TranHeader_Id", "dbo.TranHeaders");
            AddForeignKey("dbo.Transactions", "TranHeader_Id", "dbo.TranHeaders", "Id", cascadeDelete: true);
        }
    }
}
