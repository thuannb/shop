namespace Shop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductCategoryAndProduct : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Products", "ProductCategoryID");
            AddForeignKey("dbo.Products", "ProductCategoryID", "dbo.ProductCategories", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProductCategoryID", "dbo.ProductCategories");
            DropIndex("dbo.Products", new[] { "ProductCategoryID" });
        }
    }
}
