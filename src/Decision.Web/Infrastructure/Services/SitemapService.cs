using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Decision.Framework.Logging;
using Decision.Framework.MvcToolkit.Sitemap;
using Decision.Web.Infrastructure.Constants;
using Decision.Web.Infrastructure.Services.Contracts;

namespace Decision.Web.Infrastructure.Services
{
    /// <summary>
    ///     Generates sitemap XML for the current site.
    /// </summary>
    public class SitemapService : SitemapGenerator, ISitemapService
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SitemapService" /> class.
        /// </summary>
        /// <param name="cacheService">The cache service.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="urlHelper">The URL helper.</param>
        public SitemapService(
            ICacheService cacheService,
            ILogger loggingService,
            UrlHelper urlHelper)
        {
            this.cacheService = cacheService;
            this.loggingService = loggingService;
            this.urlHelper = urlHelper;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Gets the sitemap XML for the current site. If an index of null is passed and there are more than 25,000
        ///     sitemap nodes, a sitemap index file is returned (A sitemap index file contains links to other sitemap files
        ///     and is a way of splitting up your sitemap into separate files). If an index is specified, a standard
        ///     sitemap is returned for the specified index parameter. See http://www.sitemaps.org/protocol.html
        /// </summary>
        /// <param name="index">
        ///     The index of the sitemap to retrieve. <c>null</c> if you want to retrieve the root
        ///     sitemap or sitemap index document, depending on the number of sitemap nodes.
        /// </param>
        /// <returns>The sitemap XML for the current site or <c>null</c> if the sitemap index is out of range.</returns>
        public string GetSitemapXml(int? index = null)
        {
            // Here we are caching the entire set of sitemap documents. We cannot use OutputCacheAttribute because 
            // cache expiry could get out of sync if the number of sitemaps changes.
            var sitemapDocuments = this.cacheService.GetOrAdd(
                CacheSetting.SitemapNodes.Key,
                () =>
                {
                    var sitemapNodes = this.GetSitemapNodes();
                    return this.GetSitemapDocuments(sitemapNodes);
                },
                CacheSetting.SitemapNodes.SlidingExpiration);

            if (index.HasValue && ((index < 1) || (index.Value >= sitemapDocuments.Count)))
            {
                return null;
            }

            return sitemapDocuments[index.HasValue ? index.Value : 0];
        }

        #endregion

        #region Fields

        private readonly ICacheService cacheService;
        private readonly ILogger loggingService;
        private readonly UrlHelper urlHelper;

        #endregion

        #region Protected Methods

        /// <summary>
        ///     Gets a collection of sitemap nodes for the current site.
        ///     TODO: Add code here to create nodes to all your important sitemap URL's.
        ///     You may want to do this from a database or in code.
        /// </summary>
        /// <returns>A collection of sitemap nodes for the current site.</returns>
        protected virtual IReadOnlyCollection<SitemapNode> GetSitemapNodes()
        {
            var nodes = new List<SitemapNode>();

            nodes.Add(
                new SitemapNode(this.urlHelper.ActionAbsolute(MVC.Home.Index()))
                {
                    Priority = 1
                });
            nodes.Add(
                new SitemapNode(this.urlHelper.ActionAbsolute(MVC.Home.About()))
                {
                    Priority = 0.9
                });
            nodes.Add(
                new SitemapNode(this.urlHelper.ActionAbsolute(MVC.Home.Contact()))
                {
                    Priority = 0.9
                });

            // An example of how to add many pages into your sitemap.
            // foreach (int productId in myProductRepository.GetProductIds())
            // {
            //     nodes.Add(
            //        new SitemapNode(this.urlHelper.AbsoluteRouteUrl(ProductControllerRoute.GetProduct, new { id = productId }))
            //        {
            //            Frequency = SitemapFrequency.Weekly,
            //            LastModified = DateTime.Now,
            //            Priority = 0.8
            //        });
            // }

            return nodes;
        }

        protected override string GetSitemapUrl(int index)
        {
            return this.urlHelper.ActionAbsolute(MVC.Home.SitemapXml()).TrimEnd('/') + "?index=" + index;
        }

        protected override void LogWarning(Exception exception)
        {
            this.loggingService.Log(exception);
        }

        #endregion
    }
}