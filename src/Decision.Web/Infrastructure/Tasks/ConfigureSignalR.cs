using Microsoft.AspNet.SignalR;
using Decision.Web.Infrastructure.Tasks.Contracts;
using StructureMap;

namespace Decision.Web.Infrastructure.Tasks
{
    public class ConfigureSignalR : IBootstrapperTask
    {
        private readonly IContainer _container;

        public ConfigureSignalR(IContainer container)
        {
            _container = container;
        }

        public int Order => int.MaxValue;
        public void Execute()
        {
            GlobalHost.DependencyResolver = _container.GetInstance<IDependencyResolver>();
        }
    }
}