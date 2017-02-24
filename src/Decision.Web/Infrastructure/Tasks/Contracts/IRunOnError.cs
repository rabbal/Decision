namespace Decision.Web.Infrastructure.Tasks.Contracts
{
    public interface IRunOnError
    {
        void Execute();
        int Order { get; }
    }
}