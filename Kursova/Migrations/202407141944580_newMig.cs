namespace Kursova.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newMig : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserDatas", newName: "Users");
            AlterColumn("dbo.Users", "Password", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Password", c => c.String());
            RenameTable(name: "dbo.Users", newName: "UserDatas");
        }
    }
}
