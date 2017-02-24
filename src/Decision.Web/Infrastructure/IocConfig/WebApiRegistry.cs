using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Decision.Web.Infrastructure.JsonWebTokenConfig;
using StructureMap;

namespace Decision.Web.Infrastructure.IocConfig
{
    public class WebApiRegistry : Registry
    {
        public WebApiRegistry()
        {
            For<IAppJwtConfiguration>().Singleton().Use(() => AppJwtConfiguration.Config);
            For<IOAuthAuthorizationServerProvider>().Singleton().Use<AppOAuthProvider>();
            For<IAuthenticationTokenProvider>().Singleton().Use<RefreshTokenProvider>();
        }
    }
}