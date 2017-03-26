namespace DerbyManagement.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DerbyNameRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Derbies", "Name", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Derbies", "Name", c => c.String(maxLength: 255));
        }
    }
}
