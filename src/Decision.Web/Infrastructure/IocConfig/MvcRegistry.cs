using System;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Decision.Framework.MvcToolkit.Providers;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Decision.Web.Infrastructure.IocConfig
{
    public class MvcRegistry : Registry
    {
        public MvcRegistry()
        {
            For<BundleCollection>().Use(BundleTable.Bundles);
            For<RouteCollection>().Use(RouteTable.Routes);
            For<ViewEngineCollection>().Use(ViewEngines.Engines);
            For<IIdentity>().Use(() => GetIdentity());
            For<IPrincipal>().Use(() => GetPrincipal());
            For<HttpContextBase>().Use(() => new HttpContextWrapper(HttpContext.Current));
            For<HttpSessionStateBase>().Use(ctx => new HttpSessionStateWrapper(HttpContext.Current.Session));
            For<HttpServerUtilityBase>().Use(ctx => new HttpServerUtilityWrapper(HttpContext.Current.Server));

            For<HttpRequestBase>().Use(ctx => ctx.GetInstance<HttpContextBase>().Request);
            For<HttpResponseBase>().Use(ctx => ctx.GetInstance<HttpContextBase>().Response);
            For<HttpCachePolicyBase>().Use(ctx => ctx.GetInstance<HttpResponseBase>().Cache);
            For<HttpBrowserCapabilitiesBase>().Use(ctx => ctx.GetInstance<HttpRequestBase>().Browser);
            For<HttpFileCollectionBase>().Use(ctx => ctx.GetInstance<HttpRequestBase>().Files);
            For<HttpApplicationStateBase>().Use(ctx => ctx.GetInstance<HttpContextBase>().Application);
            For<RequestContext>().Use(ctx => ctx.GetInstance<HttpRequestBase>().RequestContext);

            For<VirtualPathProvider>().Use(ctx => HostingEnvironment.VirtualPathProvider);

            For<UrlHelper>().Use(ctx => new UrlHelper(ctx.GetInstance<RequestContext>()));
            For<IRemotingFormatter>().Use(a => new BinaryFormatter());
            For<ITempDataProvider>().Use<CookieTempDataProvider>();

            Policies.SetAllProperties(setterConvention =>
            {
                setterConvention.OfType<HttpContextBase>();
                setterConvention.OfType<Func<HttpContextBase>>();
            });
        }

        private static IIdentity GetIdentity()
        {
            if (HttpContext.Current != null && HttpContext.Current.User != null)
            {
                return HttpContext.Current.User.Identity;
            }
            return ClaimsPrincipal.Current != null ? ClaimsPrincipal.Current.Identity : null;
        }

        private static IPrincipal GetPrincipal()
        {
            if (HttpContext.Current != null && HttpContext.Current.User != null)
            {
                return HttpContext.Current.User;
            }
            return null;
        }
    }
}