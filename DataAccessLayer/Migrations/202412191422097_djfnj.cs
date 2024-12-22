namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class djfnj : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SpecificDateCapacities", "Day", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SpecificDateCapacities", "Day", c => c.DateTime(nullable: false));
        }
    }
}
