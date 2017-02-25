using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Decision.Framework.Configuration;
using Decision.Web.Infrastructure.IocConfig;

namespace Decision.Web.Infrastructure.JsonWebTokenConfig
{
    public class AppOAuthOptions : OAuthAuthorizationServerOptions
    {
        
        public AppOAuthOptions(IAppJwtConfiguration configuration, IAppConfiguration appConfiguration)
        {
            // Configure the application for OAuth based flow
            var publicClientId = "self";

            // In production mode set AllowInsecureHttp = false
            AllowInsecureHttp = !appConfiguration.RquiredHttps; // TODO: Buy an SSL certificate!
            TokenEndpointPath = new PathString(configuration.TokenPath);
            AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(configuration.ExpirationMinutes);
            AccessTokenFormat = new AppJwtWriterFormat(this, configuration);
            Provider = IoC.Resolve<IOAuthAuthorizationServerProvider>();
            RefreshTokenProvider = IoC.Resolve<IAuthenticationTokenProvider>();
        }
    }
}