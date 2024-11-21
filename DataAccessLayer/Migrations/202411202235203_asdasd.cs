namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdasd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AtvTours", "AgenciesId", c => c.Int(nullable: true));
            CreateIndex("dbo.AtvTours", "AgenciesId");
            AddForeignKey("dbo.AtvTours", "AgenciesId", "dbo.Agencies", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AtvTours", "AgenciesId", "dbo.Agencies");
            DropIndex("dbo.AtvTours", new[] { "AgenciesId" });
            DropColumn("dbo.AtvTours", "AgenciesId");
        }
    }
}
