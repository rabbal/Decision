using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Decision.DataLayer.Configurations.Common
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
            Property(a => a.XmlNewValue).IsOptional().HasColumnType("xml");
            Property(a => a.XmlOldValue).IsOptional().HasColumnType("xml");
            Property(a => a.Description).IsRequired().HasMaxLength(1024);

            Property(a => a.Entity).IsRequired().HasMaxLength(20)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_AuditTableName")));

            Property(a => a.EntityId).IsRequired().HasMaxLength(20)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_AuditEntityId")));
           
            Ignore(a => a.XmlNewValueWrapper);
            Ignore(a => a.XmlOldValueWrapper);

            HasRequired(a=>a.Operant).WithMany().HasForeignKey(a=>a.OperantId).WillCascadeOnDelete(false);

        }
    }
}
