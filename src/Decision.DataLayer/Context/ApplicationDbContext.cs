using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Decision.DomainClasses.Configurations.Common;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.Users;
using Decision.Utility;
using EFSecondLevelCache;
using EntityFramework.Audit;
using EntityFramework.BulkInsert.Extensions;
using EntityFramework.Extensions;
using EntityFramework.Filters;
using Microsoft.AspNet.Identity.EntityFramework;
using RefactorThis.GraphDiff;

namespace Decision.DataLayer.Context
{
    public class ApplicationDbContext : IdentityDbContext
        <User, Role, Guid, UserLogin, UserRole, UserClaim>,
        IUnitOfWork
    {
        #region Fields

        private Guid _recordedEntitykey;
        private string _newValue;
        private string _oldValue;
        private readonly AuditLogger _auditLogger;
        private string _characteristicOfLogedEntity;
        #endregion

        #region Constrans
        private const string deleteMessage = "{0} نظر توسط یکی از از کاربران در شبکه،حذف شده است.لیست را رفرش کنید";
        private const string editMessage = "{0} نظر توسط یکی از از کاربران در شبکه،ویرایش شده است.برای مشاهده اطلاعات جدید ، لیست را رفرش کنید";
        #endregion

        #region Ctor


        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;

        }

        #endregion

        #region SaveChanges
        public int SaveChanges(bool loggable,string field)
        {
            if (!loggable) return SaveChanges();
            _oldValue = _auditLogger.LastLog.ToXml();
            _characteristicOfLogedEntity =
               _auditLogger.LastLog.Entities.First().Properties.First(a => a.Name == field).Original.ToString();
            var result = SaveChanges();
            _recordedEntitykey = Guid.Parse(_auditLogger.LastLog.Entities.First().Keys.First().ToString());
            _auditLogger.LastLog.Refresh();
            _newValue = _auditLogger.LastLog.ToXml();
            return result;
        }

        public Task<int> SaveChangesAsync(bool loggable,string field)
        {
            if (!loggable) return SaveChangesAsync();
            _oldValue = _auditLogger.LastLog.ToXml();
            _characteristicOfLogedEntity =
                _auditLogger.LastLog.Entities.First().Properties.First(a => a.Name == field).Original.ToString();
            var result = SaveChangesAsync();
            _recordedEntitykey = Guid.Parse(_auditLogger.LastLog.Entities.First().Keys.First().ToString());
            _auditLogger.LastLog.Refresh();
            _newValue = _auditLogger.LastLog.ToXml();
            return result;
        }
        public override int SaveChanges()
        {
            return SaveAllChanges();
        }


        public int SaveAllChanges(bool invalidateCacheDependencies = true)
        {
            var result = base.SaveChanges();
            if (!invalidateCacheDependencies) return result;
            var changedEntityNames = GetChangedEntityNames();
            new EFCacheServiceProvider().InvalidateCacheDependencies(changedEntityNames);
            return result;
        }

        public override Task<int> SaveChangesAsync()
        {
            return SaveAllChangesAsync();
        }

        public Task<int> SaveAllChangesAsync(bool invalidateCacheDependencies = true)
        {

            var result = base.SaveChangesAsync();
            if (!invalidateCacheDependencies) return result;

            var changedEntityNames = GetChangedEntityNames();
            new EFCacheServiceProvider().InvalidateCacheDependencies(changedEntityNames);
            return result;
        }
        public async Task<string> ConcurrencySaveChangesAsync()
        {
            try
            {
                await SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException concurrencyException)
            {
                var dbEntityEntry = concurrencyException.Entries.First();
                var dbPropertyValues = dbEntityEntry.GetDatabaseValues();
                return dbPropertyValues == null ? deleteMessage : editMessage;
            }
            return string.Empty;
        }
        public Task<string> ConcurrencySaveChangesAsync(bool loggable,string field)
        {
            if (!loggable) return ConcurrencySaveChangesAsync();
            _oldValue = _auditLogger.LastLog.ToXml();
            _characteristicOfLogedEntity =
               _auditLogger.LastLog.Entities.First().Properties.First(a => a.Name == field).Original.ToString();
            var result = ConcurrencySaveChangesAsync();
            _recordedEntitykey = Guid.Parse(_auditLogger.LastLog.Entities.First().Keys.First().ToString());
            _auditLogger.LastLog.Refresh();
            _newValue = _auditLogger.LastLog.ToXml();
            return result;
        }
        #endregion

        #region IUnitOfWork

        public T Update<T>(T entity, Expression<Func<IUpdateConfiguration<T>, object>> mapping)
            where T : class, new()
        {

            return this.UpdateGraph(entity, mapping);

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

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }
        public void MarkAsDetached<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Detached;
        }
        public void MarkAsAdded<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Added;
        }

        public void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;
        }

        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
        {
            return Database.SqlQuery<T>(sql, parameters).ToList();
        }

        public void AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            ((DbSet<TEntity>)Set<TEntity>()).AddRange(entities);
        }


        public void ForceDatabaseInitialize()
        {
            Database.Initialize(force: true);
        }

        public void EnableFiltering(string name)
        {
            this.EnableFilter(name);
        }

        public void EnableFiltering(string name, string parameter, object value)
        {
            this.EnableFilter(name).SetParameter(parameter, value);
        }

        public void DisableFiltering(string name)
        {
            this.DisableFilter(name);
        }

        public void BulkInsertData<T>(IEnumerable<T> data)
        {
            this.BulkInsert(data);
        }

        public bool ValidateOnSaveEnabled
        {
            get { return Configuration.ValidateOnSaveEnabled; }
            set { Configuration.ValidateOnSaveEnabled = value; }
        }

        public bool ProxyCreationEnabled
        {
            get { return Configuration.ProxyCreationEnabled; }
            set { Configuration.ProxyCreationEnabled = value; }
        }

        bool IUnitOfWork.AutoDetectChangesEnabled
        {
            get { return Configuration.AutoDetectChangesEnabled; }
            set { Configuration.AutoDetectChangesEnabled = value; }
        }

        public bool ForceNoTracking { get; set; }
        #endregion

        #region Override OnModelCreating
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
                throw new ArgumentNullException("modelBuilder");
            DbInterception.Add(new FilterInterceptor());
            DbInterception.Add(new YeKeInterceptor());
            modelBuilder.Ignore<BaseEntity>();

            modelBuilder.Configurations.AddFromAssembly(typeof(SettingConfig).GetTypeInfo().Assembly);
            LoadEntities(typeof(User).GetTypeInfo().Assembly, modelBuilder, "Decision.DomainClasses.Entities");
        }

        #endregion

        #region AutoRegisterEntityType

        public void LoadEntities(Assembly asm, DbModelBuilder modelBuilder, string nameSpace)
        {
            var entityTypes = asm.GetTypes()
                .Where(type => type.BaseType != null &&
                               type.Namespace == nameSpace &&
                               type.BaseType == null)
                .ToList();

            var entityMethod = typeof(DbModelBuilder).GetMethod("Entity");
            entityTypes.ForEach(type => entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, new object[] { }));
        }
        #endregion

        #region AuditLogging
        public string CharacteristicOfLogedEntity
        {
            get { return _characteristicOfLogedEntity; }
        }
        public string AuditOldValue
        {
            get { return _oldValue; }
        }

        public string AuditNewValue
        {
            get { return _newValue; }
        }
        public Guid RecordedEntityKey
        {
            get { return _recordedEntitykey; }
        }
        #endregion
    }
}
