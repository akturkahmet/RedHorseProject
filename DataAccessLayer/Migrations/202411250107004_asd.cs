namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AtvTours", "CustomerCount", c => c.Int());
            AlterColumn("dbo.BalloonTours", "CustomerCount", c => c.Int());
            AlterColumn("dbo.CamelTours", "CustomerCount", c => c.Int());
            AlterColumn("dbo.HorseTours", "CustomerCount", c => c.Int());
            AlterColumn("dbo.JeepTours", "CustomerCount", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JeepTours", "CustomerCount", c => c.Int(nullable: false));
            AlterColumn("dbo.HorseTours", "CustomerCount", c => c.Int(nullable: false));
            AlterColumn("dbo.CamelTours", "CustomerCount", c => c.Int(nullable: false));
            AlterColumn("dbo.BalloonTours", "CustomerCount", c => c.Int(nullable: false));
            AlterColumn("dbo.AtvTours", "CustomerCount", c => c.Int(nullable: false));
        }
    }
}
