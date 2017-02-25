using System;

namespace Decision.Framework.Domain.Entities
{
    /// <summary>
    /// A shortcut of <see cref="IEntity{TKey}"/> for most used primary key type (<see cref="long"/>).
    /// </summary>
    public interface IEntity : IEntity<long>
    {
    }

    /// <summary>
    /// Defines interface for base entity type. All entities in the system must implement this interface.
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    public interface IEntity<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
        byte[] RowVersion { get; set; }
        bool IsTransient();
    }
}