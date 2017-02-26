using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Identity;

namespace Decision.DataLayer.Mappings.Users
{
    public class UserRoleMap : EntityTypeConfiguration<UserRole>
    {
        public UserRoleMap()
        {
            HasKey(r => new { r.UserId, r.RoleId });
            ToTable(nameof(UserRoleMap));
        }
    }
}
