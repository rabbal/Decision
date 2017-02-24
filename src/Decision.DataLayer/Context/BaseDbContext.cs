using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Decision.DomainClasses.Users;
using EFSecondLevelCache;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NTierMvcFramework.DataLayer.Context
{
    public abstract class BaseDbContext : IdentityDbContext<User, Role, long, UserLogin, UserRole, UserClaim>
    {
        #region Constructor
        protected BaseDbContext(string connectionString)
            : base(connectionString)
        {
        }

        #endregion

        #region Public Methods
        public int SaveAllChanges(bool invalidateCacheDependencies = true)
        {
            PerformPreSaveActions();
            var result = SaveChanges();

            if (!invalidateCacheDependencies) return result;

            InvalidateEfSecondLevelCache();

            return result;
        }

      
        public Task<int> SaveAllChangesAsync(bool invalidateCacheDependencies = true)
        {
            PerformPreSaveActions();
            var result = SaveChangesAsync();

            if (!invalidateCacheDependencies) return result;

            InvalidateEfSecondLevelCache();

            return result;
        }

        public override int SaveChanges()
        {
            PerformPreSaveActions();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            PerformPreSaveActions();
            return base.SaveChangesAsync();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            PerformPreSaveActions();
            return base.SaveChangesAsync(cancellationToken);
        }

        public void RejectChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries())
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
        #endregion

        #region Protected Methods
        protected abstract void PerformPreSaveActions();

        #endregion

        #region Private Methods
        private void InvalidateEfSecondLevelCache()
        {
            var changedEntityNames = this.GetChangedEntityNames();
            new EFCacheServiceProvider().InvalidateCacheDependencies(changedEntityNames);
        }

        private string[] GetChangedEntityNames()
        {
            return ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added ||
                            x.State == EntityState.Modified ||
                            x.State == EntityState.Deleted)
                .Select(x => ObjectContext.GetObjectType(x.Entity.GetType()).FullName)
                .Distinct()
                .ToArray();
        }


        #endregion
    }
}
