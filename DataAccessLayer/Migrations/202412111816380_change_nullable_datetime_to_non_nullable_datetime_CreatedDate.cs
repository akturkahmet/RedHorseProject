namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_nullable_datetime_to_non_nullable_datetime_CreatedDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservations", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservations", "CreatedDate", c => c.DateTime());
        }
    }
}
