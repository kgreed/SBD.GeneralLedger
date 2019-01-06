namespace SBD.GL.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h2cat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.H2Category",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Parent_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.H2Category", t => t.Parent_ID)
                .Index(t => t.Parent_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.H2Category", "Parent_ID", "dbo.H2Category");
            DropIndex("dbo.H2Category", new[] { "Parent_ID" });
            DropTable("dbo.H2Category");
        }
    }
}
