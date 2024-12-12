namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_Username_to_AgencyName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agencies", "AgencyName", c => c.String());
            DropColumn("dbo.Agencies", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Agencies", "UserName", c => c.String());
            DropColumn("dbo.Agencies", "AgencyName");
        }
    }
}
