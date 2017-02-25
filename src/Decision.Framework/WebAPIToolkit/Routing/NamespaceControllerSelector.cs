using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Decision.Framework.WebAPIToolkit.Routing
{
    public class NamespaceControllerSelector : IHttpControllerSelector
    {
        private readonly HttpConfiguration _configuration;
        private readonly Lazy<Dictionary<string, HttpControllerDescriptor>> _controllers;
        public NamespaceControllerSelector(HttpConfiguration config)
        {
            _configuration = config;
            _controllers = new Lazy<Dictionary<string,
                HttpControllerDescriptor>>(InitializeControllerDictionary);
        }
        public HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var routeData = request.GetRouteData();
            if (routeData == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var controllerName = GetControllerName(routeData);
            if (controllerName == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var version = GetVersion(routeData);
            if (version == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var controllerKey = string.Format(CultureInfo.InvariantCulture, "{0}.{1}",
                version, controllerName);
            HttpControllerDescriptor controllerDescriptor;
            if (_controllers.Value.TryGetValue(controllerKey, out controllerDescriptor))
            {
                return controllerDescriptor;
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }
        public IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
        {
            return _controllers.Value;
        }
        private Dictionary<string, HttpControllerDescriptor> InitializeControllerDictionary()
        {
            var dictionary = new Dictionary<string,
                HttpControllerDescriptor>(StringComparer.OrdinalIgnoreCase);
            var assembliesResolver = _configuration.Services.GetAssembliesResolver();
            var controllersResolver = _configuration.Services.GetHttpControllerTypeResolver();
            var controllerTypes = controllersResolver.GetControllerTypes(assembliesResolver);
            foreach (var controllerType in controllerTypes)
            {
                var segments = controllerType.Namespace.Split(Type.Delimiter);
                var controllerName =
                    controllerType.Name.Remove(controllerType.Name.Length -
                                               DefaultHttpControllerSelector.ControllerSuffix.Length);
                var controllerKey = string.Format(CultureInfo.InvariantCulture, "{0}.{1}",
                    segments[segments.Length - 1], controllerName);

                if (!dictionary.Keys.Contains(controllerKey))
                {
                    dictionary[controllerKey] = new HttpControllerDescriptor(_configuration,
                        controllerType.Name,
                        controllerType);
                }
                
            }
            return dictionary;
        }
        private static T GetRouteVariable<T>(IHttpRouteData routeData, string name)
        {
            object result;
            if (routeData.Values.TryGetValue(name, out result))
            {
                return (T)result;
            }
            return default(T);
        }
        private static string GetControllerName(IHttpRouteData routeData)
        {
            var subroute = routeData.GetSubRoutes().FirstOrDefault();
            var dataTokenValue = subroute?.Route.DataTokens.First().Value;
            var controllerName =
                ((HttpActionDescriptor[])dataTokenValue)?.First().ControllerDescriptor.ControllerName.Replace("Controller", string.Empty);
            return controllerName;
        }
        private static string GetVersion(IHttpRouteData routeData)
        {
            var subRouteData = routeData.GetSubRoutes().FirstOrDefault();
            return subRouteData == null ? null : GetRouteVariable<string>(subRouteData, "version");
        }
    }
}