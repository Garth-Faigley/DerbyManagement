namespace DerbyManagement.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DivisionNameRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Divisions", "Name", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Divisions", "Name", c => c.String(maxLength: 255));
        }
    }
}
