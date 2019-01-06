namespace SBD.GL.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class two : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HCategories", "Parent_ID", "dbo.HCategories");
            DropIndex("dbo.HCategories", new[] { "Parent_ID" });
            DropTable("dbo.HCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.HCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Parent_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.HCategories", "Parent_ID");
            AddForeignKey("dbo.HCategories", "Parent_ID", "dbo.HCategories", "ID");
        }
    }
}
