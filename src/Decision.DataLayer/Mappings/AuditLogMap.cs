using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using Decision.DomainClasses;

namespace Decision.DataLayer.Mappings
{
    public class AuditLogMap : EntityMap<AuditLog, long>
    {
        public AuditLogMap()
        {

            Property(a => a.JsonNewValues).IsRequired().IsMaxLength();
            Property(a => a.JsonOriginalValues).IsRequired().IsMaxLength();

            Property(a => a.EntityType).IsRequired().HasMaxLength(255)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_AuditLog_EntityType")));

            Property(a => a.EntityId).IsRequired().HasMaxLength(20)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_AuditLog_EntityId")));

            HasRequired(a => a.CreatorUser).WithMany().HasForeignKey(a => a.CreatorUserId).WillCascadeOnDelete(false);
        }
    }
}
