using System;

namespace Decision.Framework.Domain.Entities.Tracking
{
    /// <summary>
    /// An entity can implement this interface if <see cref="DeletionDateTime"/> of this entity must be stored.
    /// <see cref="DeletionDateTime"/> is automatically set when deleting <see cref="Entity"/>.
    /// </summary>
    public interface IHasDeletionDateTime : ISoftDelete
    {
        DateTime? DeletionDateTime { get; set; }
    }

}
