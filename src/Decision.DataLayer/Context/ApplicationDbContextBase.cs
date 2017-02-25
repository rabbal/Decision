using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Decision.DomainClasses.Identity;
using Decision.Framework.Domain.Entities;
using Decision.Framework.Domain.Entities.Tracking;
using Decision.Framework.Domain.Uow;
using Decision.Framework.GuardToolkit;
using EFSecondLevelCache;
using EntityFramework.BulkInsert.Extensions;
using EntityFramework.DynamicFilters;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RefactorThis.GraphDiff;

namespace Decision.DataLayer.Context
{
    public abstract class ApplicationDbContextBase :
        IdentityDbContext<User, Role, long, UserLogin, UserRole, UserClaim>, IUnitOfWork
    {
        #region Constants
        private const string ConnectionStringName = "DefaultConnection";
        #endregion

        #region Fields
        private readonly HttpContextBase _httpContextBase;
        private IUnitOfWork _unitOfWorkImplementation;

        #endregion

        #region Constructors
        protected ApplicationDbContextBase(HttpContextBase httpContextBase)
            : base(ConnectionStringName)
        {
            _httpContextBase = httpContextBase;
            Configuration.LazyLoadingEnabled = false;
        }
        #endregion

        #region IUnitOfWork Members
        IDbSet<TEntity> IUnitOfWork.Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }
        public void MarkAsSoftDeleted<TEntity>(TEntity entity) where TEntity : class, ISoftDelete
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;
        }

        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
        {
            return Database.SqlQuery<T>(sql, parameters).ToList();
        }

        public void ExecuteSqlCommand(string query)
        {
            Database.ExecuteSqlCommand(query);
        }

        public void ExecuteSqlCommand(string query, params object[] parameters)
        {
            Database.ExecuteSqlCommand(query, parameters);
        }

        public void BulkInsert<T>(IEnumerable<T> data)
        {
            BulkInsertExtension.BulkInsert(this, data);
        }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().AddRange(entities);
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().RemoveRange(entities);
        }

        public T Update<T>(T entity, Expression<Func<IUpdateConfiguration<T>, object>> mapping)
             where T : class, new()
        {
            return this.UpdateGraph(entity, mapping);
        }

        public void ForceDatabaseInitialize()
        {
            Database.Initialize(force: true);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            PerformPreSaveActions();

            Configuration.AutoDetectChangesEnabled = false; //for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChanges();
            Configuration.AutoDetectChangesEnabled = true;

            return result;
        }
        public int SaveChanges(bool invalidateCacheDependencies)
        {
            ChangeTracker.DetectChanges();

            PerformPreSaveActions();
            var changedEntityNames = ChangeTracker.GetChangedEntityNames();
            
            Configuration.AutoDetectChangesEnabled = false; //for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChanges();
            Configuration.AutoDetectChangesEnabled = true;

            if (!invalidateCacheDependencies) return result;

            InvalidateEfSecondLevelCache(changedEntityNames);

            return result;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            PerformPreSaveActions();

            Configuration.AutoDetectChangesEnabled = false; //for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChangesAsync(cancellationToken);
            Configuration.AutoDetectChangesEnabled = true;

            return result;
        }

        public Task<int> SaveChangesAsync(bool invalidateCacheDependencies, CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            PerformPreSaveActions();
            var changedEntityNames = ChangeTracker.GetChangedEntityNames();

            Configuration.AutoDetectChangesEnabled = false; //for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChangesAsync(cancellationToken);
            Configuration.AutoDetectChangesEnabled = true;

            if (!invalidateCacheDependencies) return result;

            InvalidateEfSecondLevelCache(changedEntityNames);

            return result;
        }

        public void DisableFilter(string filterName)
        {
            DynamicFilterExtensions.DisableFilter(this, filterName);
        }

        public void EnableRowLevelSecurity()
        {
            var userId = GetUserId();
            if (!userId.HasValue)
                throw new InvalidOperationException($"for enable Row Level Security {nameof(userId)} must be has value (user must be authenticated)");

            EnableFilter(nameof(IHasRowLevelSecurity));
            this.SetFilterScopedParameterValue(nameof(IHasRowLevelSecurity), userId);
        }

        public void EnableFilter(string filterName)
        {
            DynamicFilterExtensions.EnableFilter(this, filterName);
        }

        public void EnableFilter(string filterName, object parameterValue)
        {
            EnableFilter(filterName);
            this.SetFilterScopedParameterValue(filterName, parameterValue);
        }

        public void ChangeFilterParameterValue(string filterName, object parameterValue)
        {
            this.SetFilterScopedParameterValue(filterName, parameterValue);
        }

        public void DisableAllFilters()
        {
            DynamicFilterExtensions.DisableAllFilters(this);
        }

        public void EnableAllFilters()
        {
            DynamicFilterExtensions.EnableAllFilters(this);
        }
        #endregion
        
        #region Protected Methods
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Check.ArgumentNotNull(modelBuilder, nameof(modelBuilder));

            ConfigureFilters(modelBuilder);

            AddCustomConventions(modelBuilder);

            modelBuilder.Ignore<Entity>();
            modelBuilder.Ignore<TrackableEntity>();
            modelBuilder.Ignore<FullTrackableEntity>();

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        protected void PerformPreSaveActions()
        {
            ChangeTracker.SetTrackablePropertyValues(_httpContextBase);
            ChangeTracker.SetRowLevelSecurityPropertyValue(_httpContextBase);
        }
        #endregion

        #region Private Methods
        private static void AddCustomConventions(DbModelBuilder modelBuilder)
        {
            //modelBuilder.AddSoftDeleteConvention();
        }

        private static void ConfigureFilters(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Filter(nameof(IHasRowLevelSecurity),
            //    (IHasRowLevelSecurity rls, long userId) => rls.UserId == userId, 0);
            //modelBuilder.Filter(nameof(ISoftDelete), (ISoftDelete d) => d.IsDeleted, false);
            //modelBuilder.Filter(nameof(ISystemDefaultEntry), (ISystemDefaultEntry e) => e.IsSystemEntry, true);
        }
        private static void InvalidateEfSecondLevelCache(string [] changedEntityNames)
        {
            new EFCacheServiceProvider().InvalidateCacheDependencies(changedEntityNames);
        }
       


        private long? GetUserId()
        {
            long? userId = null;
            var userIdValue = _httpContextBase?.User?.Identity?.GetUserId();
            if (!string.IsNullOrWhiteSpace(userIdValue))
            {
                userId = long.Parse(userIdValue);
            }
            return userId;
        }
        #endregion
    }
}
