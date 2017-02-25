namespace Decision.Framework.Domain.Entities.Tracking
{
    /// <summary>
    /// This interface ads <see cref="IDeletionTracking"/> to <see cref="ITrackable"/> for a fully audited entity.
    /// </summary>
    public interface IFullTrakable : ITrackable, IDeletionTracking
    {

    }

    /// <summary>
    /// Adds navigation properties to <see cref="IFullTrakable"/> interface for user.
    /// </summary>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public interface IFullTrackable<TUser> : IFullTrakable, ITrackable<TUser>, IDeletionTracking<TUser>
        where TUser : IEntity<long>
    {

    }
}
