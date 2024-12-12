namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ghj : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.HoursCapacities");
            AlterColumn("dbo.HoursCapacities", "TourTypeId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.HoursCapacities", "TourTypeId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.HoursCapacities");
            AlterColumn("dbo.HoursCapacities", "TourTypeId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.HoursCapacities", "TourTypeId");
        }
    }
}
