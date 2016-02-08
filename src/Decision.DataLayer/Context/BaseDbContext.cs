using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EFSecondLevelCache;
using EntityFramework.Filters;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Decision.DomainClasses.Configurations.Users;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.Users;
using Decision.Utility;

namespace Decision.DataLayer.Context
{
    public class BaseDbContext : IdentityDbContext
        <User, Role, Guid, UserLogin, UserRole, UserClaim>
    {

        #region Constrans
        protected const string ConcurrencyMessage = "مقادیر در سمت بانک اطلاعاتی تغییر کرده‌اند. لطفا صفحه را ریفرش کنید.";
        #endregion

        #region Ctor
        public BaseDbContext(string connectionString) : base(connectionString)
        {

        }
        #endregion

        #region Override OnModelCreating
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
                throw new ArgumentNullException(nameof(modelBuilder));

            DbInterception.Add(new FilterInterceptor());
            DbInterception.Add(new YeKeInterceptor());

            // for full text search  DbInterception.Add(new FtsInterceptor());
            modelBuilder.Ignore<BaseEntity>();

            modelBuilder.Configurations.AddFromAssembly(typeof(UserConfig).Assembly);
            LoadEntities(typeof(User).Assembly, modelBuilder, "Decision.DomainClasses.Entities");
        }

        #endregion

        #region RejectChanges
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

        #region SaveChanges
        public int SaveAllChanges(bool invalidateCacheDependencies = true, Guid? auditUserId = null)
        {
            if (auditUserId.HasValue)
                UpdateAuditFields(auditUserId.Value);
            var result = SaveChanges();
            if (!invalidateCacheDependencies) return result;
            var changedEntityNames = GetChangedEntityNames();
            new EFCacheServiceProvider().InvalidateCacheDependencies(changedEntityNames);
            return result;
        }
        public Task<int> SaveAllChangesAsync(bool invalidateCacheDependencies = true, Guid? auditUserId = null)
        {
            if (auditUserId.HasValue)
                UpdateAuditFields(auditUserId.Value);
            var result = SaveChangesAsync();
            if (!invalidateCacheDependencies) return result;
            var changedEntityNames = GetChangedEntityNames();
            new EFCacheServiceProvider().InvalidateCacheDependencies(changedEntityNames);

            return result;
        }

        #endregion

        #region UpdateAuditFields
        private void UpdateAuditFields(Guid auditUserId)
        {
            var auditUserIp = HttpContext.Current.Request.GetIp();
            var auditUserAgent = HttpContext.Current.Request.GetBrowser();
            var auditDate = DateTime.Now;

            foreach (var entry in this.ChangeTracker.Entries<BaseEntity>())
            {
                // Note: You must add a reference to assembly : System.Data.Entity
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Id = SequentialGuidGenerator.NewSequentialGuid();
                        entry.Entity.CreatedOn = auditDate;
                        entry.Entity.CreatedById = auditUserId;
                        entry.Entity.ModifiedOn = auditDate;
                        entry.Entity.Action = AuditAction.Create;
                        entry.Entity.ModifiedById = auditUserId;
                        entry.Entity.CreatorIp = auditUserIp;
                        entry.Entity.ModifierIp = auditUserIp;
                        entry.Entity.CreatorAgent = auditUserAgent;
                        entry.Entity.ModifierAgent = auditUserAgent;
                        entry.Entity.Version = 1;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedOn = auditDate;
                        entry.Entity.ModifiedById = auditUserId;
                        entry.Entity.ModifierIp = auditUserIp;
                        entry.Entity.ModifierAgent = auditUserAgent;
                        entry.Entity.Version = ++entry.Entity.Version;
                        entry.Entity.Action = entry.Entity.IsDeleted ? AuditAction.SoftDelete : AuditAction.Update;
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

        #region PrivateMethods
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

        private static void LoadEntities(Assembly asm, DbModelBuilder modelBuilder, string nameSpace)
        {
            var entityTypes = asm.GetTypes()
                .Where(type => type.BaseType != null &&
                               type.Namespace == nameSpace &&
                               type.BaseType == null)
                .ToList();

            entityTypes.ForEach(modelBuilder.RegisterEntityType);
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing); //فقط تعريف شده تا يك برك پوينت در اينجا قرار داده شود براي آزمايش فراخواني آن
        }
        #endregion

    }
}
