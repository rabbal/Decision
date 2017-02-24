using System;

namespace Decision.Common.Domain.Tracking
{
    /// <summary>
    /// This interface is implemented by entities which must be tracked.
    /// Related properties automatically set when saving/updating/deleting <see cref="Entity"/> objects.
    /// </summary>
    public interface IFullTrackableEntity : IFullTrackableEntity<long>
    {
    }

    public interface IFullTrackableEntity<TKey> : IEntity<TKey>, IFullTrakable
        where TKey : IEquatable<TKey>
    {
    }
}
