using System;

namespace Decision.Framework.Domain.Entities.Tracking
{
    /// <summary>
    /// An entity can implement this interface if <see cref="LasModificationDateTime"/> of this entity must be stored.
    /// <see cref="LasModificationDateTime"/> is automatically set when updating <see cref="Entity"/>.
    /// </summary>
    public interface IHasModificationDateTime
    {
        DateTime? LasModificationDateTime { get; set; }
    }
}
