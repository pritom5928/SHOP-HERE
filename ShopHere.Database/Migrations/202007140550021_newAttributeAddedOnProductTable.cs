namespace ShopHere.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newAttributeAddedOnProductTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.products", "Category_Id", "dbo.Categories");
            DropIndex("dbo.products", new[] { "Category_Id" });
            RenameColumn(table: "dbo.products", name: "Category_Id", newName: "CategoryId");
            AlterColumn("dbo.products", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.products", "CategoryId");
            AddForeignKey("dbo.products", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.products", new[] { "CategoryId" });
            AlterColumn("dbo.products", "CategoryId", c => c.Int());
            RenameColumn(table: "dbo.products", name: "CategoryId", newName: "Category_Id");
            CreateIndex("dbo.products", "Category_Id");
            AddForeignKey("dbo.products", "Category_Id", "dbo.Categories", "Id");
        }
    }
}
