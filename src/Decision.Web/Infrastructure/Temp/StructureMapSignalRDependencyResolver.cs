using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using StructureMap;

namespace Decision.IocConfig
{
    public class StructureMapSignalRDependencyResolver : DefaultDependencyResolver
    {
        private readonly IContainer _container;

        public StructureMapSignalRDependencyResolver(IContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            _container = container;
        }

        public override object GetService(Type serviceType)
        {
            if (serviceType == null)
                return null;

            var service = base.GetService(serviceType);
            if (service != null) return service;

            return (!serviceType.IsAbstract && !serviceType.IsInterface && serviceType.IsClass)
                ? _container.GetInstance(serviceType)
                : _container.TryGetInstance(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>().Concat(base.GetServices(serviceType));
        }
    }
}
