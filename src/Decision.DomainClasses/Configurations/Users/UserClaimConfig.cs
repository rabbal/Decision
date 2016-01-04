using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Entities.Users;

namespace Decision.DomainClasses.Configurations.Users
{
    /// <summary>
    /// نشان دهنده مپینگ کلاس ادعا های کاربر
    /// </summary>
    public class UserClaimConfig : EntityTypeConfiguration<UserClaim>
    {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public UserClaimConfig()
        {
            ToTable("UserClaims");
        }
    }
}
