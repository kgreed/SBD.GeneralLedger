namespace SBD.GL.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _ref : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TranHeaders", "Reference", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TranHeaders", "Reference");
        }
    }
}
