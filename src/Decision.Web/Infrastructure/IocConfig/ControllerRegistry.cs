using StructureMap;

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