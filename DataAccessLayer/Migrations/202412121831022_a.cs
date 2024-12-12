namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "FirstName", c => c.String());
            AddColumn("dbo.Admins", "LastsName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admins", "LastsName");
            DropColumn("dbo.Admins", "FirstName");
        }
    }
}
