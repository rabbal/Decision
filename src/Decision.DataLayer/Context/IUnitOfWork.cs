using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using RefactorThis.GraphDiff;

namespace Decision.DataLayer.Context
{
    public interface IUnitOfWork : IDisposable
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class;
        void MarkAsSoftDeleted<TEntity>(TEntity entity) where TEntity : class, ISoftDeletable;
        void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class;


        IList<T> GetRows<T>(string sql, params object[] parameters) where T : class;
        void ExecuteSqlCommand(string query);
        void ExecuteSqlCommand(string query, params object[] parameters);

        void BulkInsert<T>(IEnumerable<T> data);
        void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        T Update<T>(T entity, Expression<Func<IUpdateConfiguration<T>, object>> mapping) where T : class, new();

        void ForceDatabaseInitialize();

        int SaveChanges();
        int SaveChanges(bool invalidateCacheDependencies);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(bool invalidateCacheDependencies, CancellationToken cancellationToken = new CancellationToken());

        void DisableFilter(string filterName);
        void EnableRowLevelSecurity();
        void EnableFilter(string filterName);
        void EnableFilter(string filterName, object parameterValue);
        void ChangeFilterParameterValue(string filterName, object parameterValue);
        void DisableAllFilters();
        void EnableAllFilters();
    }
}







