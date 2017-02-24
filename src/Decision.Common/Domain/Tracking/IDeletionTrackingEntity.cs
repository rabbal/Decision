using System;

namespace Decision.Common.Domain.Tracking
{
    public interface IDeletionTrackingEntity : IDeletionTrackingEntity<long>
    {
    }

    public interface IDeletionTrackingEntity<TKey> : IEntity<TKey>, IDeletionTracking
        where TKey : IEquatable<TKey>
    {
    }
}
