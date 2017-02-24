namespace Decision.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityLogs",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Title = c.String(nullable: false, maxLength: 256),
                    Description = c.String(maxLength: 512),
                    Url = c.String(maxLength: 1024),
                    OperantIp = c.String(nullable: false, maxLength: 20),
                    OperatedOn = c.DateTime(nullable: false),
                    UserId = c.Long(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    IsBanned = c.Boolean(nullable: false),
                    IsSystemAccount = c.Boolean(nullable: false),
                    LastIp = c.String(maxLength: 20),
                    LastLoggedInOn = c.DateTime(),
                    LastActivityOn = c.DateTime(),
                    BannedOn = c.DateTime(),
                    DisplayName = c.String(nullable: false, maxLength: 50),
                    TrimmedDisplayName = c.String(nullable: false, maxLength: 50),
                    BannedReason = c.String(maxLength: 256),
                    RegisteredOn = c.DateTime(nullable: false),
                    Email = c.String(nullable: false, maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(maxLength: 20),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                    CreatedOn = c.DateTime(nullable: false),
                    LastModifiedOn = c.DateTime(nullable: false),
                    CreatorIp = c.String(nullable: false, maxLength: 20),
                    LastModifierIp = c.String(nullable: false, maxLength: 20),
                    LastModifiedBy = c.String(nullable: false, maxLength: 50),
                    CreatedBy = c.String(nullable: false, maxLength: 50),
                    RowId = c.Guid(nullable: false),
                    RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, unique: true, name: "UIX_User_Email")
                .Index(t => t.UserName, unique: true, name: "UIX_User_UserName");

            CreateTable(
                "dbo.AuditLogs",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    EntityId = c.Long(nullable: false),
                    EntityType = c.String(nullable: false, maxLength: 50),
                    JsonOriginalValues = c.String(nullable: false),
                    JsonNewValues = c.String(nullable: false),
                    Action = c.Int(nullable: false),
                    UserId = c.Long(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.UserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.Long(nullable: false),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.UserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.Long(nullable: false),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.Notifications",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    IsDismissed = c.Boolean(nullable: false),
                    Message = c.String(maxLength: 512),
                    Title = c.String(nullable: false, maxLength: 256),
                    Url = c.String(maxLength: 1024),
                    ReceivedOn = c.DateTime(nullable: false),
                    Type = c.Int(nullable: false),
                    UserId = c.Long(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.UserRole",
                c => new
                {
                    UserId = c.Long(nullable: false),
                    RoleId = c.Long(nullable: false),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.Roles",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    IsSystemRole = c.Boolean(nullable: false),
                    DisplayName = c.String(nullable: false, maxLength: 50),
                    Name = c.String(nullable: false, maxLength: 50),
                    CreatedOn = c.DateTime(nullable: false),
                    LastModifiedOn = c.DateTime(nullable: false),
                    CreatorIp = c.String(nullable: false, maxLength: 20),
                    LastModifierIp = c.String(nullable: false, maxLength: 20),
                    LastModifiedBy = c.String(nullable: false, maxLength: 50),
                    CreatedBy = c.String(nullable: false, maxLength: 50),
                    RowId = c.Guid(nullable: false),
                    RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "UIX_Role_RoleName");

            CreateTable(
                "dbo.Settings",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Name = c.String(),
                    Value = c.String(),
                    CreatedOn = c.DateTime(nullable: false),
                    LastModifiedOn = c.DateTime(nullable: false),
                    CreatorIp = c.String(),
                    LastModifierIp = c.String(),
                    LastModifiedBy = c.String(),
                    CreatedBy = c.String(),
                    RowId = c.Guid(nullable: false),
                    RowVersion = c.Binary(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.UserTokens",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    AccessTokenHash = c.String(nullable: false, maxLength: 256),
                    AccessTokenExpireOn = c.DateTime(nullable: false),
                    RefreshTokenIdHash = c.String(),
                    Subject = c.String(nullable: false, maxLength: 256),
                    RefreshTokenExpiresUtc = c.DateTime(nullable: false),
                    RefreshToken = c.String(nullable: false, maxLength: 256),
                    UserId = c.Long(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.UserTokens", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.Users");
            DropForeignKey("dbo.Notifications", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.AuditLogs", "UserId", "dbo.Users");
            DropForeignKey("dbo.ActivityLogs", "UserId", "dbo.Users");
            DropIndex("dbo.UserTokens", new[] { "UserId" });
            DropIndex("dbo.Roles", "UIX_Role_RoleName");
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.Notifications", new[] { "UserId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.AuditLogs", new[] { "UserId" });
            DropIndex("dbo.Users", "UIX_User_UserName");
            DropIndex("dbo.Users", "UIX_User_Email");
            DropIndex("dbo.ActivityLogs", new[] { "UserId" });
            DropTable("dbo.UserTokens");
            DropTable("dbo.Settings");
            DropTable("dbo.Roles");
            DropTable("dbo.UserRole");
            DropTable("dbo.Notifications");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.AuditLogs");
            DropTable("dbo.Users");
            DropTable("dbo.ActivityLogs");
        }
    }
}
