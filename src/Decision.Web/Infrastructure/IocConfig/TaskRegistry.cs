using Decision.Web.Infrastructure.Tasks.Contracts;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Decision.Web.Infrastructure.IocConfig
{
    public class TaskRegistry : Registry
    {
        public TaskRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.AddAllTypesOf<IRunOnEnd>();
                scan.AddAllTypesOf<IBootstrapperTask>();
                scan.AddAllTypesOf<IRunStartUp>();
                scan.AddAllTypesOf<IRunOnEachRequest>();
                scan.AddAllTypesOf<IRunOnError>();
                scan.AddAllTypesOf<IRunAfterEachRequest>();
            });
        }
    }
}