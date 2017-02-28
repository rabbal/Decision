namespace Decision.ServiceLayer.Interfaces.Identity
{
    public interface ISecurityService
    {
        string GetSha256Hash(string input);
    }
}