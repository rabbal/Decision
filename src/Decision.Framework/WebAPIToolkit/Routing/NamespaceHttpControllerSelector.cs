using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Decision.Framework.WebAPIToolkit.Routing
{
    public class NamespaceHttpControllerSelector : IHttpControllerSelector
    {
        private const string NamespaceKey = "namespace";
        private const string ControllerKey = "controller";

        private readonly HttpConfiguration _configuration;
        private readonly Lazy<Dictionary<string, HttpControllerDescriptor>> _controllers;
        private readonly HashSet<string> _duplicates;

        public NamespaceHttpControllerSelector(HttpConfiguration config)
        {
            _configuration = config;
            _duplicates = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            _controllers = new Lazy<Dictionary<string, HttpControllerDescriptor>>(InitializeControllerDictionary);
        }

        private Dictionary<string, HttpControllerDescriptor> InitializeControllerDictionary()
        {
            var dictionary = new Dictionary<string, HttpControllerDescriptor>(StringComparer.OrdinalIgnoreCase);

            // Create a lookup table where key is "namespace.controller". The value of "namespace" is the last
            // segment of the full namespace. For example:
            // MyApplication.Controllers.V1.ProductsController => "V1.Products"
            var assembliesResolver = _configuration.Services.GetAssembliesResolver();
            var controllersResolver = _configuration.Services.GetHttpControllerTypeResolver();

            var controllerTypes = controllersResolver.GetControllerTypes(assembliesResolver);

            foreach (var t in controllerTypes)
            {
                var segments = t.Namespace.Split(Type.Delimiter);

                // For the dictionary key, strip "Controller" from the end of the type name.
                // This matches the behavior of DefaultHttpControllerSelector.
                var controllerName = t.Name.Remove(t.Name.Length - DefaultHttpControllerSelector.ControllerSuffix.Length);

                var key = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", segments[segments.Length - 1], controllerName);

                // Check for duplicate keys.
                if (dictionary.Keys.Contains(key))
                {
                    _duplicates.Add(key);
                }
                else
                {
                    dictionary[key] = new HttpControllerDescriptor(_configuration, t.Name, t);
                }
            }

            // Remove any duplicates from the dictionary, because these create ambiguous matches. 
            // For example, "Foo.V1.ProductsController" and "Bar.V1.ProductsController" both map to "v1.products".
            foreach (var s in _duplicates)
            {
                dictionary.Remove(s);
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

        public HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var routeData = request.GetRouteData();
            if (routeData == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            var namespaceName = GetRouteVariable<string>(routeData, NamespaceKey);
            if (namespaceName == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var controllerName = GetRouteVariable<string>(routeData, ControllerKey);
            if (controllerName == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var key = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", namespaceName, controllerName);

            HttpControllerDescriptor controllerDescriptor;
            if (_controllers.Value.TryGetValue(key, out controllerDescriptor))
            {
                return controllerDescriptor;
            }

            if (_duplicates.Contains(key))
            {
                throw new HttpResponseException(
                    request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        "Multiple controllers were found that match this request."));
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
        {
            return _controllers.Value;
        }
    }
}