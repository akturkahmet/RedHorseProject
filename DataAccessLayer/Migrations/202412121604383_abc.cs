namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HoursCapacities",
                c => new
                    {
                        TourTypeId = c.String(nullable: false, maxLength: 128),
                        Hour = c.String(),
                        Capacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TourTypeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HoursCapacities");
        }
    }
}
