using System.Web.Optimization;

namespace Decision.Web.Infrastructure.Bundles
{
    public sealed class KendoContentBundle : Bundle
    {
        public KendoContentBundle(string virtualPath)
            : base(virtualPath, new CssUrlTransform(), new CssMinify())
        {
            Include("~/Content/kendo/kendo.common.min.css")
                .Include("~/Content/kendo/kendo.rtl.min.css")
                .Include("~/Content/kendo/kendo.bootstrap.min.css");
        }
    }
}