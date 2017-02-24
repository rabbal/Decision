using System.Configuration;

namespace Decision.Web.Infrastructure.JsonWebTokenConfig
{
    public class AppJwtConfiguration : ConfigurationSection, IAppJwtConfiguration
    {
        private const string ConfigExpirationMinutes = "expirationMinutes";
        private const string ConfigJwtAudience = "jwtAudience";
        private const string ConfigJwtIssuer = "jwtIssuer";
        private const string ConfigJwtKey = "jwtKey";
        private const string ConfigRefreshTokenExpirationMinutes = "refreshTokenExpirationMinutes";
        private const string ConfigSectionName = "appJwtConfiguration";
        private const string ConfigTokenPath = "tokenPath";

        public static AppJwtConfiguration Config
            => ConfigurationManager.GetSection(ConfigSectionName) as AppJwtConfiguration;

        [ConfigurationProperty(ConfigExpirationMinutes, IsRequired = true)]
        public int ExpirationMinutes
        {
            get { return (int) this[ConfigExpirationMinutes]; }
            set { this[ConfigExpirationMinutes] = value; }
        }


        [ConfigurationProperty(ConfigJwtAudience, IsRequired = true)]
        public string JwtAudience
        {
            get { return (string) this[ConfigJwtAudience]; }
            set { this[ConfigJwtAudience] = value; }
        }

        [ConfigurationProperty(ConfigJwtIssuer, IsRequired = true)]
        public string JwtIssuer
        {
            get { return (string) this[ConfigJwtIssuer]; }
            set { this[ConfigJwtIssuer] = value; }
        }

        [ConfigurationProperty(ConfigJwtKey, IsRequired = true)]
        public string JwtKey
        {
            get { return (string) this[ConfigJwtKey]; }
            set { this[ConfigJwtKey] = value; }
        }

        [ConfigurationProperty(ConfigRefreshTokenExpirationMinutes, IsRequired = true)]
        public int RefreshTokenExpirationMinutes
        {
            get { return (int) this[ConfigRefreshTokenExpirationMinutes]; }
            set { this[ConfigRefreshTokenExpirationMinutes] = value; }
        }

        [ConfigurationProperty(ConfigTokenPath, IsRequired = true)]
        public string TokenPath
        {
            get { return (string) this[ConfigTokenPath]; }
            set { this[ConfigTokenPath] = value; }
        }
    }
}