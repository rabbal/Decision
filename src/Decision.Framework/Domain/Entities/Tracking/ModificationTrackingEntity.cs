using System;

namespace Decision.Framework.Domain.Entities.Tracking
{

    /// <summary>
    /// A shortcut of <see cref="ModificationTrackingEntity{TKey}"/> for most used primary key type (<see cref="long"/>).
    /// </summary>
    public abstract class ModificationTrackingEntity : ModificationTrackingEntity<long>, IModificationTrackingEntity
    {
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IModificationTrackingEntity{TKey}"/>.
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    public abstract class ModificationTrackingEntity<TKey> : Entity<TKey>, IModificationTrackingEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public DateTime? LasModificationDateTime { get; set; }
        public string LastModifierIp { get; set; }
        public string LastModifierBrowserName { get; set; }
        public long? LastModifierUserId { get; set; }
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IModificationTracking{TUser}"/>.
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public abstract class ModificationTrackingEntity<TKey, TUser> : ModificationTrackingEntity<TKey>, IModificationTracking<TUser>
        where TKey : IEquatable<TKey>
        where TUser : IEntity<long>
    {
        public TUser LastModifierUser { get; set; }
    }
}
