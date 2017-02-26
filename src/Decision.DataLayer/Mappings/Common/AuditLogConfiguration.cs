using System.Data.Entity.ModelConfiguration;

namespace Decision.DataLayer.Mappings.Common
{
    public class AuditLogConfiguration : EntityTypeConfiguration<AuditLog>
    {
        public AuditLogConfiguration()
        {
            Property(a => a.EntityType).HasMaxLength(50).IsRequired();
            Property(a => a.JsonNewValues).IsRequired().IsMaxLength();
            Property(a => a.JsonOriginalValues).IsRequired().IsMaxLength();
        }
    }
}
