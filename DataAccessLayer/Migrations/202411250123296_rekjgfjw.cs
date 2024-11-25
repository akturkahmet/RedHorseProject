namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rekjgfjw : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AtvTours", "Agency_Id", "dbo.Agencies");
            DropForeignKey("dbo.BalloonTours", "Agency_Id", "dbo.Agencies");
            DropForeignKey("dbo.CamelTours", "Agency_Id", "dbo.Agencies");
            DropForeignKey("dbo.HorseTours", "Agency_Id", "dbo.Agencies");
            DropForeignKey("dbo.JeepTours", "Agency_Id", "dbo.Agencies");
            DropIndex("dbo.AtvTours", new[] { "Agency_Id" });
            DropIndex("dbo.BalloonTours", new[] { "Agency_Id" });
            DropIndex("dbo.CamelTours", new[] { "Agency_Id" });
            DropIndex("dbo.HorseTours", new[] { "Agency_Id" });
            DropIndex("dbo.JeepTours", new[] { "Agency_Id" });
            AddColumn("dbo.AtvTours", "Agency_Id1", c => c.Int());
            AddColumn("dbo.BalloonTours", "Agency_Id1", c => c.Int());
            AddColumn("dbo.CamelTours", "Agency_Id1", c => c.Int());
            AddColumn("dbo.HorseTours", "Agency_Id1", c => c.Int());
            AddColumn("dbo.JeepTours", "Agency_Id1", c => c.Int());
            CreateIndex("dbo.AtvTours", "Agency_Id1");
            CreateIndex("dbo.BalloonTours", "Agency_Id1");
            CreateIndex("dbo.CamelTours", "Agency_Id1");
            CreateIndex("dbo.HorseTours", "Agency_Id1");
            CreateIndex("dbo.JeepTours", "Agency_Id1");
            AddForeignKey("dbo.AtvTours", "Agency_Id1", "dbo.Agencies", "Id");
            AddForeignKey("dbo.BalloonTours", "Agency_Id1", "dbo.Agencies", "Id");
            AddForeignKey("dbo.CamelTours", "Agency_Id1", "dbo.Agencies", "Id");
            AddForeignKey("dbo.HorseTours", "Agency_Id1", "dbo.Agencies", "Id");
            AddForeignKey("dbo.JeepTours", "Agency_Id1", "dbo.Agencies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JeepTours", "Agency_Id1", "dbo.Agencies");
            DropForeignKey("dbo.HorseTours", "Agency_Id1", "dbo.Agencies");
            DropForeignKey("dbo.CamelTours", "Agency_Id1", "dbo.Agencies");
            DropForeignKey("dbo.BalloonTours", "Agency_Id1", "dbo.Agencies");
            DropForeignKey("dbo.AtvTours", "Agency_Id1", "dbo.Agencies");
            DropIndex("dbo.JeepTours", new[] { "Agency_Id1" });
            DropIndex("dbo.HorseTours", new[] { "Agency_Id1" });
            DropIndex("dbo.CamelTours", new[] { "Agency_Id1" });
            DropIndex("dbo.BalloonTours", new[] { "Agency_Id1" });
            DropIndex("dbo.AtvTours", new[] { "Agency_Id1" });
            DropColumn("dbo.JeepTours", "Agency_Id1");
            DropColumn("dbo.HorseTours", "Agency_Id1");
            DropColumn("dbo.CamelTours", "Agency_Id1");
            DropColumn("dbo.BalloonTours", "Agency_Id1");
            DropColumn("dbo.AtvTours", "Agency_Id1");
            CreateIndex("dbo.JeepTours", "Agency_Id");
            CreateIndex("dbo.HorseTours", "Agency_Id");
            CreateIndex("dbo.CamelTours", "Agency_Id");
            CreateIndex("dbo.BalloonTours", "Agency_Id");
            CreateIndex("dbo.AtvTours", "Agency_Id");
            AddForeignKey("dbo.JeepTours", "Agency_Id", "dbo.Agencies", "Id");
            AddForeignKey("dbo.HorseTours", "Agency_Id", "dbo.Agencies", "Id");
            AddForeignKey("dbo.CamelTours", "Agency_Id", "dbo.Agencies", "Id");
            AddForeignKey("dbo.BalloonTours", "Agency_Id", "dbo.Agencies", "Id");
            AddForeignKey("dbo.AtvTours", "Agency_Id", "dbo.Agencies", "Id");
        }
    }
}
