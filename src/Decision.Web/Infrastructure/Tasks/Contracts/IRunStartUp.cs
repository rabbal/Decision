namespace Decision.Web.Infrastructure.Tasks.Contracts
{
    public interface IRunStartUp
    {
        void Execute();
        int Order { get; }
    }
}