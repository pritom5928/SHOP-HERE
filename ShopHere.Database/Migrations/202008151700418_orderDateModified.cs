namespace ShopHere.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderDateModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Orders", "productDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "productDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Orders", "OrderDate");
        }
    }
}
