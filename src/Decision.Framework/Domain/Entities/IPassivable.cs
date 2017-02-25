namespace Decision.Framework.Domain.Entities
{
    public interface IPassivable
    {
        bool IsActive { get; set; }
    }
}
