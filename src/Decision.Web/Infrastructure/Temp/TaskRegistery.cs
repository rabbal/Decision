using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.ServiceLayer.Contracts;
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.EFServiecs;
using Decision.ServiceLayer.EFServiecs.Users;
using StructureMap.Configuration.DSL;

namespace Decision.IocConfig
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
