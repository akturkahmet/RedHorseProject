namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdjf : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.HoursCapacities");
            AddColumn("dbo.HoursCapacities", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.HoursCapacities", "TourTypeId", c => c.String());
            AddPrimaryKey("dbo.HoursCapacities", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.HoursCapacities");
            AlterColumn("dbo.HoursCapacities", "TourTypeId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.HoursCapacities", "Id");
            AddPrimaryKey("dbo.HoursCapacities", "TourTypeId");
        }
    }
}
