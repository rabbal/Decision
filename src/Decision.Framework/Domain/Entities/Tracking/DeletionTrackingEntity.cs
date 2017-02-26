using System;

namespace Decision.Framework.Domain.Entities.Tracking
{
    /// <summary>
    /// A shortcut of <see cref="DeletionTrackingEntity{TKey}"/> for most used primary key type (<see cref="long"/>).
    /// </summary>
    public abstract class DeletionTrackingEntity : DeletionTrackingEntity<long>, IDeletionTrackingEntity
    {
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IDeletionTrackingEntity{TKey}"/>.
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    public abstract class DeletionTrackingEntity<TKey> : Entity<TKey>, IDeletionTrackingEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletionDateTime { get; set; }
        public string DeleterIp { get; set; }
        public string DeleterBrowserName { get; set; }
        public long? DeleterUserId { get; set; }
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IDeletionTracking{TUser}"/>.
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public abstract class DeletionTrackingEntity<TKey, TUser> : DeletionTrackingEntity<TKey>, IDeletionTracking<TUser>
        where TKey : IEquatable<TKey>
        where TUser : IEntity<long>
    {
        public TUser DeleterUser { get; set; }
    }
}
