namespace MyShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Credentials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameCredential = c.String(),
                        FullNameCredential = c.String(),
                        ParentCredentialId = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserEmail = c.String(),
                        UserPassword = c.String(),
                        UserName = c.String(),
                        UserCountry = c.String(),
                        UserAddress = c.String(),
                        UserRoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRoles", t => t.UserRoleId, cascadeDelete: true)
                .Index(t => t.UserRoleId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserRoleName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderHId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        OrderQTY = c.Int(nullable: false),
                        ProductPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductSum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        OrderNumber = c.String(),
                        OrderToUser = c.Int(nullable: false),
                        OrderAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderTypes", t => t.OrderTypeId, cascadeDelete: true)
                .Index(t => t.OrderTypeId);
            
            CreateTable(
                "dbo.OrderTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderTypeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductCode = c.String(),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                        Description = c.String(),
                        ProductGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroupId, cascadeDelete: true)
                .Index(t => t.ProductGroupId);
            
            CreateTable(
                "dbo.ProductGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                        GroupDescription = c.String(),
                        ProductCategoryid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryid, cascadeDelete: true)
                .Index(t => t.ProductCategoryid);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        CategoryDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserCredentials",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Credential_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Credential_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Credentials", t => t.Credential_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Credential_Id);
            
            CreateTable(
                "dbo.UserRoleCredentials",
                c => new
                    {
                        UserRole_Id = c.Int(nullable: false),
                        Credential_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRole_Id, t.Credential_Id })
                .ForeignKey("dbo.UserRoles", t => t.UserRole_Id, cascadeDelete: true)
                .ForeignKey("dbo.Credentials", t => t.Credential_Id, cascadeDelete: true)
                .Index(t => t.UserRole_Id)
                .Index(t => t.Credential_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductGroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductGroups", "ProductCategoryid", "dbo.ProductCategories");
            DropForeignKey("dbo.Orders", "OrderTypeId", "dbo.OrderTypes");
            DropForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Users", "UserRoleId", "dbo.UserRoles");
            DropForeignKey("dbo.UserRoleCredentials", "Credential_Id", "dbo.Credentials");
            DropForeignKey("dbo.UserRoleCredentials", "UserRole_Id", "dbo.UserRoles");
            DropForeignKey("dbo.UserCredentials", "Credential_Id", "dbo.Credentials");
            DropForeignKey("dbo.UserCredentials", "User_Id", "dbo.Users");
            DropIndex("dbo.UserRoleCredentials", new[] { "Credential_Id" });
            DropIndex("dbo.UserRoleCredentials", new[] { "UserRole_Id" });
            DropIndex("dbo.UserCredentials", new[] { "Credential_Id" });
            DropIndex("dbo.UserCredentials", new[] { "User_Id" });
            DropIndex("dbo.ProductGroups", new[] { "ProductCategoryid" });
            DropIndex("dbo.Products", new[] { "ProductGroupId" });
            DropIndex("dbo.Orders", new[] { "OrderTypeId" });
            DropIndex("dbo.OrderDetails", new[] { "Order_Id" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.Users", new[] { "UserRoleId" });
            DropTable("dbo.UserRoleCredentials");
            DropTable("dbo.UserCredentials");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.ProductGroups");
            DropTable("dbo.Products");
            DropTable("dbo.OrderTypes");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.Credentials");
        }
    }
}
