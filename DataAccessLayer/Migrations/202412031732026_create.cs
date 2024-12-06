namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Tours", newName: "Reservations");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Reservations", newName: "Tours");
        }
    }
}
