namespace Shop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldTagsForProducts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Tags", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Tags");
        }
    }
}
