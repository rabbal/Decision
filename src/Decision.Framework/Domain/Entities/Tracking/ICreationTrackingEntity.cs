using System;

namespace Decision.Framework.Domain.Entities.Tracking
{
    public interface ICreationTrackingEntity : ICreationTrackingEntity<long>
    {
    }

    public interface ICreationTrackingEntity<TKey> : IEntity<TKey>, ICreationTracking
        where TKey : IEquatable<TKey>
    {
    }
}
