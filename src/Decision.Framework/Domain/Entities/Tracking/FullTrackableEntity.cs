using System;

namespace Decision.Framework.Domain.Entities.Tracking
{
    /// <summary>
    /// A shortcut of <see cref="FullTrackableEntity{TKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    public abstract class FullTrackableEntity : FullTrackableEntity<long>, IFullTrackableEntity
    {
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IFullTrackableEntity{TKey}"/>.
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    public abstract class FullTrackableEntity<TKey> : Entity<TKey>, IFullTrackableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public DateTimeOffset? CreatedDateTime { get; set; }
        public DateTimeOffset? LastModifiedDateTime { get; set; }
        public DateTimeOffset? DeletedDateTime { get; set; }
        public string CreatorIp { get; set; }
        public string LastModifierIp { get; set; }
        public string DeleterIp { get; set; }
        public string CreatorBrowserName { get; set; }
        public string LastModifierBrowserName { get; set; }
        public string DeleterBrowserName { get; set; }
        public long? LastModifierUserId { get; set; }
        public long? CreatorUserId { get; set; }
        public long? DeleterUserId { get; set; }
        public bool IsDeleted { get; set; }
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IFullTrackable{TUser}"/>.
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public abstract class FullTrackableEntity<TKey, TUser> : FullTrackableEntity<TKey>, IFullTrackable<TUser>
        where TKey : IEquatable<TKey>
        where TUser : IEntity<long>
    {
        public TUser CreatorUser { get; set; }
        public TUser LastModifierUser { get; set; }
        public TUser DeleterUser { get; set; }
    }
}
