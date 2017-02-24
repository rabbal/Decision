namespace Decision.Web.Infrastructure.Tasks.Contracts
{
    public interface IRunAfterEachRequest
    {
        void Execute();
        int Order { get; }
    }
}