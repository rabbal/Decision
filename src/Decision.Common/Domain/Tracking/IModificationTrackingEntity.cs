using System;

namespace Decision.Common.Domain.Tracking
{
    public interface IModificationTrackingEntity : IModificationTrackingEntity<long>
    {
    }

    public interface IModificationTrackingEntity<TKey> : IEntity<TKey>, IModificationTracking
        where TKey : IEquatable<TKey>
    {
    }
}
