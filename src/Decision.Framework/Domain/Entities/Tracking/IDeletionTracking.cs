namespace Decision.Framework.Domain.Entities.Tracking
{

    /// <summary>
    /// This interface is implemented by entities which wanted to store deletion information such as (who , when ,..)
    /// </summary>
    public interface IDeletionTracking : IHasDeletionDateTime
    {
        string DeleterIp { get; set; }
        string DeleterBrowserName { get; set; }
        long? DeleterUserId { get; set; }
    }
    /// <summary>
    /// Adds navigation properties to <see cref="IDeletionTracking"/> interface for user.
    /// </summary>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public interface IDeletionTracking<TUser> : IDeletionTracking
        where TUser : IEntity<long>
    {
        TUser DeleterUser { get; set; }
    }
}
