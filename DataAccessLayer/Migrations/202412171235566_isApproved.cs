namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isApproved : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agencies", "isApproved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Agencies", "isApproved");
        }
    }
}
