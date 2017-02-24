namespace Decision.Web.Infrastructure.Tasks.Contracts
{
    public interface IBootstrapperTask
    {
        void Execute();
        int Order { get; }
    }
}