namespace Decision.Common.Configuration
{
    public interface IAppConfiguration
    {
        string EncryptionKey { get; set; }
        string EncryptionPrefix { get; set; }
        string HostName { get; set; }
        string GoogleAnalyticsId { get; set; }
        string NameApiKey { get; set; }
        string SiteRootUrl { get; set; }
        bool RquiredHttps { get; set; }
        string SiteName { get; set; }
        string SiteShortName { get; set; }
        string FeedTitle { get; set; }
        string DoNotReplyEmail { get; set; }
    }
}