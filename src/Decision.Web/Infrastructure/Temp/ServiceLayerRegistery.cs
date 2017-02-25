using StructureMap.Configuration.DSL;

namespace Decision.Web.Infrastructure.Temp
{
    public class ServiceLayerRegistery : Registry
    {
        public ServiceLayerRegistery()
        {
            Policies.SetAllProperties(y =>
            {
                y.OfType<IActivityLogService>();
            });
            Scan(scanner =>
            {
                scanner.WithDefaultConventions();
                scanner.AssemblyContainingType<ApplicationUserManager>();

            });
        }
    }
}
