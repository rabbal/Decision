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
                        IsBanned = c.Boolean(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        IsSystemAccount = c.Boolean(nullable: false),
                        LastIp = c.String(maxLength: 20),
                        LastLoginDate = c.DateTime(),
                        IsChangedPermissions = c.Boolean(nullable: false),
                        DirectPermissions = c.String(storeType: "xml"),
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
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(nullable: false),
                        NationalCode = c.String(nullable: false, maxLength: 50),
                        BirthCertificateNumber = c.String(nullable: false, maxLength: 20),
                        CollegiateOrder = c.Int(nullable: false),
                        OccupationalGroup = c.Int(nullable: false),
                        MarriageStatus = c.Int(nullable: false),
                        BankName = c.String(nullable: false, maxLength: 256),
                        BankBranch = c.String(maxLength: 256),
                        AccountNumber = c.String(maxLength: 256),
                        IsClothed = c.Boolean(nullable: false),
                        Gender = c.Int(nullable: false),
                        TrainingGPA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TrainigGrade = c.Int(nullable: false),
                        Photo = c.Binary(),
                        CopyOfNationalCard = c.Binary(),
                        CopyOfBirthCertificate = c.Binary(),
                        OfficialYears = c.Int(nullable: false),
                        CollegiateYears = c.Int(nullable: false),
                        IsInReference = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        BirthPlaceCity = c.String(maxLength: 50),
                        BirthPlaceState = c.String(maxLength: 50),
                        PersonnelCode = c.String(nullable: false, maxLength: 50),
                        PositionId = c.Guid(),
                        TrainingCourseId = c.Guid(),
                        ApproveById = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateDate = c.DateTime(nullable: false),
                        SoftDeleteDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatorId = c.Guid(nullable: false),
                        LasModifierId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ApproveById)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .ForeignKey("dbo.Users", t => t.LasModifierId)
                .ForeignKey("dbo.Titles", t => t.PositionId)
                .ForeignKey("dbo.TrainingCourses", t => t.TrainingCourseId)
                .Index(t => t.FirstName, name: "IX_TeacherFirstName")
                .Index(t => t.LastName, name: "IX_TeacherLastName")
                .Index(t => t.NationalCode, unique: true, name: "IX_TeacherNationalCode")
                .Index(t => t.BirthCertificateNumber, unique: true, name: "IX_TeacherBirthCertificateNumber")
                .Index(t => t.BirthPlaceCity, name: "IX_TeacherBirthPlaceCity")
                .Index(t => t.BirthPlaceState, name: "IX_TeacherBirthPlaceState")
                .Index(t => t.PositionId)
                .Index(t => t.TrainingCourseId)
                .Index(t => t.ApproveById)
                .Index(t => t.CreatorId)
                .Index(t => t.LasModifierId);
            
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
                        TeacherId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateDate = c.DateTime(nullable: false),
                        SoftDeleteDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatorId = c.Guid(nullable: false),
                        LasModifierId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.LasModifierId)
                .Index(t => t.TeacherId)
                .Index(t => t.CreatorId)
                .Index(t => t.LasModifierId);
            
            CreateTable(
                "dbo.EducationalBackgrounds",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EducationalType = c.Int(nullable: false),
                        HosDegree = c.Int(nullable: false),
                        AcademicDegree = c.Int(nullable: false),
                        ThesisTopic = c.String(nullable: false),
                        GraduationDate = c.DateTime(nullable: false),
                        EntryDate = c.DateTime(nullable: false),
                        Advisor = c.String(nullable: false, maxLength: 256),
                        Supervisor = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                        GPA = c.Decimal(nullable: false, precision: 7, scale: 2),
                        ThesisScore = c.Decimal(nullable: false, precision: 7, scale: 2),
                        RelatedToOrganizationPosition = c.Int(nullable: false),
                        Attachment = c.Binary(),
                        TeacherId = c.Guid(nullable: false),
                        InstitutionId = c.Guid(nullable: false),
                        StudyFieldId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateDate = c.DateTime(nullable: false),
                        SoftDeleteDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatorId = c.Guid(nullable: false),
                        LasModifierId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId)
                .ForeignKey("dbo.Users", t => t.LasModifierId)
                .ForeignKey("dbo.Titles", t => t.StudyFieldId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId)
                .Index(t => t.InstitutionId)
                .Index(t => t.StudyFieldId)
                .Index(t => t.CreatorId)
                .Index(t => t.LasModifierId);
            
            CreateTable(
                "dbo.Institutions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(nullable: false, maxLength: 1024),
                        IsDeleted = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateDate = c.DateTime(nullable: false),
                        SoftDeleteDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatorId = c.Guid(nullable: false),
                        LasModifierId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .ForeignKey("dbo.Users", t => t.LasModifierId)
                .Index(t => t.CreatorId)
                .Index(t => t.LasModifierId);
            
            CreateTable(
                "dbo.Titles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        Type = c.Int(nullable: false),
                        Category = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateDate = c.DateTime(nullable: false),
                        SoftDeleteDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatorId = c.Guid(nullable: false),
                        LasModifierId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .ForeignKey("dbo.Users", t => t.LasModifierId)
                .Index(t => new { t.Name, t.Type, t.Category }, unique: true, name: "IX_UniqueTitleName")
                .Index(t => t.CreatorId)
                .Index(t => t.LasModifierId);
            
            CreateTable(
                "dbo.Appraisers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 256),
                        LastName = c.String(nullable: false, maxLength: 256),
                        CellPhone = c.String(nullable: false, maxLength: 20),
                        Gender = c.Int(nullable: false),
                        AppraiserTitleId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateDate = c.DateTime(nullable: false),
                        SoftDeleteDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatorId = c.Guid(nullable: false),
                        LasModifierId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Titles", t => t.AppraiserTitleId)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .ForeignKey("dbo.Users", t => t.LasModifierId)
                .Index(t => t.AppraiserTitleId)
                .Index(t => t.CreatorId)
                .Index(t => t.LasModifierId);
            
            CreateTable(
                "dbo.EntireEvaluations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Content = c.String(nullable: false),
                        EvaluationDate = c.DateTime(nullable: false),
                        Brief = c.String(nullable: false),
                        Foible = c.String(nullable: false),
                        StrongPoint = c.String(nullable: false),
                        Attachment = c.Binary(),
                        TeacherId = c.Guid(nullable: false),
                        EvaluatorId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateDate = c.DateTime(nullable: false),
                        SoftDeleteDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatorId = c.Guid(nullable: false),
                        LasModifierId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.LasModifierId)
                .ForeignKey("dbo.Appraisers", t => t.EvaluatorId)
                .Index(t => t.TeacherId)
                .Index(t => t.EvaluatorId)
                .Index(t => t.CreatorId)
                .Index(t => t.LasModifierId);
            
            CreateTable(
                "dbo.Interviews",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        InterviewDate = c.DateTime(nullable: false),
                        Body = c.String(nullable: false),
                        Brief = c.String(nullable: false),
                        Attachment = c.Binary(),
                        TeacherId = c.Guid(nullable: false),
                        InterviewerId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateDate = c.DateTime(nullable: false),
                        SoftDeleteDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatorId = c.Guid(nullable: false),
                        LasModifierId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.LasModifierId)
                .ForeignKey("dbo.Appraisers", t => t.InterviewerId)
                .Index(t => t.TeacherId)
                .Index(t => t.InterviewerId)
                .Index(t => t.CreatorId)
                .Index(t => t.LasModifierId);
            
            CreateTable(
                "dbo.ArticleEvaluations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Content = c.String(),
                        EvaluationDate = c.DateTime(nullable: false),
                        Brief = c.String(),
                        Foible = c.String(),
                        StrongPoint = c.String(),
                        ArticleId = c.Guid(nullable: false),
                        EvaluatorId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateDate = c.DateTime(nullable: false),
                        SoftDeleteDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatorId = c.Guid(nullable: false),
                        LasModifierId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.LasModifierId)
                .ForeignKey("dbo.Appraisers", t => t.EvaluatorId)
                .Index(t => t.ArticleId)
                .Index(t => t.EvaluatorId)
                .Index(t => t.CreatorId)
                .Index(t => t.LasModifierId);
            
            CreateTable(
                "dbo.AnswerOptions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1024),
                        Weight = c.Byte(nullable: false),
                        QuestionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Type = c.Int(nullable: false),
                        Weight = c.Byte(nullable: false),
                        DefaultValue = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateDate = c.DateTime(nullable: false),
                        SoftDeleteDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatorId = c.Guid(nullable: false),
                        LasModifierId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .ForeignKey("dbo.Users", t => t.LasModifierId)
                .Index(t => t.CreatorId)
                .Index(t => t.LasModifierId);
            
            CreateTable(
                "dbo.ArticleEvaluationQuestions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Value = c.String(nullable: false),
                        ArticleEvaluationId = c.Guid(nullable: false),
                        QuestionId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateDate = c.DateTime(nullable: false),
                        SoftDeleteDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatorId = c.Guid(nullable: false),
                        LasModifierId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .ForeignKey("dbo.ArticleEvaluations", t => t.ArticleEvaluationId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.LasModifierId)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => new { t.QuestionId, t.ArticleEvaluationId }, unique: true, name: "IX_ ArticleEvaluationQuestion")
                .Index(t => t.CreatorId)
                .Index(t => t.LasModifierId);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Content = c.String(nullable: false),
                        Brief = c.String(),
                        ArticleDate = c.DateTime(nullable: false),
                        Attachment = c.Binary(),
                        TeacherId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateDate = c.DateTime(nullable: false),
                        SoftDeleteDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatorId = c.Guid(nullable: false),
                        LasModifierId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .ForeignKey("dbo.Users", t => t.LasModifierId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId)
                .Index(t => t.CreatorId)
                .Index(t => t.LasModifierId);
            
            CreateTable(
                "dbo.EducationalExperiences",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        BeginYear = c.Int(nullable: false),
                        EndYear = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        TeacherId = c.Guid(nullable: false),
                        TitleId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateDate = c.DateTime(nullable: false),
                        SoftDeleteDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatorId = c.Guid(nullable: false),
                        LasModifierId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .ForeignKey("dbo.Users", t => t.LasModifierId)
                .ForeignKey("dbo.Titles", t => t.TitleId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId)
                .Index(t => t.TitleId)
                .Index(t => t.CreatorId)
                .Index(t => t.LasModifierId);
            
            CreateTable(
                "dbo.TeacherInServiceCourseTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HoursCount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TeacherId = c.Guid(nullable: false),
                        InServiceCourseTypeTitleId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateDate = c.DateTime(nullable: false),
                        SoftDeleteDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatorId = c.Guid(nullable: false),
                        LasModifierId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .ForeignKey("dbo.Titles", t => t.InServiceCourseTypeTitleId)
                .ForeignKey("dbo.Users", t => t.LasModifierId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId)
                .Index(t => t.InServiceCourseTypeTitleId)
                .Index(t => t.CreatorId)
                .Index(t => t.LasModifierId);
            
            CreateTable(
                "dbo.WorkExperiences",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TenureBeginDate = c.DateTime(nullable: false),
                        TenureEndDate = c.DateTime(nullable: false),
                        ReferentialProjectCount = c.Int(nullable: false),
                        ClosedProjectCount = c.Int(nullable: false),
                        OpenProjectCount = c.Int(nullable: false),
                        CooperationType = c.Int(nullable: false),
                        OfficeName = c.String(nullable: false, maxLength: 1024),
                        City = c.String(),
                        State = c.String(),
                        TeacherId = c.Guid(nullable: false),
                        TitleId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateDate = c.DateTime(nullable: false),
                        SoftDeleteDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatorId = c.Guid(nullable: false),
                        LasModifierId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .ForeignKey("dbo.Users", t => t.LasModifierId)
                .ForeignKey("dbo.Titles", t => t.TitleId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId)
                .Index(t => t.TitleId)
                .Index(t => t.CreatorId)
                .Index(t => t.LasModifierId);
            
            CreateTable(
                "dbo.ReferentialTeachers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        FinishedDate = c.DateTime(),
                        ReferencedFromId = c.Guid(nullable: false),
                        ReferencedToId = c.Guid(nullable: false),
                        TeacherId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.ReferencedFromId)
                .ForeignKey("dbo.Users", t => t.ReferencedToId)
                .Index(t => t.ReferencedFromId)
                .Index(t => t.ReferencedToId)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.ResearchExperiences",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        ResearchType = c.Int(nullable: false),
                        PublishedIn = c.String(),
                        PublishDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        TeacherId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateDate = c.DateTime(nullable: false),
                        SoftDeleteDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatorId = c.Guid(nullable: false),
                        LasModifierId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .ForeignKey("dbo.Users", t => t.LasModifierId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId)
                .Index(t => t.CreatorId)
                .Index(t => t.LasModifierId);
            
            CreateTable(
                "dbo.TrainingCourses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CourseCode = c.String(nullable: false, maxLength: 256),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        TheoryTotalHoures = c.Int(nullable: false),
                        PracticalTotalHoures = c.Int(nullable: false),
                        TrainingCenterId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateDate = c.DateTime(nullable: false),
                        SoftDeleteDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatorId = c.Guid(nullable: false),
                        LasModifierId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .ForeignKey("dbo.Users", t => t.LasModifierId)
                .ForeignKey("dbo.TrainingCenters", t => t.TrainingCenterId, cascadeDelete: true)
                .Index(t => new { t.CourseCode, t.TrainingCenterId }, unique: true, name: "IX_UniqueCourseCode")
                .Index(t => t.CreatorId)
                .Index(t => t.LasModifierId);
            
            CreateTable(
                "dbo.TrainingCenters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CenterName = c.String(nullable: false, maxLength: 256),
                        PhoneNumber1 = c.String(maxLength: 20),
                        PhoneNumber2 = c.String(maxLength: 20),
                        Location = c.String(),
                        Description = c.String(maxLength: 1024),
                        City = c.String(nullable: false, maxLength: 50),
                        State = c.String(nullable: false, maxLength: 50),
                        IsDeleted = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateDate = c.DateTime(nullable: false),
                        SoftDeleteDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatorId = c.Guid(nullable: false),
                        LasModifierId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .ForeignKey("dbo.Users", t => t.LasModifierId)
                .Index(t => new { t.CenterName, t.City }, unique: true, name: "IX_UniqueCenterName")
                .Index(t => t.State, name: "IX_TrainingCenterState")
                .Index(t => t.CreatorId)
                .Index(t => t.LasModifierId);
            
            CreateTable(
                "dbo.AuditLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.Int(),
                        TableName = c.String(maxLength: 20),
                        RecordedEntityId = c.Guid(),
                        Description = c.String(nullable: false, maxLength: 1024),
                        NewValue = c.String(storeType: "xml"),
                        OldValue = c.String(storeType: "xml"),
                        OperateDate = c.DateTime(nullable: false),
                        CreatorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .Index(t => t.Type, name: "IX_AuditType")
                .Index(t => t.TableName, name: "IX_AuditTableName")
                .Index(t => t.RecordedEntityId, name: "IX_AuditEntityId")
                .Index(t => t.CreatorId);
            
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
                "dbo.Conversations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsSeen = c.Boolean(nullable: false),
                        Subject = c.String(nullable: false, maxLength: 1024),
                        StartDate = c.DateTime(nullable: false),
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
                        Content = c.String(nullable: false),
                        SendDate = c.DateTime(nullable: false),
                        ReplyId = c.Guid(),
                        SenderId = c.Guid(nullable: false),
                        ConversationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Messages", t => t.ReplyId)
                .ForeignKey("dbo.Users", t => t.SenderId, cascadeDelete: true)
                .ForeignKey("dbo.Conversations", t => t.ConversationId, cascadeDelete: true)
                .Index(t => t.ReplyId)
                .Index(t => t.SenderId)
                .Index(t => t.ConversationId);
            
            CreateTable(
                "dbo.MessageAttachments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FriendlyName = c.String(nullable: false, maxLength: 256),
                        Data = c.Binary(),
                        ContentType = c.String(),
                        Extension = c.String(),
                        MessageId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Messages", t => t.MessageId, cascadeDelete: true)
                .Index(t => t.MessageId);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 50),
                        Value = c.String(storeType: "xml"),
                        RowVersion = c.Binary(),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.AnswerOptionTeachermentEvaluation",
                c => new
                    {
                        AnswerOptionId = c.Guid(nullable: false),
                        ArticleEvaluationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.AnswerOptionId, t.ArticleEvaluationId })
                .ForeignKey("dbo.AnswerOptions", t => t.AnswerOptionId, cascadeDelete: true)
                .ForeignKey("dbo.ArticleEvaluations", t => t.ArticleEvaluationId, cascadeDelete: true)
                .Index(t => t.AnswerOptionId)
                .Index(t => t.ArticleEvaluationId);
            
            CreateTable(
                "dbo.QuestionArticleEvaluation",
                c => new
                    {
                        QuestionId = c.Guid(nullable: false),
                        ArticleEvaluationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionId, t.ArticleEvaluationId })
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.ArticleEvaluations", t => t.ArticleEvaluationId, cascadeDelete: true)
                .Index(t => t.QuestionId)
                .Index(t => t.ArticleEvaluationId);


            #region Db
            Sql("EXEC sp_configure filestream_access_level, 2");
            Sql("RECONFIGURE", true);

            Sql("alter database DecisionDB Add FileGroup FileGroupTeacher contains FileStream", true);
            Sql("alter database DecisionDB add file ( name = 'TeacherDocuements'  ,  filename = 'C:\\FileStream\\TeacherDocuements') to filegroup FileGroupTeacher", true);

            Sql("alter database DecisionDB Add FileGroup FileGroupArticle contains FileStream", true);
            Sql("alter database DecisionDB add file ( name = 'ArticleDocuements'  ,  filename = 'C:\\FileStream\\ArticleDocuements') to filegroup FileGroupArticle", true);

            Sql("alter database DecisionDB Add FileGroup FileGroupEducationalBackground contains FileStream", true);
            Sql("alter database DecisionDB add file ( name = 'EducationalBackgroundDocuements'  ,  filename = 'C:\\FileStream\\EducationalBackgroundDocuements') to filegroup FileGroupEducationalBackground", true);

            Sql("alter database DecisionDB Add FileGroup FileGroupInterview contains FileStream", true);
            Sql("alter database DecisionDB add file ( name = 'InterviewDocuements'  ,  filename = 'C:\\FileStream\\InterviewDocuements') to filegroup FileGroupInterview", true);

            Sql("alter database DecisionDB Add FileGroup FileGroupEntireEvaluation contains FileStream", true);
            Sql("alter database DecisionDB add file ( name = 'EntireEvaluationDocuements'  ,  filename = 'C:\\FileStream\\EntireEvaluationDocuements') to filegroup FileGroupEntireEvaluation", true);


            Sql("alter database DecisionDB Add FileGroup FileGroupMessageAttachment contains FileStream", true);
            Sql("alter database DecisionDB add file ( name = 'MessageAttachmentDocuements'  ,  filename = 'C:\\FileStream\\MessageAttachmentDocuements') to filegroup FileGroupMessageAttachment", true);

            Sql("ALTER DATABASE DecisionDB SET ALLOW_SNAPSHOT_ISOLATION ON", true);
            Sql("ALTER DATABASE DecisionDB SET READ_COMMITTED_SNAPSHOT ON", true);
            #endregion

            #region Teacher
            Sql("alter table [dbo].[Teachers] set(filestream_on ='FileGroupTeacher')");
            Sql("alter table [dbo].[Teachers] add [RowId] uniqueidentifier rowguidcol not null");
            Sql("alter table [dbo].[Teachers] add constraint [UQ_Teachers_RowId] UNIQUE NONCLUSTERED ([RowId])");
            Sql("alter table [dbo].[Teachers] add constraint [DF_Teachers_RowId] default (newid()) for [RowId]");

            DropColumn("dbo.Teachers", "Photo");
            Sql("alter table [dbo].[Teachers] add [PhotoTemp] varbinary(max) FILESTREAM not null");
            RenameColumn("dbo.Teachers", "PhotoTemp", "Photo");
            Sql("alter table [dbo].[Teachers] add constraint [DF_Teachers_Photo] default(0x) for [Photo]");

            DropColumn("dbo.Teachers", "CopyOfBirthCertificate");
            Sql("alter table [dbo].[Teachers] add [CopyOfBirthCertificateTemp] varbinary(max) FILESTREAM not null");
            RenameColumn("dbo.Teachers", "CopyOfBirthCertificateTemp", "CopyOfBirthCertificate");
            Sql("alter table [dbo].[Teachers] add constraint [DF_Teachers_CopyOfBirthCertificate] default(0x) for [CopyOfBirthCertificate]");

            DropColumn("dbo.Teachers", "CopyOfNationalCard");
            Sql("alter table [dbo].[Teachers] add [CopyOfNationalCardTemp] varbinary(max) FILESTREAM not null");
            RenameColumn("dbo.Teachers", "CopyOfNationalCardTemp", "CopyOfNationalCard");
            Sql("alter table [dbo].[Teachers] add constraint [DF_Teachers_CopyOfNationalCard] default(0x) for [CopyOfNationalCard]");

            #endregion

            #region EducationalBackgrounds
            Sql("alter table [dbo].[EducationalBackgrounds] set(filestream_on ='FileGroupEducationalBackground')");
            Sql("alter table [dbo].[EducationalBackgrounds] add [RowId] uniqueidentifier rowguidcol not null");
            Sql("alter table [dbo].[EducationalBackgrounds] add constraint [UQ_EducationalBackgrounds_RowId] UNIQUE NONCLUSTERED ([RowId])");
            Sql("alter table [dbo].[EducationalBackgrounds] add constraint [DF_EducationalBackgrounds_RowId] default (newid()) for [RowId]");

            DropColumn("dbo.EducationalBackgrounds", "Attachment");
            Sql("alter table [dbo].[EducationalBackgrounds] add [AttachmentTemp] varbinary(max) FILESTREAM not null");
            RenameColumn("dbo.EducationalBackgrounds", "AttachmentTemp", "Attachment");
            Sql("alter table [dbo].[EducationalBackgrounds] add constraint [DF_EducationalBackgrounds_Attachment] default(0x) for [Attachment]");
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

            #region Interviews
            Sql("alter table [dbo].[Interviews] set(filestream_on ='FileGroupInterview')");
            Sql("alter table [dbo].[Interviews] add [RowId] uniqueidentifier rowguidcol not null");
            Sql("alter table [dbo].[Interviews] add constraint [UQ_Interviews_RowId] UNIQUE NONCLUSTERED ([RowId])");
            Sql("alter table [dbo].[Interviews] add constraint [DF_Interviews_RowId] default (newid()) for [RowId]");

            DropColumn("dbo.Interviews", "Attachment");
            Sql("alter table [dbo].[Interviews] add [AttachmentTemp] varbinary(max) FILESTREAM not null");
            RenameColumn("dbo.Interviews", "AttachmentTemp", "Attachment");
            Sql("alter table [dbo].[Interviews] add constraint [DF_Interviews_Attachment] default(0x) for [Attachment]");
            #endregion

            #region EntireEvaluations
            Sql("alter table [dbo].[EntireEvaluations] set(filestream_on ='FileGroupEntireEvaluation')");
            Sql("alter table [dbo].[EntireEvaluations] add [RowId] uniqueidentifier rowguidcol not null");
            Sql("alter table [dbo].[EntireEvaluations] add constraint [UQ_EntireEvaluations_RowId] UNIQUE NONCLUSTERED ([RowId])");
            Sql("alter table [dbo].[EntireEvaluations] add constraint [DF_EntireEvaluations_RowId] default (newid()) for [RowId]");

            DropColumn("dbo.EntireEvaluations", "Attachment");
            Sql("alter table [dbo].[EntireEvaluations] add [AttachmentTemp] varbinary(max) FILESTREAM not null");
            RenameColumn("dbo.EntireEvaluations", "AttachmentTemp", "Attachment");
            Sql("alter table [dbo].[EntireEvaluations] add constraint [DF_EntireEvaluations_Attachment] default(0x) for [Attachment]");
            #endregion

            #region MessageAttachments
            Sql("alter table [dbo].[MessageAttachments] set(filestream_on ='FileGroupMessageAttachment')");
            Sql("alter table [dbo].[MessageAttachments] add [RowId] uniqueidentifier rowguidcol not null");
            Sql("alter table [dbo].[MessageAttachments] add constraint [UQ_MessageAttachments_RowId] UNIQUE NONCLUSTERED ([RowId])");
            Sql("alter table [dbo].[MessageAttachments] add constraint [DF_MessageAttachments_RowId] default (newid()) for [RowId]");

            DropColumn("dbo.MessageAttachments", "Data");
            Sql("alter table [dbo].[MessageAttachments] add [DataTemp] varbinary(max) FILESTREAM not null");
            RenameColumn("dbo.MessageAttachments", "DataTemp", "Data");
            Sql("alter table [dbo].[MessageAttachments] add constraint [DF_MessageAttachments_Data] default(0x) for [Data]");
            #endregion

        }

        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "UserId", "dbo.Users");
            DropForeignKey("dbo.Conversations", "SenderId", "dbo.Users");
            DropForeignKey("dbo.Conversations", "ReceiverId", "dbo.Users");
            DropForeignKey("dbo.Messages", "ConversationId", "dbo.Conversations");
            DropForeignKey("dbo.Messages", "SenderId", "dbo.Users");
            DropForeignKey("dbo.Messages", "ReplyId", "dbo.Messages");
            DropForeignKey("dbo.MessageAttachments", "MessageId", "dbo.Messages");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.AuditLogs", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.WorkExperiences", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "TrainingCourseId", "dbo.TrainingCourses");
            DropForeignKey("dbo.TrainingCenters", "LasModifierId", "dbo.Users");
            DropForeignKey("dbo.TrainingCenters", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.TrainingCourses", "TrainingCenterId", "dbo.TrainingCenters");
            DropForeignKey("dbo.TrainingCourses", "LasModifierId", "dbo.Users");
            DropForeignKey("dbo.TrainingCourses", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.ResearchExperiences", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.ResearchExperiences", "LasModifierId", "dbo.Users");
            DropForeignKey("dbo.ResearchExperiences", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.ReferentialTeachers", "ReferencedToId", "dbo.Users");
            DropForeignKey("dbo.ReferentialTeachers", "ReferencedFromId", "dbo.Users");
            DropForeignKey("dbo.ReferentialTeachers", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "PositionId", "dbo.Titles");
            DropForeignKey("dbo.Teachers", "LasModifierId", "dbo.Users");
            DropForeignKey("dbo.Articles", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.TeacherInServiceCourseTypes", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.EducationalExperiences", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.EducationalBackgrounds", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.EducationalBackgrounds", "StudyFieldId", "dbo.Titles");
            DropForeignKey("dbo.WorkExperiences", "TitleId", "dbo.Titles");
            DropForeignKey("dbo.WorkExperiences", "LasModifierId", "dbo.Users");
            DropForeignKey("dbo.WorkExperiences", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.Titles", "LasModifierId", "dbo.Users");
            DropForeignKey("dbo.TeacherInServiceCourseTypes", "LasModifierId", "dbo.Users");
            DropForeignKey("dbo.TeacherInServiceCourseTypes", "InServiceCourseTypeTitleId", "dbo.Titles");
            DropForeignKey("dbo.TeacherInServiceCourseTypes", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.EducationalExperiences", "TitleId", "dbo.Titles");
            DropForeignKey("dbo.EducationalExperiences", "LasModifierId", "dbo.Users");
            DropForeignKey("dbo.EducationalExperiences", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.Titles", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.Appraisers", "LasModifierId", "dbo.Users");
            DropForeignKey("dbo.ArticleEvaluations", "EvaluatorId", "dbo.Appraisers");
            DropForeignKey("dbo.ArticleEvaluations", "LasModifierId", "dbo.Users");
            DropForeignKey("dbo.ArticleEvaluations", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Articles", "LasModifierId", "dbo.Users");
            DropForeignKey("dbo.Articles", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.ArticleEvaluations", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.Questions", "LasModifierId", "dbo.Users");
            DropForeignKey("dbo.QuestionArticleEvaluation", "ArticleEvaluationId", "dbo.ArticleEvaluations");
            DropForeignKey("dbo.QuestionArticleEvaluation", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.ArticleEvaluationQuestions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.ArticleEvaluationQuestions", "LasModifierId", "dbo.Users");
            DropForeignKey("dbo.ArticleEvaluationQuestions", "ArticleEvaluationId", "dbo.ArticleEvaluations");
            DropForeignKey("dbo.ArticleEvaluationQuestions", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.Questions", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.AnswerOptions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.AnswerOptionTeachermentEvaluation", "ArticleEvaluationId", "dbo.ArticleEvaluations");
            DropForeignKey("dbo.AnswerOptionTeachermentEvaluation", "AnswerOptionId", "dbo.AnswerOptions");
            DropForeignKey("dbo.Interviews", "InterviewerId", "dbo.Appraisers");
            DropForeignKey("dbo.Interviews", "LasModifierId", "dbo.Users");
            DropForeignKey("dbo.Interviews", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Interviews", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.EntireEvaluations", "EvaluatorId", "dbo.Appraisers");
            DropForeignKey("dbo.EntireEvaluations", "LasModifierId", "dbo.Users");
            DropForeignKey("dbo.EntireEvaluations", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.EntireEvaluations", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.Appraisers", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.Appraisers", "AppraiserTitleId", "dbo.Titles");
            DropForeignKey("dbo.EducationalBackgrounds", "LasModifierId", "dbo.Users");
            DropForeignKey("dbo.EducationalBackgrounds", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.Institutions", "LasModifierId", "dbo.Users");
            DropForeignKey("dbo.Institutions", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.EducationalBackgrounds", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.Teachers", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.Teachers", "ApproveById", "dbo.Users");
            DropForeignKey("dbo.Addresses", "LasModifierId", "dbo.Users");
            DropForeignKey("dbo.Addresses", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Addresses", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Roles");
            DropIndex("dbo.QuestionArticleEvaluation", new[] { "ArticleEvaluationId" });
            DropIndex("dbo.QuestionArticleEvaluation", new[] { "QuestionId" });
            DropIndex("dbo.AnswerOptionTeachermentEvaluation", new[] { "ArticleEvaluationId" });
            DropIndex("dbo.AnswerOptionTeachermentEvaluation", new[] { "AnswerOptionId" });
            DropIndex("dbo.MessageAttachments", new[] { "MessageId" });
            DropIndex("dbo.Messages", new[] { "ConversationId" });
            DropIndex("dbo.Messages", new[] { "SenderId" });
            DropIndex("dbo.Messages", new[] { "ReplyId" });
            DropIndex("dbo.Conversations", new[] { "ReceiverId" });
            DropIndex("dbo.Conversations", new[] { "SenderId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.AuditLogs", new[] { "CreatorId" });
            DropIndex("dbo.AuditLogs", "IX_AuditEntityId");
            DropIndex("dbo.AuditLogs", "IX_AuditTableName");
            DropIndex("dbo.AuditLogs", "IX_AuditType");
            DropIndex("dbo.TrainingCenters", new[] { "LasModifierId" });
            DropIndex("dbo.TrainingCenters", new[] { "CreatorId" });
            DropIndex("dbo.TrainingCenters", "IX_TrainingCenterState");
            DropIndex("dbo.TrainingCenters", "IX_UniqueCenterName");
            DropIndex("dbo.TrainingCourses", new[] { "LasModifierId" });
            DropIndex("dbo.TrainingCourses", new[] { "CreatorId" });
            DropIndex("dbo.TrainingCourses", "IX_UniqueCourseCode");
            DropIndex("dbo.ResearchExperiences", new[] { "LasModifierId" });
            DropIndex("dbo.ResearchExperiences", new[] { "CreatorId" });
            DropIndex("dbo.ResearchExperiences", new[] { "TeacherId" });
            DropIndex("dbo.ReferentialTeachers", new[] { "TeacherId" });
            DropIndex("dbo.ReferentialTeachers", new[] { "ReferencedToId" });
            DropIndex("dbo.ReferentialTeachers", new[] { "ReferencedFromId" });
            DropIndex("dbo.WorkExperiences", new[] { "LasModifierId" });
            DropIndex("dbo.WorkExperiences", new[] { "CreatorId" });
            DropIndex("dbo.WorkExperiences", new[] { "TitleId" });
            DropIndex("dbo.WorkExperiences", new[] { "TeacherId" });
            DropIndex("dbo.TeacherInServiceCourseTypes", new[] { "LasModifierId" });
            DropIndex("dbo.TeacherInServiceCourseTypes", new[] { "CreatorId" });
            DropIndex("dbo.TeacherInServiceCourseTypes", new[] { "InServiceCourseTypeTitleId" });
            DropIndex("dbo.TeacherInServiceCourseTypes", new[] { "TeacherId" });
            DropIndex("dbo.EducationalExperiences", new[] { "LasModifierId" });
            DropIndex("dbo.EducationalExperiences", new[] { "CreatorId" });
            DropIndex("dbo.EducationalExperiences", new[] { "TitleId" });
            DropIndex("dbo.EducationalExperiences", new[] { "TeacherId" });
            DropIndex("dbo.Articles", new[] { "LasModifierId" });
            DropIndex("dbo.Articles", new[] { "CreatorId" });
            DropIndex("dbo.Articles", new[] { "TeacherId" });
            DropIndex("dbo.ArticleEvaluationQuestions", new[] { "LasModifierId" });
            DropIndex("dbo.ArticleEvaluationQuestions", new[] { "CreatorId" });
            DropIndex("dbo.ArticleEvaluationQuestions", "IX_ ArticleEvaluationQuestion");
            DropIndex("dbo.Questions", new[] { "LasModifierId" });
            DropIndex("dbo.Questions", new[] { "CreatorId" });
            DropIndex("dbo.AnswerOptions", new[] { "QuestionId" });
            DropIndex("dbo.ArticleEvaluations", new[] { "LasModifierId" });
            DropIndex("dbo.ArticleEvaluations", new[] { "CreatorId" });
            DropIndex("dbo.ArticleEvaluations", new[] { "EvaluatorId" });
            DropIndex("dbo.ArticleEvaluations", new[] { "ArticleId" });
            DropIndex("dbo.Interviews", new[] { "LasModifierId" });
            DropIndex("dbo.Interviews", new[] { "CreatorId" });
            DropIndex("dbo.Interviews", new[] { "InterviewerId" });
            DropIndex("dbo.Interviews", new[] { "TeacherId" });
            DropIndex("dbo.EntireEvaluations", new[] { "LasModifierId" });
            DropIndex("dbo.EntireEvaluations", new[] { "CreatorId" });
            DropIndex("dbo.EntireEvaluations", new[] { "EvaluatorId" });
            DropIndex("dbo.EntireEvaluations", new[] { "TeacherId" });
            DropIndex("dbo.Appraisers", new[] { "LasModifierId" });
            DropIndex("dbo.Appraisers", new[] { "CreatorId" });
            DropIndex("dbo.Appraisers", new[] { "AppraiserTitleId" });
            DropIndex("dbo.Titles", new[] { "LasModifierId" });
            DropIndex("dbo.Titles", new[] { "CreatorId" });
            DropIndex("dbo.Titles", "IX_UniqueTitleName");
            DropIndex("dbo.Institutions", new[] { "LasModifierId" });
            DropIndex("dbo.Institutions", new[] { "CreatorId" });
            DropIndex("dbo.EducationalBackgrounds", new[] { "LasModifierId" });
            DropIndex("dbo.EducationalBackgrounds", new[] { "CreatorId" });
            DropIndex("dbo.EducationalBackgrounds", new[] { "StudyFieldId" });
            DropIndex("dbo.EducationalBackgrounds", new[] { "InstitutionId" });
            DropIndex("dbo.EducationalBackgrounds", new[] { "TeacherId" });
            DropIndex("dbo.Addresses", new[] { "LasModifierId" });
            DropIndex("dbo.Addresses", new[] { "CreatorId" });
            DropIndex("dbo.Addresses", new[] { "TeacherId" });
            DropIndex("dbo.Teachers", new[] { "LasModifierId" });
            DropIndex("dbo.Teachers", new[] { "CreatorId" });
            DropIndex("dbo.Teachers", new[] { "ApproveById" });
            DropIndex("dbo.Teachers", new[] { "TrainingCourseId" });
            DropIndex("dbo.Teachers", new[] { "PositionId" });
            DropIndex("dbo.Teachers", "IX_TeacherBirthPlaceState");
            DropIndex("dbo.Teachers", "IX_TeacherBirthPlaceCity");
            DropIndex("dbo.Teachers", "IX_TeacherBirthCertificateNumber");
            DropIndex("dbo.Teachers", "IX_TeacherNationalCode");
            DropIndex("dbo.Teachers", "IX_TeacherLastName");
            DropIndex("dbo.Teachers", "IX_TeacherFirstName");
            DropIndex("dbo.Users", new[] { "UserName" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.Roles", "IX_RoleName");
            DropTable("dbo.QuestionArticleEvaluation");
            DropTable("dbo.AnswerOptionTeachermentEvaluation");
            DropTable("dbo.Settings");
            DropTable("dbo.MessageAttachments");
            DropTable("dbo.Messages");
            DropTable("dbo.Conversations");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.AuditLogs");
            DropTable("dbo.TrainingCenters");
            DropTable("dbo.TrainingCourses");
            DropTable("dbo.ResearchExperiences");
            DropTable("dbo.ReferentialTeachers");
            DropTable("dbo.WorkExperiences");
            DropTable("dbo.TeacherInServiceCourseTypes");
            DropTable("dbo.EducationalExperiences");
            DropTable("dbo.Articles");
            DropTable("dbo.ArticleEvaluationQuestions");
            DropTable("dbo.Questions");
            DropTable("dbo.AnswerOptions");
            DropTable("dbo.ArticleEvaluations");
            DropTable("dbo.Interviews");
            DropTable("dbo.EntireEvaluations");
            DropTable("dbo.Appraisers");
            DropTable("dbo.Titles");
            DropTable("dbo.Institutions");
            DropTable("dbo.EducationalBackgrounds");
            DropTable("dbo.Addresses");
            DropTable("dbo.Teachers");
            DropTable("dbo.Users");
            DropTable("dbo.UserRole");
            DropTable("dbo.Roles");
        }
    }
}
