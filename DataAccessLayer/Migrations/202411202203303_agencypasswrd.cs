namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agencypasswrd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agencies", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Agencies", "Password");
        }
    }
}
