using System;
using Decision.Framework.Configuration;
using Decision.ServiceLayer.Interfaces.Identity;

using Decision.Web.Infrastructure.Services;
using Decision.Web.Infrastructure.Services.Contracts;
using StructureMap.Web;
using StructureMap.Configuration.DSL;

namespace Decision.Web.Infrastructure.IocConfig
{
    public class ServicesRegistry : Registry
    {
        public ServicesRegistry()
        {
            For<IAppConfiguration>().Singleton().Use(AppConfiguration.Config);

            For<IFeedService>().HybridHttpOrThreadLocalScoped().Use<FeedService>();
            For<IRobotsService>().HybridHttpOrThreadLocalScoped().Use<RobotsService>();
            For<IBrowserConfigService>().HybridHttpOrThreadLocalScoped().Use<BrowserConfigService>();
            For<IOpenSearchService>().HybridHttpOrThreadLocalScoped().Use<OpenSearchService>();
            For<ICacheService>().HybridHttpOrThreadLocalScoped().Use<CacheService>();
            For<IManifestService>().HybridHttpOrThreadLocalScoped().Use<ManifestService>();
            For<ISitemapPingerService>().HybridHttpOrThreadLocalScoped().Use<SitemapPingerService>();

            Policies.SetAllProperties(setterConvention =>
            {
                setterConvention.OfType<Func<IUserService>>();
                setterConvention.OfType<Func<ICurrentUser>>();
                setterConvention.OfType<ICurrentUser>();
                setterConvention.OfType<IAppConfiguration>();
                setterConvention.OfType<Func<IAppConfiguration>>();
            });

            Scan(a =>
            {
                a.AssemblyContainingType<IUserService>();
                a.WithDefaultConventions();
            });
        }
    }
}