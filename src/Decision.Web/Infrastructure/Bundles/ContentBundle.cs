using System.Web.Optimization;

namespace Decision.Web.Infrastructure.Bundles
{
    public sealed class ContentBundle : Bundle
    {
        public ContentBundle(string virtualPath)
            : base(virtualPath, new CssMinify())
        {
            Include("~/Content/style.css",new CssRewriteUrlTransform())
                .Include("~/Content/site.css")
                .Include("~/Content/plugins/animate.css")
                .Include("~/Content/plugins/nprogress.css");
        }
    }
}