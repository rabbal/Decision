namespace NTierMvcFramework.Common.Infrastructure
{
    public interface IStartupTask
    {
        int Order { get; }
        void Execute();
    }
}