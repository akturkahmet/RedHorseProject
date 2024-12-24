namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfsbfvber : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservations", "CustomerCount", c => c.Int());
            AlterColumn("dbo.Reservations", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.Reservations", "ReservationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservations", "ReservationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Reservations", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Reservations", "CustomerCount", c => c.Int(nullable: false));
        }
    }
}
