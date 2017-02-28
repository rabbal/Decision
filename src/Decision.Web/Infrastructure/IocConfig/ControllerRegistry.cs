using StructureMap;
using StructureMap.Configuration.DSL;

namespace Decision.Web.Infrastructure.IocConfig
{
    public class ControllerRegistry : Registry
    {
        public ControllerRegistry()
        {
            Scan(scan => { scan.WithDefaultConventions(); });
        }
    }
}