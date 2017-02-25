using StructureMap.Configuration.DSL;

namespace Decision.Web.Infrastructure.Temp
{
    public class TaskRegistry : Registry
    {
        public TaskRegistry()
        {
            Scan(scan =>
            {
                scan.AddAllTypesOf<IRunAtInit>();
                scan.AddAllTypesOf<IRunAtStartUp>();
                scan.AddAllTypesOf<IRunOnEachRequest>();
                scan.AddAllTypesOf<IRunOnError>();
                scan.AddAllTypesOf<IRunAfterEachRequest>();
            });
        }
    }
}
