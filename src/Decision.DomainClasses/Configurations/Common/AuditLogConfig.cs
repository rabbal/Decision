using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.Common;

namespace Decision.DomainClasses.Configurations.Common
{
    /// <summary>
    /// کلاس میپنگ مربوط به لاگ آماری
    /// </summary>
    public class AuditLogConfig : EntityTypeConfiguration<AuditLog>
    {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public AuditLogConfig()
        {
            Property(a => a.NewValue).IsOptional().HasColumnType("xml");
            Property(a => a.OldValue).IsOptional().HasColumnType("xml");
            Property(a => a.Description).IsRequired().HasMaxLength(1024);
            Property(a => a.TableName).IsOptional().HasMaxLength(20)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_AuditTableName") ));
            Property(a => a.RecordedEntityId).IsOptional()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_AuditEntityId")));
            Property(a => a.Type).IsOptional()
               .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_AuditType")));

            Ignore(a => a.XmlNewValue);
            Ignore(a => a.XmlOldValue);

            HasRequired(a=>a.Creator).WithMany(a=>a.AuditLogs).HasForeignKey(a=>a.CreatorId).WillCascadeOnDelete(false);

        }
    }
}
