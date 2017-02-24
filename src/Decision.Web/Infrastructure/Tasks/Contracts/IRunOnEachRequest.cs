namespace Decision.Web.Infrastructure.Tasks.Contracts
{
    public interface IRunOnEachRequest
    {
        void Execute();
        int Order { get; }
    }
}