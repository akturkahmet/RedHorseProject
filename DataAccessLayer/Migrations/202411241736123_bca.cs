namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bca : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AtvTours", "Capacity", c => c.Int());
            AlterColumn("dbo.AtvTours", "HotelRoomNo", c => c.Int());
            AlterColumn("dbo.AtvTours", "CustomerCount", c => c.Int());
            AlterColumn("dbo.AtvTours", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.AtvTours", "ReservationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AtvTours", "ReservationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AtvTours", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AtvTours", "CustomerCount", c => c.Int(nullable: false));
            AlterColumn("dbo.AtvTours", "HotelRoomNo", c => c.Int(nullable: false));
            AlterColumn("dbo.AtvTours", "Capacity", c => c.Int(nullable: false));
        }
    }
}
