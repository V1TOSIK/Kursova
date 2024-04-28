namespace Kursova.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserHealths", "VolumeOxygenInBlood", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserHealths", "VolumeOxygenInBlood", c => c.String());
        }
    }
}
