using System.Data.Entity;
using System.Web;
using Decision.DomainClasses;
using Decision.DomainClasses.Identity;

namespace Decision.DataLayer.Context
{
    public class ApplicationDbContext : ApplicationDbContextBase
    {
        #region Constructors
        public ApplicationDbContext(HttpContextBase httpContextBase)
            : base(httpContextBase)
        {
        }
        #endregion

        #region Properties
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        #endregion
    }
}