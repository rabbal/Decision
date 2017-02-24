using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using Decision.DomainClasses.Users;

namespace Decision.DataLayer.Configurations.Users
{
    public class UserConfiguration : AuditableEntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable(nameof(Users));

            Property(u => u.RowVersion).IsRowVersion();
            Property(u => u.PhoneNumber).IsOptional().HasMaxLength(20);
            Property(u => u.LastIp).IsOptional().HasMaxLength(20);
            Property(u => u.DisplayName).IsRequired().HasMaxLength(50);
            Property(u => u.TrimmedDisplayName).HasMaxLength(50).IsRequired();
            Property(u => u.BannedReason).HasMaxLength(256).IsOptional();

            Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("UIX_User_UserName") {IsUnique = true}));
            Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("UIX_User_Email") {IsUnique = true}));

            HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.UserId);
            HasMany(u => u.Claims).WithRequired().HasForeignKey(uc => uc.UserId);
            HasMany(u => u.Logins).WithRequired().HasForeignKey(ul => ul.UserId);
            HasMany(u => u.ActivityLogs).WithRequired(a=>a.User).HasForeignKey(ul => ul.UserId);
            HasMany(u => u.AuditLogs).WithRequired(a => a.User).HasForeignKey(ul => ul.UserId);
            HasMany(u => u.Notifications).WithRequired(a => a.User).HasForeignKey(ul => ul.UserId);

        }
    }
}
