using System.Threading.Tasks;
using System.Web.Mvc;
using Decision.Framework.Logging;
using Decision.Web.Infrastructure.Services.Contracts;

namespace Decision.Web.Infrastructure.Services
{
    public class SitemapPingerService : ISitemapPingerService
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SitemapPingerService" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="urlHelper">The URL helper.</param>
        public SitemapPingerService(
            ILogger loggingService,
            UrlHelper urlHelper)
        {
            this.loggingService = loggingService;
            this.urlHelper = urlHelper;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Send (or 'ping') the URL of this sites sitemap.xml file to search engines like Google, Bing and Yahoo,
        ///     This method should be called each time the sitemap changes. Google says that 'We recommend that you
        ///     resubmit a Sitemap no more than once per hour.' The way we 'ping' our sitemap to search engines is
        ///     actually an open standard See
        ///     http://www.sitemaps.org/protocol.html#submit_ping
        ///     You can read the sitemap ping documentation for the top search engines below:
        ///     Google - http://googlewebmastercentral.blogspot.co.uk/2014/10/best-practices-for-xml-sitemaps-rssatom.html
        ///     Bing - http://www.bing.com/webmaster/help/how-to-submit-sitemaps-82a15bd4.
        ///     Yahoo - https://developer.yahoo.com/search/siteexplorer/V1/ping.html
        /// </summary>
#if Release
        public async Task PingSearchEnginesAsync()
        {
            foreach (var url in SitemapPingLocations.Select(sitemapPingLocation => sitemapPingLocation +
                  this.urlHelper.Encode(this.urlHelper.AbsoluteRouteUrl(HomeControllerRoute.GetSitemapXml))))
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode) continue;

                    var exception = new HttpRequestException(string.Format(
                        CultureInfo.InvariantCulture,
                        "Pinging search engine {0}. Response status code does not indicate success: {1} ({2}).",
                        url,
                        (int)response.StatusCode,
                        response.ReasonPhrase));
                    this.loggingService.Log(exception);
                }
            }
        }
#else
        public Task PingSearchEnginesAsync()
        {
            return Task.FromResult<object>(null);
        }
#endif

        #endregion

        #region Fields

        /// <summary>
        ///     The URL's provided by search engines where we can send the location of our sitemap.
        /// </summary>
        private static readonly string[] SitemapPingLocations =
        {
            // Google
            "https://www.google.com/ping?sitemap=",
            // Bing and Yahoo share the same sitemap ping URL.
            "http://www.bing.com/ping?sitemap="
        };

        private readonly ILogger loggingService;
        private readonly UrlHelper urlHelper;

        #endregion
    }
}