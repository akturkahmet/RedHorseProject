namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agencies", "Role", c => c.String());
            AlterColumn("dbo.Agencies", "Phone", c => c.String());
            AlterColumn("dbo.Agencies", "Tc", c => c.String());
            AlterColumn("dbo.Agencies", "TaxNo", c => c.String());
            AlterColumn("dbo.Agencies", "TursabNo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Agencies", "TursabNo", c => c.Int(nullable: false));
            AlterColumn("dbo.Agencies", "TaxNo", c => c.Int(nullable: false));
            AlterColumn("dbo.Agencies", "Tc", c => c.Int(nullable: false));
            AlterColumn("dbo.Agencies", "Phone", c => c.Int(nullable: false));
            DropColumn("dbo.Agencies", "Role");
        }
    }
}
