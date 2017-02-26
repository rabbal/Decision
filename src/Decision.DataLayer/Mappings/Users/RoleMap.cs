using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using Decision.DomainClasses.Identity;

namespace Decision.DataLayer.Mappings.Users
{
    
    public class RoleMap : TrackableMap<Role,long>
    {
        public RoleMap()
        {
            Property(a => a.RowVersion).IsRowVersion();

            Property(r => r.Name)
                 .IsRequired()
                 .HasMaxLength(50)
                 .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("UIX_Role_RoleName") { IsUnique = true }));
            
            HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.RoleId);
            
        }
    }
}
