namespace Decision.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsSystemRole = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        IsBanned = c.Boolean(nullable: false),
                        Permissions = c.String(storeType: "xml"),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "IX_RoleName");
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AttachmentsSize = c.Long(nullable: false),
                        Space = c.Long(nullable: false),
                        IsBanned = c.Boolean(nullable: false),
                        IsSystemAccount = c.Boolean(nullable: false),
                        LastIp = c.String(maxLength: 20),
                        LastLoginDate = c.DateTime(),
                        IsChangedPermissions = c.Boolean(nullable: false),
                        DirectPermissions = c.String(storeType: "xml"),
                        IsApproved = c.Boolean(nullable: false),
                        LastPasswordChangedDate = c.DateTime(),
                        BannedDate = c.DateTime(),
                        BannedReason = c.String(),
                        LastActivityOn = c.DateTime(),
                        DisplayName = c.String(nullable: false, maxLength: 50),
                        IsDeleted = c.Boolean(nullable: false),
                        AdminComment = c.String(),
                        Avatar = c.String(),
                        BirthDay = c.DateTime(),
                        RegisterDate = c.DateTime(nullable: false),
                        CurrentPageUrl = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Email = c.String(maxLength: 256),
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
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true);
            
            CreateTable(
                "dbo.ActivityLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Comment = c.String(),
                        OperatedOn = c.DateTime(nullable: false),
                        Url = c.String(),
                        Title = c.String(),
                        Agent = c.String(),
                        OperantIp = c.String(),
                        Type = c.Int(nullable: false),
                        OperantId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.OperantId, cascadeDelete: true)
                .Index(t => t.OperantId);
            
            CreateTable(
                "dbo.Applicants",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FatherName = c.String(),
                        Gilder = c.String(),
                        Nationality = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        CellphoneNumber = c.String(),
                        NumberIndispensable = c.String(),
                        MilitaryStatus = c.Int(nullable: false),
                        ServedEndOn = c.DateTime(),
                        MembershipType = c.Int(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        NationalCode = c.String(nullable: false, maxLength: 50),
                        BirthCertificateNumber = c.String(nullable: false, maxLength: 20),
                        MarriageStatus = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        Photo = c.Binary(),
                        CopyOfNationalCard = c.Binary(),
                        CopyOfBirthCertificate = c.Binary(),
                        BirthPlaceCity = c.String(maxLength: 50),
                        BirthPlaceState = c.String(maxLength: 50),
                        TotalReputation = c.Double(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        CreatorIp = c.String(),
                        ModifierIp = c.String(),
                        ModifyLocked = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifierAgent = c.String(),
                        CreatorAgent = c.String(),
                        Version = c.Int(nullable: false),
                        Action = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        ModifiedById = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.ModifiedById)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.FirstName, name: "IX_ApplicantFirstName")
                .Index(t => t.LastName, name: "IX_ApplicantLastName")
                .Index(t => t.NationalCode, unique: true, name: "IX_ApplicantNationalCode")
                .Index(t => t.BirthCertificateNumber, name: "IX_ApplicantBirthCertificateNumber")
                .Index(t => t.BirthPlaceCity, name: "IX_ApplicantBirthPlaceCity")
                .Index(t => t.BirthPlaceState, name: "IX_ApplicantBirthPlaceState")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CellPhone = c.String(maxLength: 20),
                        Location = c.String(nullable: false),
                        PhoneNumber = c.String(maxLength: 20),
                        Type = c.Int(nullable: false),
                        City = c.String(nullable: false, maxLength: 50),
                        State = c.String(nullable: false, maxLength: 50),
                        PostalCode = c.String(),
                        ApplicantId = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        CreatorIp = c.String(),
                        ModifierIp = c.String(),
                        ModifyLocked = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifierAgent = c.String(),
                        CreatorAgent = c.String(),
                        Version = c.Int(nullable: false),
                        Action = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        ModifiedById = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.ModifiedById)
                .Index(t => t.ApplicantId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Score = c.Double(nullable: false),
                        Title = c.String(),
                        MagazineOrSeminarName = c.String(),
                        MagazineOrSeminarType = c.Int(nullable: false),
                        ResponsibilityType = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        ArticleType = c.String(),
                        Brief = c.String(),
                        PublicatedOn = c.DateTime(),
                        Attachment = c.Binary(),
                        ApplicantId = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        CreatorIp = c.String(),
                        ModifierIp = c.String(),
                        ModifyLocked = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifierAgent = c.String(),
                        CreatorAgent = c.String(),
                        Version = c.Int(nullable: false),
                        Action = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        ModifiedById = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.ModifiedById)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId, cascadeDelete: true)
                .Index(t => t.ApplicantId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.EducationalBackgrounds",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Score = c.Double(nullable: false),
                        AcademicDegree = c.Int(nullable: false),
                        ThesisTopic = c.String(nullable: false),
                        GraduationDate = c.DateTime(nullable: false),
                        EntryDate = c.DateTime(nullable: false),
                        Advisor = c.String(maxLength: 50),
                        Supervisor = c.String(maxLength: 50),
                        Description = c.String(),
                        GPA = c.Decimal(nullable: false, precision: 7, scale: 2),
                        ThesisScore = c.Decimal(nullable: false, precision: 7, scale: 2),
                        Country = c.String(),
                        University = c.String(),
                        Field = c.String(),
                        ApplicantId = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        CreatorIp = c.String(),
                        ModifierIp = c.String(),
                        ModifyLocked = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifierAgent = c.String(),
                        CreatorAgent = c.String(),
                        Version = c.Int(nullable: false),
                        Action = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        ModifiedById = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.ModifiedById)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId, cascadeDelete: true)
                .Index(t => t.ApplicantId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.EducationalExperiences",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Institution = c.String(),
                        BeginYear = c.DateTime(nullable: false),
                        EndYear = c.DateTime(),
                        Lessons = c.String(),
                        InstitutionAddress = c.String(),
                        InstitutionPhoneNumber = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Score = c.Double(nullable: false),
                        ApplicantId = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        CreatorIp = c.String(),
                        ModifierIp = c.String(),
                        ModifyLocked = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifierAgent = c.String(),
                        CreatorAgent = c.String(),
                        Version = c.Int(nullable: false),
                        Action = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        ModifiedById = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.ModifiedById)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId, cascadeDelete: true)
                .Index(t => t.ApplicantId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.EntireEvaluations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Content = c.String(nullable: false),
                        EvaluationDate = c.DateTime(nullable: false),
                        Foible = c.String(nullable: false),
                        StrongPoint = c.String(nullable: false),
                        ApplicantId = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        CreatorIp = c.String(),
                        ModifierIp = c.String(),
                        ModifyLocked = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifierAgent = c.String(),
                        CreatorAgent = c.String(),
                        Version = c.Int(nullable: false),
                        Action = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        ModifiedById = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                        AnswerOption_Id = c.Guid(),
                        Question_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.ModifiedById)
                .ForeignKey("dbo.AnswerOptions", t => t.AnswerOption_Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .Index(t => t.ApplicantId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById)
                .Index(t => t.AnswerOption_Id)
                .Index(t => t.Question_Id);
            
            CreateTable(
                "dbo.Interviews",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        InterviewDate = c.DateTime(nullable: false),
                        Body = c.String(nullable: false),
                        ApplicantId = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        CreatorIp = c.String(),
                        ModifierIp = c.String(),
                        ModifyLocked = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifierAgent = c.String(),
                        CreatorAgent = c.String(),
                        Version = c.Int(nullable: false),
                        Action = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        ModifiedById = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.ModifiedById)
                .Index(t => t.ApplicantId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.Presenters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FullName = c.String(),
                        RelationType = c.String(),
                        PhoneNumber = c.String(),
                        CellPhoneNumber = c.String(),
                        DurationOfRelation = c.String(),
                        Job = c.String(),
                        Type = c.Int(nullable: false),
                        ApplicantId = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        CreatorIp = c.String(),
                        ModifierIp = c.String(),
                        ModifyLocked = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifierAgent = c.String(),
                        CreatorAgent = c.String(),
                        Version = c.Int(nullable: false),
                        Action = c.Int(nullable: false),
                        RowVersion = c.Binary(),
                        ModifiedById = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.ModifiedById)
                .Index(t => t.ApplicantId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.ResearchExperiences",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Score = c.Double(nullable: false),
                        Institution = c.String(),
                        BeginYear = c.DateTime(nullable: false),
                        EndYear = c.DateTime(),
                        Lessons = c.String(),
                        InstitutionAddress = c.String(),
                        InstitutionPhoneNumber = c.String(),
                        ApplicantId = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        CreatorIp = c.String(),
                        ModifierIp = c.String(),
                        ModifyLocked = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifierAgent = c.String(),
                        CreatorAgent = c.String(),
                        Version = c.Int(nullable: false),
                        Action = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        ModifiedById = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.ModifiedById)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId, cascadeDelete: true)
                .Index(t => t.ApplicantId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.WorkExperiences",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Score = c.Double(nullable: false),
                        TenureBeginDate = c.DateTime(nullable: false),
                        TenureEndDate = c.DateTime(nullable: false),
                        OfficeName = c.String(nullable: false, maxLength: 1024),
                        City = c.String(),
                        State = c.String(),
                        OffieceAddress = c.String(),
                        OfficePhoneNumber = c.String(),
                        ResponsibilityType = c.String(),
                        OrganizationUnit = c.String(),
                        ApplicantId = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        CreatorIp = c.String(),
                        ModifierIp = c.String(),
                        ModifyLocked = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifierAgent = c.String(),
                        CreatorAgent = c.String(),
                        Version = c.Int(nullable: false),
                        Action = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        ModifiedById = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.ModifiedById)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId, cascadeDelete: true)
                .Index(t => t.ApplicantId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
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
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        Message = c.String(),
                        Url = c.String(),
                        ReceivedOn = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        OwnerId = c.Long(nullable: false),
                        Owner_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        Subject = c.String(nullable: false, maxLength: 1024),
                        SentOn = c.DateTime(nullable: false),
                        DeletedBySender = c.Boolean(nullable: false),
                        DeletedByReceiver = c.Boolean(nullable: false),
                        UnReadSenderMessagesCount = c.Int(nullable: false),
                        UnReadReceiverMessagesCount = c.Int(nullable: false),
                        MessagesCount = c.Int(nullable: false),
                        SenderId = c.Guid(nullable: false),
                        ReceiverId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ReceiverId)
                .ForeignKey("dbo.Users", t => t.SenderId)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        Body = c.String(nullable: false),
                        SentOn = c.DateTime(nullable: false),
                        ParentId = c.Guid(),
                        SenderId = c.Guid(nullable: false),
                        ConversationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Messages", t => t.ParentId)
                .ForeignKey("dbo.Users", t => t.SenderId, cascadeDelete: true)
                .ForeignKey("dbo.Conversations", t => t.ConversationId, cascadeDelete: true)
                .Index(t => t.ParentId)
                .Index(t => t.SenderId)
                .Index(t => t.ConversationId);
            
            CreateTable(
                "dbo.AnswerOptions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 1024),
                        Weight = c.Int(nullable: false),
                        Description = c.String(),
                        DisplayOrder = c.Int(nullable: false),
                        QuestionId = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        CreatorIp = c.String(),
                        ModifierIp = c.String(),
                        ModifyLocked = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifierAgent = c.String(),
                        CreatorAgent = c.String(),
                        Version = c.Int(nullable: false),
                        Action = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        ModifiedById = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.ModifiedById)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        IsMultiSelect = c.Boolean(nullable: false),
                        Weight = c.Int(nullable: false),
                        Description = c.String(),
                        DisplayOrder = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        CreatorIp = c.String(),
                        ModifierIp = c.String(),
                        ModifyLocked = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifierAgent = c.String(),
                        CreatorAgent = c.String(),
                        Version = c.Int(nullable: false),
                        Action = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        ModifiedById = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.ModifiedById)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FileName = c.String(),
                        ContentType = c.String(),
                        Size = c.Long(nullable: false),
                        Extension = c.String(),
                        Data = c.Binary(),
                        AttachedOn = c.DateTime(nullable: false),
                        DownloadsCount = c.Long(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        Agent = c.String(),
                        OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.AuditLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Action = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 1024),
                        OperatedOn = c.DateTime(nullable: false),
                        Entity = c.String(nullable: false, maxLength: 20),
                        XmlOldValue = c.String(storeType: "xml"),
                        XmlNewValue = c.String(storeType: "xml"),
                        EntityId = c.String(nullable: false, maxLength: 20),
                        Agent = c.String(),
                        OperantIp = c.String(),
                        OperantId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.OperantId)
                .Index(t => t.Entity, name: "IX_AuditTableName")
                .Index(t => t.EntityId, name: "IX_AuditEntityId")
                .Index(t => t.OperantId);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 50),
                        Value = c.String(),
                        RowVersion = c.Binary(),
                    })
                .PrimaryKey(t => t.Name);

            #region Db
            Sql("EXEC sp_configure filestream_access_level, 2");
            Sql("RECONFIGURE", true);

            Sql("alter database DecisionDb Add FileGroup FileGroupApplicant contains FileStream", true);
            Sql("alter database DecisionDb add file ( name = 'ApplicantDocuements'  ,  filename = 'C:\\FileStream\\ApplicantDocuements') to filegroup FileGroupApplicant", true);

            Sql("alter database DecisionDb Add FileGroup FileGroupArticle contains FileStream", true);
            Sql("alter database DecisionDb add file ( name = 'ArticleDocuements'  ,  filename = 'C:\\FileStream\\ArticleDocuements') to filegroup FileGroupArticle", true);

            Sql("alter database DecisionDb Add FileGroup FileGroupAttachment contains FileStream", true);
            Sql("alter database DecisionDb add file ( name = 'MessageAttachmentDocuements'  ,  filename = 'C:\\FileStream\\AttachmentDocuements') to filegroup FileGroupAttachment", true);

            Sql("ALTER DATABASE DecisionDb SET ALLOW_SNAPSHOT_ISOLATION ON", true);
            Sql("ALTER DATABASE DecisionDb SET READ_COMMITTED_SNAPSHOT ON", true);
            #endregion

            #region Applicant
            Sql("alter table [dbo].[Applicants] set(filestream_on ='FileGroupApplicant')");
            Sql("alter table [dbo].[Applicants] add [RowId] uniqueidentifier rowguidcol not null");
            Sql("alter table [dbo].[Applicants] add constraint [UQ_Applicants_RowId] UNIQUE NONCLUSTERED ([RowId])");
            Sql("alter table [dbo].[Applicants] add constraint [DF_Applicants_RowId] default (newid()) for [RowId]");

            DropColumn("dbo.Applicants", "Photo");
            Sql("alter table [dbo].[Applicants] add [PhotoTemp] varbinary(max) FILESTREAM not null");
            RenameColumn("dbo.Applicants", "PhotoTemp", "Photo");
            Sql("alter table [dbo].[Applicants] add constraint [DF_Applicants_Photo] default(0x) for [Photo]");

            DropColumn("dbo.Applicants", "CopyOfBirthCertificate");
            Sql("alter table [dbo].[Applicants] add [CopyOfBirthCertificateTemp] varbinary(max) FILESTREAM not null");
            RenameColumn("dbo.Applicants", "CopyOfBirthCertificateTemp", "CopyOfBirthCertificate");
            Sql("alter table [dbo].[Applicants] add constraint [DF_Applicants_CopyOfBirthCertificate] default(0x) for [CopyOfBirthCertificate]");

            DropColumn("dbo.Applicants", "CopyOfNationalCard");
            Sql("alter table [dbo].[Applicants] add [CopyOfNationalCardTemp] varbinary(max) FILESTREAM not null");
            RenameColumn("dbo.Applicants", "CopyOfNationalCardTemp", "CopyOfNationalCard");
            Sql("alter table [dbo].[Applicants] add constraint [DF_Applicants_CopyOfNationalCard] default(0x) for [CopyOfNationalCard]");

            #endregion

            #region Articles
            Sql("alter table [dbo].[Articles] set(filestream_on ='FileGroupArticle')");
            Sql("alter table [dbo].[Articles] add [RowId] uniqueidentifier rowguidcol not null");
            Sql("alter table [dbo].[Articles] add constraint [UQ_Articles_RowId] UNIQUE NONCLUSTERED ([RowId])");
            Sql("alter table [dbo].[Articles] add constraint [DF_Articles_RowId] default (newid()) for [RowId]");

            DropColumn("dbo.Articles", "Attachment");
            Sql("alter table [dbo].[Articles] add [AttachmentTemp] varbinary(max) FILESTREAM not null");
            RenameColumn("dbo.Articles", "AttachmentTemp", "Attachment");
            Sql("alter table [dbo].[Articles] add constraint [DF_Articles_Attachment] default(0x) for [Attachment]");
            #endregion

            #region Attachments
            Sql("alter table [dbo].[Attachments] set(filestream_on ='FileGroupAttachment')");
            Sql("alter table [dbo].[Attachments] add [RowId] uniqueidentifier rowguidcol not null");
            Sql("alter table [dbo].[Attachments] add constraint [UQ_Attachments_RowId] UNIQUE NONCLUSTERED ([RowId])");
            Sql("alter table [dbo].[Attachments] add constraint [DF_Attachments_RowId] default (newid()) for [RowId]");

            DropColumn("dbo.Attachments", "Data");
            Sql("alter table [dbo].[Attachments] add [DataTemp] varbinary(max) FILESTREAM not null");
            RenameColumn("dbo.Attachments", "DataTemp", "Data");
            Sql("alter table [dbo].[Attachments] add constraint [DF_Attachments_Data] default(0x) for [Data]");
            #endregion
        }

        public override void Down()
        {
            DropForeignKey("dbo.AuditLogs", "OperantId", "dbo.Users");
            DropForeignKey("dbo.Attachments", "OwnerId", "dbo.Users");
            DropForeignKey("dbo.Questions", "ModifiedById", "dbo.Users");
            DropForeignKey("dbo.EntireEvaluations", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Questions", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.AnswerOptions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.AnswerOptions", "ModifiedById", "dbo.Users");
            DropForeignKey("dbo.EntireEvaluations", "AnswerOption_Id", "dbo.AnswerOptions");
            DropForeignKey("dbo.AnswerOptions", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.Users");
            DropForeignKey("dbo.Conversations", "SenderId", "dbo.Users");
            DropForeignKey("dbo.Conversations", "ReceiverId", "dbo.Users");
            DropForeignKey("dbo.Messages", "ConversationId", "dbo.Conversations");
            DropForeignKey("dbo.Messages", "SenderId", "dbo.Users");
            DropForeignKey("dbo.Messages", "ParentId", "dbo.Messages");
            DropForeignKey("dbo.Notifications", "Owner_Id", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.Applicants", "User_Id", "dbo.Users");
            DropForeignKey("dbo.WorkExperiences", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.WorkExperiences", "ModifiedById", "dbo.Users");
            DropForeignKey("dbo.WorkExperiences", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ResearchExperiences", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.ResearchExperiences", "ModifiedById", "dbo.Users");
            DropForeignKey("dbo.ResearchExperiences", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Presenters", "ModifiedById", "dbo.Users");
            DropForeignKey("dbo.Presenters", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Presenters", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.Applicants", "ModifiedById", "dbo.Users");
            DropForeignKey("dbo.Interviews", "ModifiedById", "dbo.Users");
            DropForeignKey("dbo.Interviews", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Interviews", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.EntireEvaluations", "ModifiedById", "dbo.Users");
            DropForeignKey("dbo.EntireEvaluations", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.EntireEvaluations", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.EducationalExperiences", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.EducationalExperiences", "ModifiedById", "dbo.Users");
            DropForeignKey("dbo.EducationalExperiences", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.EducationalBackgrounds", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.EducationalBackgrounds", "ModifiedById", "dbo.Users");
            DropForeignKey("dbo.EducationalBackgrounds", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Applicants", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Articles", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.Articles", "ModifiedById", "dbo.Users");
            DropForeignKey("dbo.Articles", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Addresses", "ModifiedById", "dbo.Users");
            DropForeignKey("dbo.Addresses", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Addresses", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.ActivityLogs", "OperantId", "dbo.Users");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Roles");
            DropIndex("dbo.AuditLogs", new[] { "OperantId" });
            DropIndex("dbo.AuditLogs", "IX_AuditEntityId");
            DropIndex("dbo.AuditLogs", "IX_AuditTableName");
            DropIndex("dbo.Attachments", new[] { "OwnerId" });
            DropIndex("dbo.Questions", new[] { "CreatedById" });
            DropIndex("dbo.Questions", new[] { "ModifiedById" });
            DropIndex("dbo.AnswerOptions", new[] { "CreatedById" });
            DropIndex("dbo.AnswerOptions", new[] { "ModifiedById" });
            DropIndex("dbo.AnswerOptions", new[] { "QuestionId" });
            DropIndex("dbo.Messages", new[] { "ConversationId" });
            DropIndex("dbo.Messages", new[] { "SenderId" });
            DropIndex("dbo.Messages", new[] { "ParentId" });
            DropIndex("dbo.Conversations", new[] { "ReceiverId" });
            DropIndex("dbo.Conversations", new[] { "SenderId" });
            DropIndex("dbo.Notifications", new[] { "Owner_Id" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.WorkExperiences", new[] { "CreatedById" });
            DropIndex("dbo.WorkExperiences", new[] { "ModifiedById" });
            DropIndex("dbo.WorkExperiences", new[] { "ApplicantId" });
            DropIndex("dbo.ResearchExperiences", new[] { "CreatedById" });
            DropIndex("dbo.ResearchExperiences", new[] { "ModifiedById" });
            DropIndex("dbo.ResearchExperiences", new[] { "ApplicantId" });
            DropIndex("dbo.Presenters", new[] { "CreatedById" });
            DropIndex("dbo.Presenters", new[] { "ModifiedById" });
            DropIndex("dbo.Presenters", new[] { "ApplicantId" });
            DropIndex("dbo.Interviews", new[] { "CreatedById" });
            DropIndex("dbo.Interviews", new[] { "ModifiedById" });
            DropIndex("dbo.Interviews", new[] { "ApplicantId" });
            DropIndex("dbo.EntireEvaluations", new[] { "Question_Id" });
            DropIndex("dbo.EntireEvaluations", new[] { "AnswerOption_Id" });
            DropIndex("dbo.EntireEvaluations", new[] { "CreatedById" });
            DropIndex("dbo.EntireEvaluations", new[] { "ModifiedById" });
            DropIndex("dbo.EntireEvaluations", new[] { "ApplicantId" });
            DropIndex("dbo.EducationalExperiences", new[] { "CreatedById" });
            DropIndex("dbo.EducationalExperiences", new[] { "ModifiedById" });
            DropIndex("dbo.EducationalExperiences", new[] { "ApplicantId" });
            DropIndex("dbo.EducationalBackgrounds", new[] { "CreatedById" });
            DropIndex("dbo.EducationalBackgrounds", new[] { "ModifiedById" });
            DropIndex("dbo.EducationalBackgrounds", new[] { "ApplicantId" });
            DropIndex("dbo.Articles", new[] { "CreatedById" });
            DropIndex("dbo.Articles", new[] { "ModifiedById" });
            DropIndex("dbo.Articles", new[] { "ApplicantId" });
            DropIndex("dbo.Addresses", new[] { "CreatedById" });
            DropIndex("dbo.Addresses", new[] { "ModifiedById" });
            DropIndex("dbo.Addresses", new[] { "ApplicantId" });
            DropIndex("dbo.Applicants", new[] { "User_Id" });
            DropIndex("dbo.Applicants", new[] { "CreatedById" });
            DropIndex("dbo.Applicants", new[] { "ModifiedById" });
            DropIndex("dbo.Applicants", "IX_ApplicantBirthPlaceState");
            DropIndex("dbo.Applicants", "IX_ApplicantBirthPlaceCity");
            DropIndex("dbo.Applicants", "IX_ApplicantBirthCertificateNumber");
            DropIndex("dbo.Applicants", "IX_ApplicantNationalCode");
            DropIndex("dbo.Applicants", "IX_ApplicantLastName");
            DropIndex("dbo.Applicants", "IX_ApplicantFirstName");
            DropIndex("dbo.ActivityLogs", new[] { "OperantId" });
            DropIndex("dbo.Users", new[] { "UserName" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.Roles", "IX_RoleName");
            DropTable("dbo.Settings");
            DropTable("dbo.AuditLogs");
            DropTable("dbo.Attachments");
            DropTable("dbo.Questions");
            DropTable("dbo.AnswerOptions");
            DropTable("dbo.Messages");
            DropTable("dbo.Conversations");
            DropTable("dbo.Notifications");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.WorkExperiences");
            DropTable("dbo.ResearchExperiences");
            DropTable("dbo.Presenters");
            DropTable("dbo.Interviews");
            DropTable("dbo.EntireEvaluations");
            DropTable("dbo.EducationalExperiences");
            DropTable("dbo.EducationalBackgrounds");
            DropTable("dbo.Articles");
            DropTable("dbo.Addresses");
            DropTable("dbo.Applicants");
            DropTable("dbo.ActivityLogs");
            DropTable("dbo.Users");
            DropTable("dbo.UserRole");
            DropTable("dbo.Roles");
        }
    }
}
