namespace DerbyManagement.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Derbies",
                c => new
                    {
                        DerbyId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        Lanes = c.Int(nullable: false),
                        HasChampionship = c.Boolean(nullable: false),
                        DivisionPlacesToAdvance = c.Int(nullable: false),
                        ScoringType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DerbyId);
            
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        DivisionId = c.Int(nullable: false, identity: true),
                        DerbyId = c.Int(nullable: false),
                        Sequence = c.Int(nullable: false),
                        Name = c.String(),
                        LogoPath = c.String(),
                        IncludeInChampionship = c.Boolean(nullable: false),
                        IsChampionship = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DivisionId)
                .ForeignKey("dbo.Derbies", t => t.DerbyId, cascadeDelete: true)
                .Index(t => t.DerbyId);
            
            CreateTable(
                "dbo.Heats",
                c => new
                    {
                        HeatId = c.Int(nullable: false, identity: true),
                        DivisionId = c.Int(nullable: false),
                        Sequence = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HeatId)
                .ForeignKey("dbo.Divisions", t => t.DivisionId, cascadeDelete: true)
                .Index(t => t.DivisionId);
            
            CreateTable(
                "dbo.Runs",
                c => new
                    {
                        RunId = c.Int(nullable: false, identity: true),
                        HeatId = c.Int(nullable: false),
                        RacerId = c.Int(nullable: false),
                        Lane = c.Int(nullable: false),
                        Place = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RunId)
                .ForeignKey("dbo.Heats", t => t.HeatId, cascadeDelete: true)
                .ForeignKey("dbo.Racers", t => t.RacerId, cascadeDelete: true)
                .Index(t => t.HeatId)
                .Index(t => t.RacerId);
            
            CreateTable(
                "dbo.Racers",
                c => new
                    {
                        RacerId = c.Int(nullable: false, identity: true),
                        CarNumber = c.Int(nullable: false),
                        CarName = c.String(),
                        OwnerName = c.String(),
                    })
                .PrimaryKey(t => t.RacerId);
            
            CreateTable(
                "dbo.RacerDivisions",
                c => new
                    {
                        Racer_RacerId = c.Int(nullable: false),
                        Division_DivisionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Racer_RacerId, t.Division_DivisionId })
                .ForeignKey("dbo.Racers", t => t.Racer_RacerId, cascadeDelete: true)
                .ForeignKey("dbo.Divisions", t => t.Division_DivisionId, cascadeDelete: true)
                .Index(t => t.Racer_RacerId)
                .Index(t => t.Division_DivisionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Runs", "RacerId", "dbo.Racers");
            DropForeignKey("dbo.RacerDivisions", "Division_DivisionId", "dbo.Divisions");
            DropForeignKey("dbo.RacerDivisions", "Racer_RacerId", "dbo.Racers");
            DropForeignKey("dbo.Runs", "HeatId", "dbo.Heats");
            DropForeignKey("dbo.Heats", "DivisionId", "dbo.Divisions");
            DropForeignKey("dbo.Divisions", "DerbyId", "dbo.Derbies");
            DropIndex("dbo.RacerDivisions", new[] { "Division_DivisionId" });
            DropIndex("dbo.RacerDivisions", new[] { "Racer_RacerId" });
            DropIndex("dbo.Runs", new[] { "RacerId" });
            DropIndex("dbo.Runs", new[] { "HeatId" });
            DropIndex("dbo.Heats", new[] { "DivisionId" });
            DropIndex("dbo.Divisions", new[] { "DerbyId" });
            DropTable("dbo.RacerDivisions");
            DropTable("dbo.Racers");
            DropTable("dbo.Runs");
            DropTable("dbo.Heats");
            DropTable("dbo.Divisions");
            DropTable("dbo.Derbies");
        }
    }
}
