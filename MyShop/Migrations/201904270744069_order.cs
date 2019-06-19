namespace MyShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "Order_Id" });
            RenameColumn(table: "dbo.OrderDetails", name: "Order_Id", newName: "OrderId");
            AlterColumn("dbo.OrderDetails", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderDetails", "OrderId");
            AddForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
            DropColumn("dbo.OrderDetails", "OrderHId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "OrderHId", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            AlterColumn("dbo.OrderDetails", "OrderId", c => c.Int());
            RenameColumn(table: "dbo.OrderDetails", name: "OrderId", newName: "Order_Id");
            CreateIndex("dbo.OrderDetails", "Order_Id");
            AddForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Orders", "Id");
        }
    }
}
