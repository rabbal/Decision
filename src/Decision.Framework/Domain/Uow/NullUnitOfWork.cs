using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Decision.Framework.Domain.Entities;
using RefactorThis.GraphDiff;

namespace Decision.Framework.Domain.Uow
{
    public class NullUnitOfWork : IUnitOfWork
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void MarkAsSoftDeleted<TEntity>(TEntity entity) where TEntity : class, ISoftDelete
        {
            throw new NotImplementedException();
        }

        public void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
        {
            throw new NotImplementedException();
        }

        public void ExecuteSqlCommand(string query)
        {
            throw new NotImplementedException();
        }

        public void ExecuteSqlCommand(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void BulkInsert<T>(IEnumerable<T> data)
        {
            throw new NotImplementedException();
        }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public T Update<T>(T entity, Expression<Func<IUpdateConfiguration<T>, object>> mapping) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public void ForceDatabaseInitialize()
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public int SaveChanges(bool invalidateCacheDependencies)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(bool invalidateCacheDependencies, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void DisableFilter(string filterName)
        {
            throw new NotImplementedException();
        }

        public void EnableRowLevelSecurity()
        {
            throw new NotImplementedException();
        }

        public void EnableFilter(string filterName)
        {
            throw new NotImplementedException();
        }

        public void EnableFilter(string filterName, object parameterValue)
        {
            throw new NotImplementedException();
        }

        public void ChangeFilterParameterValue(string filterName, object parameterValue)
        {
            throw new NotImplementedException();
        }

        public void DisableAllFilters()
        {
            throw new NotImplementedException();
        }

        public void EnableAllFilters()
        {
            throw new NotImplementedException();
        }
    }
}
