using Microsoft.AspNet.SignalR;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Decision.Web.Infrastructure.IocConfig
{
    public class SignalrRegistry : Registry
    {
        public SignalrRegistry()
        {
            For<IDependencyResolver>().Singleton().Add<StructureMapSignalRDependencyResolver>();
        }
    }
}