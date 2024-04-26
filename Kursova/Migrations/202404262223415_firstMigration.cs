namespace Kursova.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserActivities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExerciseName = c.String(),
                        ConsumedCalories = c.Double(nullable: false),
                        BurnedCalories = c.Double(nullable: false),
                        Steps = c.Int(nullable: false),
                        Traveled = c.Double(nullable: false),
                        DateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDates", t => t.DateId, cascadeDelete: true)
                .Index(t => t.DateId);
            
            CreateTable(
                "dbo.UserDates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Datetime = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDatas", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserHealths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pulse = c.Int(nullable: false),
                        Pressure = c.String(),
                        VolumeOxygenInBlood = c.String(),
                        DateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDates", t => t.DateId, cascadeDelete: true)
                .Index(t => t.DateId);
            
            CreateTable(
                "dbo.UserDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDates", "UserId", "dbo.UserDatas");
            DropForeignKey("dbo.UserHealths", "DateId", "dbo.UserDates");
            DropForeignKey("dbo.UserActivities", "DateId", "dbo.UserDates");
            DropIndex("dbo.UserHealths", new[] { "DateId" });
            DropIndex("dbo.UserDates", new[] { "UserId" });
            DropIndex("dbo.UserActivities", new[] { "DateId" });
            DropTable("dbo.UserDatas");
            DropTable("dbo.UserHealths");
            DropTable("dbo.UserDates");
            DropTable("dbo.UserActivities");
        }
    }
}
