using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;

namespace Decision.Web.Infrastructure.IocConfig
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        #region Fields (1)

        private readonly IContainer _containerFactory;

        #endregion

        #region Ctor (1)

        public StructureMapDependencyResolver(Func<IContainer> containerFactory)
        {
            if (containerFactory == null)
            {
                throw new ArgumentNullException(nameof(containerFactory));
            }
            _containerFactory = containerFactory.Invoke();
        }

        #endregion

        #region Methods (2)

        public object GetService(Type serviceType)
        {
            if (serviceType == null) return null;

            return serviceType.IsAbstract || serviceType.IsInterface
                ? _containerFactory.TryGetInstance(serviceType)
                : _containerFactory.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _containerFactory.GetAllInstances(serviceType).Cast<object>();
        }

        #endregion
    }
}