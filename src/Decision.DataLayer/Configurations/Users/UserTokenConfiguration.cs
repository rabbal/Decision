using System.Data.Entity.ModelConfiguration;

namespace Decision.DataLayer.Configurations.Users
{
    public class UserTokenConfiguration : EntityTypeConfiguration<UserToken>
    {
        public UserTokenConfiguration()
        {
            Property(token => token.AccessTokenExpireOn).IsRequired();
            Property(token => token.RefreshToken).HasMaxLength(256).IsRequired();
            Property(token => token.AccessTokenHash).HasMaxLength(256).IsRequired();
            Property(token => token.Subject).HasMaxLength(256).IsRequired();
        }
    }
}
