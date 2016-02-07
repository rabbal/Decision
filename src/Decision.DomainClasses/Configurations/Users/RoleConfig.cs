using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Entities.Users;

namespace Decision.DomainClasses.Configurations.Users
{
    /// <summary>
    /// نشان دهنده مپینگ کلاس گروه کاربری
    /// </summary>
    public class RoleConfig : EntityTypeConfiguration<Role>
    {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public RoleConfig()
        {
            ToTable("Roles");
            Property(r => r.RowVersion).IsRowVersion();
            Ignore(r => r.XmlPermissions);
            Property(r => r.Name)
                 .IsRequired()
                 .HasMaxLength(50)
                 .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_RoleName") { IsUnique = true }));
            Property(r => r.RowVersion).IsRowVersion();
            Property(r => r.Permissions).HasColumnType("xml");
            HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.RoleId);
        }
    }
}
