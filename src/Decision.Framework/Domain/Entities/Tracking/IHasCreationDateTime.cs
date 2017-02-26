using System;

namespace Decision.Framework.Domain.Entities.Tracking
{
    /// <summary>
    /// An entity can implement this interface if <see cref="CreationDateTime"/> of this entity must be stored.
    /// <see cref="CreationDateTime"/> is automatically set when saving <see cref="Entity"/> to database.
    /// </summary>
    public interface IHasCreationDateTime
    {
        DateTime CreationDateTime { get; set; }
    }
}
