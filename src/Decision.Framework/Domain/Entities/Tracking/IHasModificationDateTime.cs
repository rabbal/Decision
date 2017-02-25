using System;

namespace Decision.Framework.Domain.Entities.Tracking
{
    /// <summary>
    /// An entity can implement this interface if <see cref="LastModifiedDateTime"/> of this entity must be stored.
    /// <see cref="LastModifiedDateTime"/> is automatically set when updating <see cref="Entity"/>.
    /// </summary>
    public interface IHasModificationDateTime
    {
        DateTimeOffset? LastModifiedDateTime { get; set; }
    }
}
