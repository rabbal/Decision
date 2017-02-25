using System;

namespace Decision.Framework.Domain.Entities.Tracking
{
    public interface ITrackableEntity : ITrackableEntity<long>
    {
    }
    public interface ITrackableEntity<TKey> : IEntity<TKey>, ITrackable
        where TKey : IEquatable<TKey>
    {
    }
    public interface ITrackableEntity<TKey, TUser> : IEntity<TKey>, ITrackable<TUser>
        where TKey : IEquatable<TKey>
        where TUser : IEntity<long>
    {
    }
}