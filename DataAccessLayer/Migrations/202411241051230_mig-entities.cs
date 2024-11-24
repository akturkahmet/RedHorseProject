namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migentities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AtvTours", "ReservationTime", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.BalloonTours", "BallonCount", c => c.Int(nullable: false));
            AddColumn("dbo.BalloonTours", "FirstName", c => c.String());
            AddColumn("dbo.BalloonTours", "LastName", c => c.String());
            AddColumn("dbo.BalloonTours", "Mail", c => c.String());
            AddColumn("dbo.BalloonTours", "CountryCode", c => c.String());
            AddColumn("dbo.BalloonTours", "Phone", c => c.Int(nullable: false));
            AddColumn("dbo.BalloonTours", "HotelName", c => c.String());
            AddColumn("dbo.BalloonTours", "HotelRoomNo", c => c.Int(nullable: false));
            AddColumn("dbo.BalloonTours", "PassportNo", c => c.String());
            AddColumn("dbo.BalloonTours", "CustomerCount", c => c.Int(nullable: false));
            AddColumn("dbo.BalloonTours", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.BalloonTours", "ReservationTime", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.BalloonTours", "TourNote", c => c.String());
            AddColumn("dbo.BalloonTours", "AgenciesId", c => c.Int(nullable: false));
            AddColumn("dbo.CamelTours", "ReservationTime", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.CamelTours", "AgenciesId", c => c.Int(nullable: false));
            AddColumn("dbo.HorseTours", "ReservationTime", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.HorseTours", "AgenciesId", c => c.Int(nullable: false));
            AddColumn("dbo.JeepTours", "ReservationTime", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.JeepTours", "AgenciesId", c => c.Int(nullable: false));
            CreateIndex("dbo.BalloonTours", "AgenciesId");
            CreateIndex("dbo.CamelTours", "AgenciesId");
            CreateIndex("dbo.HorseTours", "AgenciesId");
            CreateIndex("dbo.JeepTours", "AgenciesId");
            AddForeignKey("dbo.BalloonTours", "AgenciesId", "dbo.Agencies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CamelTours", "AgenciesId", "dbo.Agencies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.HorseTours", "AgenciesId", "dbo.Agencies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.JeepTours", "AgenciesId", "dbo.Agencies", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JeepTours", "AgenciesId", "dbo.Agencies");
            DropForeignKey("dbo.HorseTours", "AgenciesId", "dbo.Agencies");
            DropForeignKey("dbo.CamelTours", "AgenciesId", "dbo.Agencies");
            DropForeignKey("dbo.BalloonTours", "AgenciesId", "dbo.Agencies");
            DropIndex("dbo.JeepTours", new[] { "AgenciesId" });
            DropIndex("dbo.HorseTours", new[] { "AgenciesId" });
            DropIndex("dbo.CamelTours", new[] { "AgenciesId" });
            DropIndex("dbo.BalloonTours", new[] { "AgenciesId" });
            DropColumn("dbo.JeepTours", "AgenciesId");
            DropColumn("dbo.JeepTours", "ReservationTime");
            DropColumn("dbo.HorseTours", "AgenciesId");
            DropColumn("dbo.HorseTours", "ReservationTime");
            DropColumn("dbo.CamelTours", "AgenciesId");
            DropColumn("dbo.CamelTours", "ReservationTime");
            DropColumn("dbo.BalloonTours", "AgenciesId");
            DropColumn("dbo.BalloonTours", "TourNote");
            DropColumn("dbo.BalloonTours", "ReservationTime");
            DropColumn("dbo.BalloonTours", "CreatedDate");
            DropColumn("dbo.BalloonTours", "CustomerCount");
            DropColumn("dbo.BalloonTours", "PassportNo");
            DropColumn("dbo.BalloonTours", "HotelRoomNo");
            DropColumn("dbo.BalloonTours", "HotelName");
            DropColumn("dbo.BalloonTours", "Phone");
            DropColumn("dbo.BalloonTours", "CountryCode");
            DropColumn("dbo.BalloonTours", "Mail");
            DropColumn("dbo.BalloonTours", "LastName");
            DropColumn("dbo.BalloonTours", "FirstName");
            DropColumn("dbo.BalloonTours", "BallonCount");
            DropColumn("dbo.AtvTours", "ReservationTime");
        }
    }
}
