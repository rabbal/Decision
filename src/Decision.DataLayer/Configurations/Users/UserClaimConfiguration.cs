using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Users;

namespace Decision.DataLayer.Configurations.Users
{
    public class UserClaimConfiguration : EntityTypeConfiguration<UserClaim>
    {
        public UserClaimConfiguration()
        {
            ToTable("UserClaims");
        }
    }
}
