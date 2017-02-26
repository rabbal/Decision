using System;
using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Identity;
using Decision.Framework.Domain.Entities.Tracking;

namespace Decision.DataLayer.Mappings
{
    public abstract class TrackableEntityMap<TEntity, TKey> : EntityMap<TEntity, TKey>
        where TEntity : TrackableEntity<TKey, User>
        where TKey : IEquatable<TKey>
    {
        protected TrackableEntityMap()
        {
            Property(a => a.RowVersion).IsRowVersion();

            Property(a => a.CreatorIp).HasMaxLength(255).IsRequired();
            Property(a => a.LastModifierIp).HasMaxLength(255).IsOptional();

            Property(a => a.CreationDateTime).IsRequired();
            Property(a => a.LasModificationDateTime).IsOptional();

            Property(a => a.CreatorBrowserName).HasMaxLength(1000).IsRequired();
            Property(a => a.LastModifierBrowserName).HasMaxLength(1000).IsOptional();

            HasOptional(a => a.CreatorUser).WithMany().HasForeignKey(a => a.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(a => a.LastModifierUser).WithMany().HasForeignKey(a => a.LastModifierUserId).WillCascadeOnDelete(false);
        }
    }
}
