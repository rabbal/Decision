using System.Data.Entity;
using System.Web;
using Decision.DomainClasses;
using Decision.DomainClasses.Applicants;
using Decision.DomainClasses.Evaluations;
using Decision.DomainClasses.Identity;
using Decision.DomainClasses.Messages;

namespace Decision.DataLayer.Context
{
    public class ApplicationDbContext : ApplicationDbContextBase
    {
        #region Constructor
        public ApplicationDbContext(HttpContextBase httpContextBase)
            : base(httpContextBase)
        {
        }
        #endregion

        #region Properties

        public DbSet<RoleClaim> RoleClaims { get; set; }
        public DbSet<UserUsedPassword> UserUsedPasswords { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<EducationalBackground> EducationalBackgrounds { get; set; }
        public DbSet<EducationalExperience> EducationalExperiences { get; set; }
        public DbSet<ResearchExperience> ResearchExperiences { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<EntireEvaluation> EntireEvaluations { get; set; }
        public DbSet<EvaluationPeriod> EvaluationPeriods { get; set; }
        public DbSet<InterviewAnswer> InterviewAnswers { get; set; }
        public DbSet<QuestionCategory> QuestionCategories { get; set; }

        #endregion
    }
}