namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agencies", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.AtvTours", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.BalloonTours", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.CamelTours", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.HorseTours", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.JeepTours", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JeepTours", "Status");
            DropColumn("dbo.HorseTours", "Status");
            DropColumn("dbo.CamelTours", "Status");
            DropColumn("dbo.BalloonTours", "Status");
            DropColumn("dbo.AtvTours", "Status");
            DropColumn("dbo.Agencies", "Status");
        }
    }
}
