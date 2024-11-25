namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedisDeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agencies", "isDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Agencies", "isDeleted");
        }
    }
}
