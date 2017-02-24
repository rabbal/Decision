
using System.Runtime.Serialization;
using System.Web.Mvc;
using Decision.Common.Controller;
using StructureMap.Configuration.DSL;
using System.Runtime.Serialization.Formatters.Binary;
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.EFServiecs.Users;
using Decision.ServiceLayer.Contracts.Users;

namespace Decision.IocConfig
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
