namespace Shop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateColumn : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.IdentityUserRoles");
            AddColumn("dbo.ApplicationUsers", "FullName", c => c.String(maxLength: 255));
            AddColumn("dbo.ApplicationUsers", "Address", c => c.String(maxLength: 255));
            AddColumn("dbo.ApplicationUsers", "BirthDay", c => c.DateTime());
            AlterColumn("dbo.IdentityUserRoles", "RoleId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.IdentityUserRoles", new[] { "UserId", "RoleId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.IdentityUserRoles");
            AlterColumn("dbo.IdentityUserRoles", "RoleId", c => c.String());
            DropColumn("dbo.ApplicationUsers", "BirthDay");
            DropColumn("dbo.ApplicationUsers", "Address");
            DropColumn("dbo.ApplicationUsers", "FullName");
            AddPrimaryKey("dbo.IdentityUserRoles", "UserId");
        }
    }
}
