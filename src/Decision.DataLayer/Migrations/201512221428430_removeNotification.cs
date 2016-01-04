namespace Decision.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeNotification : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notifications", "OwnerId", "dbo.Users");
            DropIndex("dbo.Notifications", new[] { "OwnerId" });
            DropTable("dbo.Notifications");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        Message = c.String(nullable: false, maxLength: 1024),
                        Read = c.Boolean(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Notifications", "OwnerId");
            AddForeignKey("dbo.Notifications", "OwnerId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
