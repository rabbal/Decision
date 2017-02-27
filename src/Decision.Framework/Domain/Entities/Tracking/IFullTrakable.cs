namespace Decision.Framework.Domain.Entities.Tracking
{
    /// <summary>
    /// This interface ads <see cref="IDeletionTracking"/> to <see cref="ITrackable"/> for a fully audited entity.
    /// </summary>
    public interface IFullTrakable : ITrackable, IDeletionTracking
    {

    }
}