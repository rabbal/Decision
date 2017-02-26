using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Identity;

namespace Decision.DataLayer.Mappings.Users
{
    public class UserLoginMap : EntityTypeConfiguration<UserLogin>
    {
        public UserLoginMap()
        {
            HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId });
            ToTable("UserLogins");
        }
    }
}
