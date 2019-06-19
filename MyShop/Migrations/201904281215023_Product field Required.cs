namespace MyShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductfieldRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ProductCode", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Name", c => c.String());
            AlterColumn("dbo.Products", "ProductCode", c => c.String());
        }
    }
}
