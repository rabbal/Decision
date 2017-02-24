namespace Decision.Common.Domain
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}