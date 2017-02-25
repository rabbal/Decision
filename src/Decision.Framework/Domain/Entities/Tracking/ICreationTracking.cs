namespace Decision.Framework.Domain.Entities.Tracking
{
    /// <summary>
    /// This interface is implemented by entities that is wanted to store creation information
    /// Creation time and creator user are automatically set when saving <see cref="Entity"/> to database.
    /// </summary>
    public interface ICreationTracking : IHasCreationDateTime
    {
        string CreatorIp { get; set; }
        string CreatorBrowserName { get; set; }
        long? CreatorUserId { get; set; }
    }
    /// <summary>
    /// Adds navigation properties to <see cref="ICreationTracking"/> interface for user.
    /// </summary>
    /// <typeparam name="TUser">Type of the user</typeparam>

    public interface ICreationTracking<TUser> : ICreationTracking
        where TUser : IEntity<long>
    {
        TUser CreatorUser { get; set; }
    }
}
