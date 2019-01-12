namespace SBD.GL.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class req : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Transactions", new[] { "TranHeader_Id" });
            AlterColumn("dbo.Transactions", "TranHeader_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Transactions", "TranHeader_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Transactions", new[] { "TranHeader_Id" });
            AlterColumn("dbo.Transactions", "TranHeader_Id", c => c.Int());
            CreateIndex("dbo.Transactions", "TranHeader_Id");
        }
    }
}
