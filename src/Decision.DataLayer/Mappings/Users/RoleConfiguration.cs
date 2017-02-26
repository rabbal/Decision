using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace Decision.DataLayer.Mappings.Users
{
    
    public class RoleConfiguration : AuditableEntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            Property(r => r.RowVersion).IsRowVersion();
            Property(r => r.Name)
                 .IsRequired()
                 .HasMaxLength(50)
                 .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("UIX_Role_RoleName") { IsUnique = true }));
            Property(r => r.DisplayName).HasMaxLength(50).IsRequired();
            HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.RoleId);
        }
    }
}
