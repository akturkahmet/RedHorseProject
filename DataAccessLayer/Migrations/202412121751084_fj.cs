namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fj : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservations", "CustomerCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservations", "CustomerCount", c => c.Int());
        }
    }
}
