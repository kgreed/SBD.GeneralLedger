namespace SBD.GL.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tranheader : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TranHeaders", "StatementNumber", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TranHeaders", "StatementNumber");
        }
    }
}
