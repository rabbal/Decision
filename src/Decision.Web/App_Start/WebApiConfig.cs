using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using System.Web.Http.Routing;
using Decision.Framework.WebAPIToolkit.DelegatingHandlers;
using Decision.Framework.WebAPIToolkit.ExceptionHandling;
using Decision.Framework.WebAPIToolkit.Routing;
using Decision.Web.Infrastructure.IocConfig;
using FluentValidation.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Decision.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Services.Replace(
                typeof(IHttpControllerActivator),
                IoC.Resolve<StructureMapHttpControllerActivator>());

            config.Services.Replace(typeof(IFilterProvider),
                IoC.Resolve<WebApiFilterProvider>());

            // ignore iis host authentication . instead use tokenbase
            config.SuppressDefaultHostAuthentication();

            var responseWrapping = new ResponseWrappingHandler();
            config.MessageHandlers.Add(responseWrapping);

            config.Services.Add(typeof(IExceptionLogger), new ElmahExceptionLogger());

            config.Services.Replace(typeof(IExceptionHandler), new GenericTextExceptionHandler());

            SetSerializerSettings(config);

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;

            var constraintsResolver = new DefaultInlineConstraintResolver();
            constraintsResolver.ConstraintMap.Add(nameof(VersionConstraint), typeof
                (VersionConstraint));

            config.MapHttpAttributeRoutes(constraintsResolver);

            config.Services.Replace(typeof(IHttpControllerSelector), new NamespaceControllerSelector(config));

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
                );

            // config.EnableCors();

            FluentValidationModelValidatorProvider.Configure(config);

            //https://github.com/stefanprodan/WebApiThrottle
            var throttleHandler = new ThrottlingHandler
            {
                Policy = new ThrottlePolicy(1, 20, 200, 1500, 3000)
                {
                    IpThrottling = true
                }
            };

            config.MessageHandlers.Add(throttleHandler);

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            //config.EnableSystemDiagnosticsTracing();
        }

        private static void SetSerializerSettings(HttpConfiguration config)
        {
            var settings = config.Formatters.JsonFormatter.SerializerSettings;

            settings.Converters.Add(new StringEnumConverter());
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}