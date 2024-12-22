namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jfj : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservations", "HotelRoomNo", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservations", "HotelRoomNo", c => c.Int(nullable: false));
        }
    }
}
