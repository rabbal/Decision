using System;

namespace Decision.Common.Domain.Tracking
{
    public interface ICreationTrackingEntity : ICreationTrackingEntity<long>
    {
    }

    public interface ICreationTrackingEntity<TKey> : IEntity<TKey>, ICreationTracking
        where TKey : IEquatable<TKey>
    {
    }
}
