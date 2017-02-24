using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Users;

namespace Decision.DataLayer.Configurations.Users
{
    public class UserRoleConfiguration : EntityTypeConfiguration<UserRole>
    {
        public UserRoleConfiguration()
        {
            HasKey(r => new { r.UserId, r.RoleId });
            ToTable(nameof(UserRole));
        }
    }
}
