using System;

namespace Decision.Framework.Domain.Entities.Tracking
{
    /// <summary>
    /// A shortcut of <see cref="TrackableEntity{TKey}"/> for most used primary key type (<see cref="long"/>).
    /// </summary>
    public abstract class TrackableEntity : TrackableEntity<long>, ITrackableEntity
    {
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="ITrackableEntity{TKey}"/>.
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    public abstract class TrackableEntity<TKey> : Entity<TKey>, ITrackableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public DateTime CreationDateTime { get; set; }
        public DateTime? LasModificationDateTime { get; set; }
        public string CreatorIp { get; set; }
        public string LastModifierIp { get; set; }
        public string CreatorBrowserName { get; set; }
        public string LastModifierBrowserName { get; set; }
        public long? LastModifierUserId { get; set; }
        public long? CreatorUserId { get; set; }
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="ITrackable{TUser}"/>.
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public abstract class TrackableEntity<TKey, TUser> : TrackableEntity<TKey>, ITrackable<TUser>
        where TKey : IEquatable<TKey>
        where TUser : IEntity<long>
    {
        public TUser CreatorUser { get; set; }
        public TUser LastModifierUser { get; set; }
    }
}