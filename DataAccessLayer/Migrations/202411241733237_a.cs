namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AtvTours", "AgenciesId", "dbo.Agencies");
            DropIndex("dbo.AtvTours", new[] { "AgenciesId" });
            AlterColumn("dbo.AtvTours", "AgenciesId", c => c.Int());
            CreateIndex("dbo.AtvTours", "AgenciesId");
            AddForeignKey("dbo.AtvTours", "AgenciesId", "dbo.Agencies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AtvTours", "AgenciesId", "dbo.Agencies");
            DropIndex("dbo.AtvTours", new[] { "AgenciesId" });
            AlterColumn("dbo.AtvTours", "AgenciesId", c => c.Int(nullable: false));
            CreateIndex("dbo.AtvTours", "AgenciesId");
            AddForeignKey("dbo.AtvTours", "AgenciesId", "dbo.Agencies", "Id", cascadeDelete: true);
        }
    }
}
