using System.Web.Optimization;

namespace Decision.Web.Infrastructure.Bundles
{
    public sealed class AdminContentBundle : Bundle
    {
        public AdminContentBundle(string virtualPath)
            : base(virtualPath, new CssUrlTransform(), new CssMinify())
        {
            Include("~/Content/admin/style.css")
                .Include("~/Content/plugins/animate.css")
                .Include("~/Content/plugins/nprogress.css");
        }
    }
}