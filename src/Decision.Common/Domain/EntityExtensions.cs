using System;
using System.Data.Entity.Core.Objects;
using Decision.Common.Domain.Tracking;
using Decision.Common.Extensions;

namespace Decision.Common.Domain
{
    public static class EntityExtensions
    {
        public static bool IsNullOrDeleted(this ISoftDeletable entity)
        {
            return entity == null || entity.IsDeleted;
        }

        public static void UnDelete(this ISoftDeletable entity)
        {
            entity.IsDeleted = false;
            if (!(entity is IDeletionTracking)) return;

            var deletionAuditedEntity = entity.As<IDeletionTracking>();
            deletionAuditedEntity.DeletedDateTime = null;
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