using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RefactorThis.GraphDiff;

namespace Decision.DataLayer.Context
{
    public interface IUnitOfWork: IDisposable
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void MarkAsModified<TEntity>(TEntity entity) where TEntity : class;
        void MarkAsDetached<TEntity>(TEntity entity) where TEntity : class;
        void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class;
        void MarkAsAdded<TEntity>(TEntity entity) where TEntity : class;
        IList<T> GetRows<T>(string sql, params object[] parameters) where T : class;
        int SaveAllChanges(bool invalidateCacheDependencies = true);
        Task<int> SaveAllChangesAsync(bool invalidateCacheDependencies = true);
        void AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        void ForceDatabaseInitialize();
        void BulkInsertData<T>(IEnumerable<T> data);
        bool ValidateOnSaveEnabled { get; set; }
        bool ProxyCreationEnabled { get; set; }
        bool AutoDetectChangesEnabled { get; set; }
        bool ForceNoTracking { get; set; }
        T Update<T>(T entity,
            Expression<Func<IUpdateConfiguration<T>, object>> mapping) where T : class, new();
        Database Database { get; }
    }
}







