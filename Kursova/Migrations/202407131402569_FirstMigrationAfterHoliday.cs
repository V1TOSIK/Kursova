namespace Kursova.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigrationAfterHoliday : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "UserDatas");
            AlterColumn("dbo.UserDatas", "Password", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserDatas", "Password", c => c.Int(nullable: false));
            RenameTable(name: "dbo.UserDatas", newName: "Users");
        }
    }
}
