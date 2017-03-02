namespace DerbyManagement.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImplementIModificationHistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Derbies", "DateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.Derbies", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Divisions", "DateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.Divisions", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Heats", "DateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.Heats", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Runs", "DateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.Runs", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Racers", "DateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.Racers", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Racers", "DateCreated");
            DropColumn("dbo.Racers", "DateModified");
            DropColumn("dbo.Runs", "DateCreated");
            DropColumn("dbo.Runs", "DateModified");
            DropColumn("dbo.Heats", "DateCreated");
            DropColumn("dbo.Heats", "DateModified");
            DropColumn("dbo.Divisions", "DateCreated");
            DropColumn("dbo.Divisions", "DateModified");
            DropColumn("dbo.Derbies", "DateCreated");
            DropColumn("dbo.Derbies", "DateModified");
        }
    }
}
