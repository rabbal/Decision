using Decision.DomainClasses.Identity;

namespace Decision.ServiceLayer.Interfaces.Identity
{
    public interface ICurrentUser
    {
        User User { get; }
        long Id { get; }
        string UserName { get; }
        string DisplayName { get; }
        bool IsAuthenticated { get; }
    }
}
