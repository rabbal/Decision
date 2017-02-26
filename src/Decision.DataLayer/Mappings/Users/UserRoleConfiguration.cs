using System.Data.Entity.ModelConfiguration;

namespace Decision.DataLayer.Mappings.Users
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
