namespace ShopHere.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validationsAddedOnServerside : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Categories", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.products", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.products", "Description", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.products", "Description", c => c.String());
            AlterColumn("dbo.products", "Name", c => c.String());
            AlterColumn("dbo.Categories", "Description", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String());
        }
    }
}
