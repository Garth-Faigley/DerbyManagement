namespace DerbyManagement.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RacerValidationAndNameSplit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Racers", "OwnerLastName", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Racers", "OwnerFirstName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Racers", "CarName", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Racers", "OwnerName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Racers", "OwnerName", c => c.String(maxLength: 255));
            AlterColumn("dbo.Racers", "CarName", c => c.String(maxLength: 255));
            DropColumn("dbo.Racers", "OwnerFirstName");
            DropColumn("dbo.Racers", "OwnerLastName");
        }
    }
}
