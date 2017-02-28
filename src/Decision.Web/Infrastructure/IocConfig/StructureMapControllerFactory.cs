using System;
using System.Web.Mvc;
using System.Web.Routing;
using Decision.Framework.Logging;
using Decision.Web.Controllers;
using StructureMap;


namespace Decision.Web.Infrastructure.IocConfig
{
    /// <summary>
    ///     more info :http://www.dotnettips.info/post/1748
    /// </summary>
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        private readonly IContainer _container;
        public StructureMapControllerFactory(IContainer container)
        {
            _container = container;
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType != null)
            {
                var controller = _container.GetInstance(controllerType) as Controller;
                if (controller != null)
                {
                    controller.TempDataProvider = _container.GetInstance<ITempDataProvider>();
                }
                return controller;
            }

            var url = requestContext.HttpContext.Request.RawUrl;
            _container.GetInstance<ILogger>().Log(new InvalidOperationException($"Page not found: {url}"));

            //requestContext.RouteData.Values["controller"] = MVC.Search.Name;
            //requestContext.RouteData.Values["action"] = MVC.Search.ActionNames.Index;
            //requestContext.RouteData.Values["term"] = url.GetPostSlug().Replace("-", " ");

            var searchController = _container.GetInstance(typeof(HomeController)) as Controller;
            searchController.TempDataProvider = _container.GetInstance<ITempDataProvider>();

            return searchController;
        }
    }
}