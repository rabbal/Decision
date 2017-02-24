namespace Decision.Web.Infrastructure.JsonWebTokenConfig
{
    public interface IAppJwtConfiguration
    {
        int ExpirationMinutes { get; set; }
        string JwtAudience { get; set; }
        string JwtIssuer { get; set; }
        string JwtKey { get; set; }
        int RefreshTokenExpirationMinutes { get; set; }
        string TokenPath { get; set; }
    }
}