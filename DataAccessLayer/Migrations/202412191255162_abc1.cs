namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abc1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpecificDateCapacities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourTypeId = c.String(),
                        Day = c.DateTime(nullable: false),
                        Hour = c.String(),
                        Capacity = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SpecificDateCapacities");
        }
    }
}
