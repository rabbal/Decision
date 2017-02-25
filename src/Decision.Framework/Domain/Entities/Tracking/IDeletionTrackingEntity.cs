using System;

namespace Decision.Framework.Domain.Entities.Tracking
{
    public interface IDeletionTrackingEntity : IDeletionTrackingEntity<long>
    {
    }

    public interface IDeletionTrackingEntity<TKey> : IEntity<TKey>, IDeletionTracking
        where TKey : IEquatable<TKey>
    {
    }
}
