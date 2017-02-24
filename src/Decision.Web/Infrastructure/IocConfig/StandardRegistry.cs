using StructureMap;
using StructureMap.Graph;

namespace Decision.Web.Infrastructure.IocConfig
{
    public class StandardRegistry : Registry
    {
        public StandardRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();

                scan.WithDefaultConventions();
            });
        }
    }
}