using System.Data.Entity.ModelConfiguration;

namespace Decision.DataLayer.Mappings.Users
{
    public class UserClaimConfiguration : EntityTypeConfiguration<UserClaim>
    {
        public UserClaimConfiguration()
        {
            ToTable("UserClaims");
        }
    }
}
