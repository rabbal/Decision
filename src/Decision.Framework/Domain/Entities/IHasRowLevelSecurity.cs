namespace Decision.Framework.Domain.Entities
{
    public interface IHasRowLevelSecurity
    {
        long UserId { get; set; }
    }
}