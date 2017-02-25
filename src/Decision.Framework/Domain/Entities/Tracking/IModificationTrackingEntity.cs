using System;

namespace Decision.Framework.Domain.Entities.Tracking
{
    public interface IModificationTrackingEntity : IModificationTrackingEntity<long>
    {
    }

    public interface IModificationTrackingEntity<TKey> : IEntity<TKey>, IModificationTracking
        where TKey : IEquatable<TKey>
    {
    }
}
