namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phonestring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AtvTours", "Phone", c => c.String());
            AlterColumn("dbo.BalloonTours", "Phone", c => c.String());
            AlterColumn("dbo.CamelTours", "Phone", c => c.String());
            AlterColumn("dbo.HorseTours", "Phone", c => c.String());
            AlterColumn("dbo.JeepTours", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JeepTours", "Phone", c => c.Int(nullable: false));
            AlterColumn("dbo.HorseTours", "Phone", c => c.Int(nullable: false));
            AlterColumn("dbo.CamelTours", "Phone", c => c.Int(nullable: false));
            AlterColumn("dbo.BalloonTours", "Phone", c => c.Int(nullable: false));
            AlterColumn("dbo.AtvTours", "Phone", c => c.Int(nullable: false));
        }
    }
}
