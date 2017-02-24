using System;
using System.Text;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;

namespace Decision.Web.Infrastructure.JsonWebTokenConfig
{
    public class AppJwtOptions : JwtBearerAuthenticationOptions
    {
        public AppJwtOptions(IAppJwtConfiguration config)
        {
            AuthenticationMode = AuthenticationMode.Active;
            AllowedAudiences = new[] {config.JwtAudience};

            IssuerSecurityTokenProviders = new[]
            {
                new SymmetricKeyIssuerSecurityTokenProvider(
                    config.JwtIssuer,
                    Convert.ToBase64String(Encoding.UTF8.GetBytes(config.JwtKey)))
            };
        }
    }
}