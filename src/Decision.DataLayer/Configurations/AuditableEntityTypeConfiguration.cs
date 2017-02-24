using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Common;

namespace Decision.DataLayer.Configurations
{
    public abstract class AuditableEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : class, IAuditableEntity
    {
        protected AuditableEntityTypeConfiguration()
        {
            Property(a => a.CreatorIp).HasMaxLength(20).IsRequired();
            Property(a => a.LastModifierIp).HasMaxLength(20).IsRequired();

            Property(a => a.CreatedOn).IsRequired();
            Property(a => a.LastModifiedOn).IsRequired();

            Property(a => a.CreatedBy).HasMaxLength(50).IsRequired();
            Property(a => a.LastModifiedBy).HasMaxLength(50).IsRequired();
        }
    }
}
