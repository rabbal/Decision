using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using Decision.Framework.Domain.Entities;
using Decision.Framework.Domain.Entities.Tracking;
using Decision.Framework.Extensions;
using Microsoft.AspNet.Identity;

namespace Decision.Framework.EntityFrameworkToolkit.Extensions
{
    public static class DbChangeTrackerExtensions
    {
        public static void RejectChanges(this DbChangeTracker changeTracker)
        {
            foreach (var entry in changeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        public static string[] GetChangedEntityNames(this DbChangeTracker changeTracker)
        {
            return changeTracker.Entries()
                .Where(x => x.State == EntityState.Added ||
                            x.State == EntityState.Modified ||
                            x.State == EntityState.Deleted)
                .Select(x => ObjectContext.GetObjectType(x.Entity.GetType()).FullName)
                .Distinct()
                .ToArray();
        }
        public static void SetRowLevelSecurityPropertyValue(this DbChangeTracker changeTracker, HttpContextBase httpContextBase)
        {
            var userId = GetUserId(httpContextBase);
            if (!userId.HasValue) return;

            var addedEntries = changeTracker.Entries<IHasRowLevelSecurity>()
              .Where(x => x.State == EntityState.Added);

            foreach (var addedEntry in addedEntries)
            {
                addedEntry.Entity.UserId = userId.Value;
            }
        }
        public static void SetTrackablePropertyValues(this DbChangeTracker changeTracker, HttpContextBase httpContextBase)
        {
            var userAgent = httpContextBase?.Request?.GetUserAgent();
            var userIp = httpContextBase?.Request?.GetUserIp();
            var now = DateTime.Now;
            var userId = GetUserId(httpContextBase);

            var modifiedEntries = changeTracker.Entries<ITrackable>()
                   .Where(x => x.State == EntityState.Modified);
            foreach (var modifiedEntry in modifiedEntries)
            {
                modifiedEntry.Entity.LasModificationDateTime = now;
                modifiedEntry.Entity.LastModifierUserId = userId;
                modifiedEntry.Entity.LastModifierIp = userIp;
                modifiedEntry.Entity.LastModifierBrowserName = userAgent;
            }

            var addedEntries = changeTracker.Entries<ITrackable>()
                .Where(x => x.State == EntityState.Added);
            foreach (var addedEntry in addedEntries)
            {
                addedEntry.Entity.CreationDateTime = now;
                addedEntry.Entity.CreatorUserId = userId;
                addedEntry.Entity.CreatorIp = userIp;
                addedEntry.Entity.CreatorBrowserName = userAgent;
            }

        }

        private static long? GetUserId(HttpContextBase httpContextBase)
        {
            long? userId = null;
            var userIdValue = httpContextBase?.User?.Identity?.GetUserId();
            if (!string.IsNullOrWhiteSpace(userIdValue))
            {
                userId = long.Parse(userIdValue);
            }
            return userId;
        }
    }
}