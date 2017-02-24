using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.DomainClasses.Common;

namespace Decision.DataLayer.Configurations.Common
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
