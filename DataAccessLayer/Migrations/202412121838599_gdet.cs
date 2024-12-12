namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gdet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "LastName", c => c.String());
            DropColumn("dbo.Admins", "LastsName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Admins", "LastsName", c => c.String());
            DropColumn("dbo.Admins", "LastName");
        }
    }
}
