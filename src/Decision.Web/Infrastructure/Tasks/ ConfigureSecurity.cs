using Decision.Web.Infrastructure.Tasks.Contracts;

namespace Decision.Web.Infrastructure.Tasks
{
    public class ConfigureSecurity : IBootstrapperTask
    {
        public int Order => int.MaxValue;
        public void Execute()
        {
        }
    }
}