using System;

namespace Decision.Framework.Domain.Entities.Tracking
{
    /// <summary>
    /// A shortcut of <see cref="CreationTrackingEntity{TKey}"/> for most used primary key type (<see cref="long"/>).
    /// </summary>
    public abstract class CreationTrackingEntity : CreationTrackingEntity<long>, ICreationTrackingEntity
    {
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="ICreationTrackingEntity{TKey}"/>.
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    public abstract class CreationTrackingEntity<TKey> : Entity<TKey>, ICreationTrackingEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public DateTimeOffset? CreatedDateTime { get; set; }
        public string CreatorIp { get; set; }
        public string CreatorBrowserName { get; set; }
        public long? CreatorUserId { get; set; }
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="ICreationTracking{TUser}"/>.
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public abstract class CreationTrackingEntity<TKey, TUser> : CreationTrackingEntity<TKey>, ICreationTracking<TUser>
        where TKey : IEquatable<TKey>
        where TUser : IEntity<long>
    {
        public TUser CreatorUser { get; set; }
    }
}
