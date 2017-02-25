using System;

namespace Decision.Framework.Domain.Entities.Tracking
{
    /// <summary>
    /// An entity can implement this interface if <see cref="CreatedDateTime"/> of this entity must be stored.
    /// <see cref="CreatedDateTime"/> is automatically set when saving <see cref="Entity"/> to database.
    /// </summary>
    public interface IHasCreationDateTime
    {
        DateTimeOffset? CreatedDateTime { get; set; }
    }
}
