using System;
using System.Data.Entity.Core.Objects;
using Decision.Framework.Domain.Entities.Tracking;
using Decision.Framework.Extensions;

namespace Decision.Framework.Domain.Entities
{
    public static class EntityExtensions
    {
        public static bool IsNullOrDeleted(this ISoftDelete entity)
        {
            return entity == null || entity.IsDeleted;
        }

        public static void UnDelete(this ISoftDelete entity)
        {
            entity.IsDeleted = false;
            if (!(entity is IDeletionTracking)) return;

            var deletionAuditedEntity = entity.As<IDeletionTracking>();
            deletionAuditedEntity.DeletionDateTime = null;
            deletionAuditedEntity.DeleterUserId = null;
        }

        public static Type GetUnproxiedEntityType(this Entity entity)
        {
            var userType = ObjectContext.GetObjectType(entity.GetType());
            return userType;
        }

        public static Type GetUnproxiedEntityType<TKey>(this Entity<TKey> entity) where TKey : IEquatable<TKey>
        {
            var userType = ObjectContext.GetObjectType(entity.GetType());
            return userType;
        }
    }
}