namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _as : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AtvTours", "AgenciesId", "dbo.Agencies");
            DropForeignKey("dbo.BalloonTours", "AgenciesId", "dbo.Agencies");
            DropForeignKey("dbo.CamelTours", "AgenciesId", "dbo.Agencies");
            DropForeignKey("dbo.HorseTours", "AgenciesId", "dbo.Agencies");
            DropForeignKey("dbo.JeepTours", "AgenciesId", "dbo.Agencies");
            DropIndex("dbo.BalloonTours", new[] { "AgenciesId" });
            DropIndex("dbo.CamelTours", new[] { "AgenciesId" });
            DropIndex("dbo.HorseTours", new[] { "AgenciesId" });
            DropIndex("dbo.JeepTours", new[] { "AgenciesId" });
            AddColumn("dbo.AtvTours", "ReservationTime", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.AtvTours", "Agency_Id", c => c.Int());
            AddColumn("dbo.BalloonTours", "Agency_Id", c => c.Int());
            AddColumn("dbo.CamelTours", "Agency_Id", c => c.Int());
            AddColumn("dbo.HorseTours", "Agency_Id", c => c.Int());
            AddColumn("dbo.JeepTours", "Agency_Id", c => c.Int());
            AlterColumn("dbo.AtvTours", "HotelRoomNo", c => c.Int(nullable: false));
            AlterColumn("dbo.AtvTours", "CustomerCount", c => c.Int(nullable: false));
            AlterColumn("dbo.BalloonTours", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.BalloonTours", "AgenciesId", c => c.Int());
            AlterColumn("dbo.CamelTours", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.CamelTours", "AgenciesId", c => c.Int());
            AlterColumn("dbo.HorseTours", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.HorseTours", "AgenciesId", c => c.Int());
            AlterColumn("dbo.JeepTours", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.JeepTours", "AgenciesId", c => c.Int());
            CreateIndex("dbo.AtvTours", "Agency_Id");
            CreateIndex("dbo.BalloonTours", "AgenciesId");
            CreateIndex("dbo.BalloonTours", "Agency_Id");
            CreateIndex("dbo.CamelTours", "AgenciesId");
            CreateIndex("dbo.CamelTours", "Agency_Id");
            CreateIndex("dbo.HorseTours", "AgenciesId");
            CreateIndex("dbo.HorseTours", "Agency_Id");
            CreateIndex("dbo.JeepTours", "AgenciesId");
            CreateIndex("dbo.JeepTours", "Agency_Id");
            AddForeignKey("dbo.AtvTours", "AgenciesId", "dbo.Agencies", "Id");
            AddForeignKey("dbo.BalloonTours", "AgenciesId", "dbo.Agencies", "Id");
            AddForeignKey("dbo.CamelTours", "AgenciesId", "dbo.Agencies", "Id");
            AddForeignKey("dbo.HorseTours", "AgenciesId", "dbo.Agencies", "Id");
            AddForeignKey("dbo.JeepTours", "AgenciesId", "dbo.Agencies", "Id");
            AddForeignKey("dbo.AtvTours", "Agency_Id", "dbo.Agencies", "Id");
            AddForeignKey("dbo.BalloonTours", "Agency_Id", "dbo.Agencies", "Id");
            AddForeignKey("dbo.CamelTours", "Agency_Id", "dbo.Agencies", "Id");
            AddForeignKey("dbo.HorseTours", "Agency_Id", "dbo.Agencies", "Id");
            AddForeignKey("dbo.JeepTours", "Agency_Id", "dbo.Agencies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JeepTours", "Agency_Id", "dbo.Agencies");
            DropForeignKey("dbo.HorseTours", "Agency_Id", "dbo.Agencies");
            DropForeignKey("dbo.CamelTours", "Agency_Id", "dbo.Agencies");
            DropForeignKey("dbo.BalloonTours", "Agency_Id", "dbo.Agencies");
            DropForeignKey("dbo.AtvTours", "Agency_Id", "dbo.Agencies");
            DropForeignKey("dbo.JeepTours", "AgenciesId", "dbo.Agencies");
            DropForeignKey("dbo.HorseTours", "AgenciesId", "dbo.Agencies");
            DropForeignKey("dbo.CamelTours", "AgenciesId", "dbo.Agencies");
            DropForeignKey("dbo.BalloonTours", "AgenciesId", "dbo.Agencies");
            DropForeignKey("dbo.AtvTours", "AgenciesId", "dbo.Agencies");
            DropIndex("dbo.JeepTours", new[] { "Agency_Id" });
            DropIndex("dbo.JeepTours", new[] { "AgenciesId" });
            DropIndex("dbo.HorseTours", new[] { "Agency_Id" });
            DropIndex("dbo.HorseTours", new[] { "AgenciesId" });
            DropIndex("dbo.CamelTours", new[] { "Agency_Id" });
            DropIndex("dbo.CamelTours", new[] { "AgenciesId" });
            DropIndex("dbo.BalloonTours", new[] { "Agency_Id" });
            DropIndex("dbo.BalloonTours", new[] { "AgenciesId" });
            DropIndex("dbo.AtvTours", new[] { "Agency_Id" });
            AlterColumn("dbo.JeepTours", "AgenciesId", c => c.Int(nullable: false));
            AlterColumn("dbo.JeepTours", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.HorseTours", "AgenciesId", c => c.Int(nullable: false));
            AlterColumn("dbo.HorseTours", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CamelTours", "AgenciesId", c => c.Int(nullable: false));
            AlterColumn("dbo.CamelTours", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BalloonTours", "AgenciesId", c => c.Int(nullable: false));
            AlterColumn("dbo.BalloonTours", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AtvTours", "CustomerCount", c => c.Int());
            AlterColumn("dbo.AtvTours", "HotelRoomNo", c => c.Int());
            DropColumn("dbo.JeepTours", "Agency_Id");
            DropColumn("dbo.HorseTours", "Agency_Id");
            DropColumn("dbo.CamelTours", "Agency_Id");
            DropColumn("dbo.BalloonTours", "Agency_Id");
            DropColumn("dbo.AtvTours", "Agency_Id");
            DropColumn("dbo.AtvTours", "ReservationTime");
            CreateIndex("dbo.JeepTours", "AgenciesId");
            CreateIndex("dbo.HorseTours", "AgenciesId");
            CreateIndex("dbo.CamelTours", "AgenciesId");
            CreateIndex("dbo.BalloonTours", "AgenciesId");
            AddForeignKey("dbo.JeepTours", "AgenciesId", "dbo.Agencies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.HorseTours", "AgenciesId", "dbo.Agencies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CamelTours", "AgenciesId", "dbo.Agencies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BalloonTours", "AgenciesId", "dbo.Agencies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AtvTours", "AgenciesId", "dbo.Agencies", "Id");
        }
    }
}
