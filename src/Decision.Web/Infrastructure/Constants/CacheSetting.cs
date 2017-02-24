using System;

namespace Decision.Web.Infrastructure.Constants
{
    public static class CacheSetting
    {
        public static class SitemapNodes
        {
            public const string Key = nameof(SitemapNodes);
            public static readonly TimeSpan SlidingExpiration = TimeSpan.FromDays(1);
        }
    }
}