using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Entities.Users;

namespace Decision.DomainClasses.Configurations.Users
{
    /// <summary>
    /// نشان دهنده  مپینگ کلاس کاربر
    /// </summary>
    public class UserConfig : EntityTypeConfiguration<User>
    {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public UserConfig()
        {
            ToTable(nameof(Users));
            HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.UserId);
            HasMany(u => u.Claims).WithRequired().HasForeignKey(uc => uc.UserId);
            HasMany(u => u.Logins).WithRequired().HasForeignKey(ul => ul.UserId);
            Property(u => u.LastIp).IsOptional().HasMaxLength(20);
            Property(u => u.RowVersion).IsRowVersion();
            Property(u => u.DisplayName).IsRequired().HasMaxLength(50);
            Property(u => u.PhoneNumber).IsOptional().HasMaxLength(20);
            Property(u => u.DirectPermissions).HasColumnType("xml");
            Ignore(u => u.XmlDirectPermissions);
            Ignore(u => u.ConnectionIds);
            Property(u => u.UserName)
                 .IsRequired()
                 .HasMaxLength(256)
                 .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_UserName") { IsUnique = true }));

            Property(u => u.Email)
                .IsOptional()
                .HasMaxLength(256);
        }
    }
}
