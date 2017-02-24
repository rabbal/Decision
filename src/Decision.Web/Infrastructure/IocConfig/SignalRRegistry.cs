using Microsoft.AspNet.SignalR;
using StructureMap;

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