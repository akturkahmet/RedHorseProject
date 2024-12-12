namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fdhh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HoursCapacities", "Status", c => c.Boolean(nullable: false));
            DropColumn("dbo.TourTypes", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TourTypes", "Status", c => c.Boolean(nullable: false));
            DropColumn("dbo.HoursCapacities", "Status");
        }
    }
}
