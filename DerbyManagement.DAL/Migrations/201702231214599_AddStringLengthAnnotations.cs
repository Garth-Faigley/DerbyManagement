namespace DerbyManagement.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStringLengthAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Derbies", "Name", c => c.String(maxLength: 255));
            AlterColumn("dbo.Divisions", "Name", c => c.String(maxLength: 255));
            AlterColumn("dbo.Divisions", "LogoPath", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Racers", "CarName", c => c.String(maxLength: 255));
            AlterColumn("dbo.Racers", "OwnerFirstName", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Racers", "OwnerFirstName", c => c.String());
            AlterColumn("dbo.Racers", "CarName", c => c.String());
            AlterColumn("dbo.Divisions", "LogoPath", c => c.String());
            AlterColumn("dbo.Divisions", "Name", c => c.String());
            AlterColumn("dbo.Derbies", "Name", c => c.String());
        }
    }
}
