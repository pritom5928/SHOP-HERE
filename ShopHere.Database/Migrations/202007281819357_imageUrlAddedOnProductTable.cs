namespace ShopHere.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageUrlAddedOnProductTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.products", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.products", "ImageUrl");
        }
    }
}
