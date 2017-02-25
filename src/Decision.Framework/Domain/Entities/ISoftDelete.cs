namespace Decision.Framework.Domain.Entities
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}