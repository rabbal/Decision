namespace Decision.Common.Domain.Tracking
{
    /// <summary>
    /// This interface is implemented by entities that is wanted to store modification information
    /// Properties are automatically set when updating the <see cref="IEntity"/>.
    /// </summary>
    public interface IModificationTracking : IHasModificationDateTime
    {
        string LastModifierIp { get; set; }
        string LastModifierBrowserName { get; set; }
        long? LastModifierUserId { get; set; }
    }
    /// <summary>
    /// Adds navigation properties to <see cref="IModificationTracking"/> interface for user.
    /// </summary>
    /// <typeparam name="TUser">Type of the user</typeparam>

    public interface IModificationTracking<TUser> : IModificationTracking
        where TUser : IEntity<long>
    {
        TUser LastModifierUser { get; set; }
    }
}
