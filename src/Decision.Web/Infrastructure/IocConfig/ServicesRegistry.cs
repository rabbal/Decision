using System;
using Decision.Common.Configuration;
using Decision.Services.EntityFramework.Common;
using Decision.Services.Interfaces.Common;
using Decision.Services.Interfaces.Security;
using Decision.Services.Interfaces.Users;
using Decision.Web.Infrastructure.Services;
using Decision.Web.Infrastructure.Services.Contracts;
using StructureMap;
using StructureMap.Web;

namespace Decision.Web.Infrastructure.IocConfig
{
    public class ServicesRegistry : Registry
    {
        public ServicesRegistry()
        {
            For<IDateTime>().Use<SystemDateTime>();
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
                setterConvention.OfType<Func<ITokenStoreService>>();
            });

            Scan(a =>
            {
                a.AssemblyContainingType<IUserService>();
                a.WithDefaultConventions();
            });
        }
    }
}