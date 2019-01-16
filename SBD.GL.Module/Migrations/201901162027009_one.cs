namespace SBD.GL.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Accounts", name: "Category_Id", newName: "GLCategory_Id");
            RenameIndex(table: "dbo.Accounts", name: "IX_Category_Id", newName: "IX_GLCategory_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Accounts", name: "IX_GLCategory_Id", newName: "IX_Category_Id");
            RenameColumn(table: "dbo.Accounts", name: "GLCategory_Id", newName: "Category_Id");
        }
    }
}
