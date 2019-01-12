namespace SBD.GL.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class three1 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.TransactionHeader2");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TransactionHeader2",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
