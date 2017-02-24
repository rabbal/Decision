namespace Decision.Web.Infrastructure.Tasks.Contracts
{
    public interface IRunOnEnd
    {
        int Order { get; }
        void Execute();
        
    }
}