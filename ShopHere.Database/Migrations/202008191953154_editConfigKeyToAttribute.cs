namespace ShopHere.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editConfigKeyToAttribute : DbMigration
    {
        public override void Up()
        {
            //DropPrimaryKey("dbo.Configs");
            //AddColumn("dbo.Configs", "Attribute", c => c.String(nullable: false, maxLength: 128));
            //AddPrimaryKey("dbo.Configs", "Attribute");
            //DropColumn("dbo.Configs", "Key");

            RenameColumn("dbo.Configs", "Key", "Attribute");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Configs", "Key", c => c.String(nullable: false, maxLength: 128));
            //DropPrimaryKey("dbo.Configs");
            //DropColumn("dbo.Configs", "Attribute");
            //AddPrimaryKey("dbo.Configs", "Key");

            RenameColumn("dbo.Configs", "Key", "Attribute");
        }
    }
}
