namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class date : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AtvTours", "ReservationDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.AtvTours", "ReservationTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AtvTours", "ReservationTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.AtvTours", "ReservationDate");
        }
    }
}
