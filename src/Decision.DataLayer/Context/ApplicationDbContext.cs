using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using EntityFramework.BulkInsert.Extensions;
using NTierMvcFramework.Common.Extensions;
using NTierMvcFramework.Common.Utility;
using NTierMvcFramework.DataLayer.Interceptors;
using NTierMvcFramework.DomainClasses.Common;
using NTierMvcFramework.DomainClasses.Users;
using RefactorThis.GraphDiff;

namespace NTierMvcFramework.DataLayer.Context
{
    public class ApplicationDbContext : BaseDbContext, IUnitOfWork
    {
        #region Constants
        private const string ConnectionStringName = "DefaultConnection";
        private const string DefaultAuditUserName = "سیستم";

        #endregion

        #region Properties
        public HttpContextBase HttpContext { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        #endregion

        #region Constructor
        public ApplicationDbContext()
           : base(ConnectionStringName)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        #endregion

        #region Public Methods
        public T Update<T>(T entity, Expression<Func<IUpdateConfiguration<T>, object>> mapping)
            where T : class, new()
        {
            return this.UpdateGraph(entity, mapping);
        }
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void MarkAsModified<TEntity>(TEntity entity) where TEntity : class
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

        #region StoredProcedures
        //[DbFunction("MyContext", "CustomersByZipCode")]
        //public IQueryable<Customer> CustomersByZipCode(string zipCode)
        //{
        //    var zipCodeParameter = zipCode != null ?
        //        new ObjectParameter("ZipCode", zipCode) :
        //        new ObjectParameter("ZipCode", typeof(string));

        //    return ((IObjectContextAdapter)this).ObjectContext
        //        .CreateQuery<Customer>(
        //            string.Format("[{0}].{1}", GetType().Name,
        //                "[CustomersByZipCode](@ZipCode)"), zipCodeParameter);
        //}

        //public ObjectResult<Customer> GetCustomersByName(string name)
        //{
        //    var nameParameter = name != null ?
        //        new ObjectParameter("Name", name) :
        //        new ObjectParameter("Name", typeof(string));

        //    return ((IObjectContextAdapter)this).ObjectContext.
        //        ExecuteFunction<Customer>("GetCustomersByName", nameParameter);
        //}
        #endregion

        #endregion

        #region Protected Methods
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
                throw new ArgumentNullException(nameof(modelBuilder));

            //modelBuilder.Conventions.Add(new CodeFirstStoreFunctions.FunctionsConvention<BaseDbContext>("dbo"));

            DbInterception.Add(new YeKeInterceptor());

            modelBuilder.Ignore<Entity>();
            modelBuilder.Ignore<AuditableEntity>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());

        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing); //ÝÞØ ÊÚÑíÝ ÔÏå ÊÇ íß ÈÑß æíäÊ ÏÑ ÇíäÌÇ ÞÑÇÑ ÏÇÏå ÔæÏ ÈÑÇí ÂÒãÇíÔ ÝÑÇÎæÇäí Âä
        }
        protected override void PerformPreSaveActions()
        {
            SetIdOfEntities();
            SetRowIdOfEntities();
            UpdateAuditFields();
        }
        #endregion

        #region Private Methods
        private void UpdateAuditFields()
        {
            var httpContext = HttpContext;

            if (httpContext?.User == null) return;

            var auditUserName = httpContext.User.Identity.Name;
            var auditUserIp = httpContext.Request.GetIp();
            var auditDate = DateTime.Now;

            if (string.IsNullOrWhiteSpace(auditUserName))
                auditUserName = DefaultAuditUserName;


            var entries = ChangeTracker.Entries<IAuditableEntity>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .ToList();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedOn = auditDate;
                    entry.Entity.CreatedBy = auditUserName;
                    entry.Entity.LastModifiedOn = auditDate;
                    entry.Entity.LastModifiedBy = auditUserName;
                    entry.Entity.CreatorIp = auditUserIp;
                    entry.Entity.LastModifierIp = auditUserIp;
                }
                else
                {
                    Entry(entry.Entity).Property(x => x.CreatedBy).IsModified = false;
                    Entry(entry.Entity).Property(x => x.CreatedOn).IsModified = false;
                    Entry(entry.Entity).Property(x => x.CreatorIp).IsModified = false;
                    entry.Entity.LastModifiedOn = auditDate;
                    entry.Entity.LastModifiedBy = auditUserName;
                    entry.Entity.LastModifierIp = auditUserIp;
                }
            }
        }
        private void SetIdOfEntities()
        {
            var entries = ChangeTracker.Entries<IHaveGuidKey>()
                .Where(a => a.State == EntityState.Added)
                .ToList();

            foreach (var entry in entries)
            {
                entry.Entity.Id = SequentialGuidGenerator.NewSequentialGuid();
            }
        }
        private void SetRowIdOfEntities()
        {
            foreach (var entry in ChangeTracker.Entries<IEntity>().Where(a => a.State == EntityState.Added))
            {
                entry.Entity.RowId = SequentialGuidGenerator.NewSequentialGuid();
            }
        }
        #endregion
    }
}