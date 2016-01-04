namespace Decision.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUniqueOfTeacherTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Teachers", "IX_TeacherBirthCertificateNumber");
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId);
            
            CreateIndex("dbo.Teachers", "BirthCertificateNumber", name: "IX_TeacherBirthCertificateNumber");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "OwnerId", "dbo.Users");
            DropIndex("dbo.Notifications", new[] { "OwnerId" });
            DropIndex("dbo.Teachers", "IX_TeacherBirthCertificateNumber");
            DropTable("dbo.Notifications");
            CreateIndex("dbo.Teachers", "BirthCertificateNumber", unique: true, name: "IX_TeacherBirthCertificateNumber");
        }
    }
}
