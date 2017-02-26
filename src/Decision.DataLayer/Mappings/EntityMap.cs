using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.Framework.Domain.Entities;

namespace Decision.DataLayer.Mappings
{
    public abstract class EntityMap<TEntity, TKey> : EntityTypeConfiguration<TEntity>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>

    {
        protected EntityMap()
        {
            Property(a => a.RowVersion).IsRowVersion();
        }
    }
}
