using System;

namespace Decision.Framework.Domain.Entities.Tracking
{
    /// <summary>
    /// An entity can implement this interface if <see cref="DeletedDateTime"/> of this entity must be stored.
    /// <see cref="DeletedDateTime"/> is automatically set when deleting <see cref="Entity"/>.
    /// </summary>
    public interface IHasDeletionDateTime : ISoftDelete
    {
        DateTimeOffset? DeletedDateTime { get; set; }
    }

}
