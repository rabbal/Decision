namespace Decision.Framework.Domain.Entities.Tracking
{

    /// <summary>
    /// This interface is implemented by entities which must be audited.
    /// Related properties automatically set when saving/updating <see cref="Entity"/> objects.
    /// </summary>
    public interface ITrackable : ICreationTracking, IModificationTracking
    {
    }

    /// <summary>
    /// Adds navigation properties to <see cref="ITrackable"/> interface for user.
    /// </summary>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public interface ITrackable<TUser> : ITrackable, ICreationTracking<TUser>, IModificationTracking<TUser>
        where TUser : IEntity<long>
    {
    }
}
