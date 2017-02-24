using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using StructureMap;

namespace Decision.IocConfig
{
    public class StructureMapHttpControllerActivator : IHttpControllerActivator
    {
        private readonly IContainer _container;
        public StructureMapHttpControllerActivator(IContainer container)
        {
            _container = container;
        }

        public IHttpController Create(
                HttpRequestMessage request,
                HttpControllerDescriptor controllerDescriptor,
                Type controllerType)
        {
            var nestedContainer = _container.GetNestedContainer();
            request.RegisterForDispose(nestedContainer);
            return (IHttpController)nestedContainer.GetInstance(controllerType);
        }
    }
}