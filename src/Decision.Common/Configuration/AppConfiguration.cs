using System;
using System.Configuration;
using System.Web;
using NTierMvcFramework.Common.Utility;

namespace NTierMvcFramework.Common.Configuration
{
    public class AppConfiguration : ConfigurationSection, IAppConfiguration
    {

        #region Static Members
        public static AppConfiguration Config
            => ConfigurationManager.GetSection(ConfigSectionName) as AppConfiguration;

        #endregion

        #region Constants

        private const string ConfigSiteRootUrl = "siteRootUrl";
        private const string ConfigSectionName = "appConfiguration";
        private const string ConfigNameApiKey = "nameApiKey";
        private const string ConfigGoogleAnalyticsId = "googleAnalyticsId";
        private const string ConfigHostName = "hostName";
        private const string ConfigEncryptionKey = "encryptionKey";
        private const string ConfigEncryptionPrefix = "encryptionPrefix";
        private const string ConfigRequiredHttps = "requiredHttps";
        private const string ConfigSiteName = "siteName";
        private const string ConfigSiteShortName = "siteShortName";
        private const string ConfigSiteFeedTitle = "feedTitle";
        private const string ConfigDotNotReplyEmail = "dotNotReplyEmail";

        #endregion

        #region Properties
        public Func<HttpContextBase> HttpContext { get; set; }

        [ConfigurationProperty(ConfigSiteName, IsRequired = true)]
        public string SiteName
        {
            get { return ((string)this[ConfigSiteName]).CorrectRtl(); }
            set { this[ConfigSiteName] = value; }
        }

        [ConfigurationProperty(ConfigDotNotReplyEmail, IsRequired = true)]
        public string DoNotReplyEmail
        {
            get { return ((string)this[ConfigDotNotReplyEmail]); }
            set { this[ConfigDotNotReplyEmail] = value; }
        }

        [ConfigurationProperty(ConfigSiteShortName, IsRequired = true)]
        public string SiteShortName
        {
            get { return ((string)this[ConfigSiteShortName]).CorrectRtl(); }
            set { this[ConfigSiteShortName] = value; }
        }


        [ConfigurationProperty(ConfigRequiredHttps, IsRequired = false)]
        public bool RquiredHttps
        {
            get { return (bool?)this[ConfigRequiredHttps] ?? false; }

            set { this[ConfigRequiredHttps] = value; }
        }

        [ConfigurationProperty(ConfigEncryptionKey, IsRequired = true)]
        public string EncryptionKey
        {
            get { return (string)this[ConfigEncryptionKey]; }

            set { this[ConfigEncryptionKey] = value; }
        }

        [ConfigurationProperty(ConfigEncryptionPrefix, IsRequired = true)]
        public string EncryptionPrefix
        {
            get { return (string)this[ConfigEncryptionPrefix]; }

            set { this[ConfigEncryptionPrefix] = value; }
        }

        [ConfigurationProperty(ConfigHostName, IsRequired = true)]
        public string HostName
        {
            get { return (string)this[ConfigHostName]; }

            set { this[ConfigHostName] = value; }
        }

        [ConfigurationProperty(ConfigGoogleAnalyticsId, IsRequired = true)]
        public string GoogleAnalyticsId
        {
            get { return (string)this[ConfigGoogleAnalyticsId]; }

            set { this[ConfigGoogleAnalyticsId] = value; }
        }

        [ConfigurationProperty(ConfigNameApiKey, IsRequired = true)]
        public string NameApiKey
        {
            get { return (string)this[ConfigNameApiKey]; }

            set { this[ConfigNameApiKey] = value; }
        }

        [ConfigurationProperty(ConfigSiteRootUrl, IsRequired = false)]
        public string SiteRootUrl
        {
            get
            {
                return (string)this[ConfigSiteRootUrl] ??
                       HttpContext.Invoke().Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Invoke().Request.ApplicationPath;
            }

            set { this[ConfigSiteRootUrl] = value; }
        }

        [ConfigurationProperty(ConfigSiteFeedTitle, IsRequired = true)]
        public string FeedTitle
        {
            get { return ((string)this[ConfigSiteFeedTitle]).CorrectRtl(); }

            set { this[ConfigSiteFeedTitle] = value; }
        }

        #endregion
    }
}