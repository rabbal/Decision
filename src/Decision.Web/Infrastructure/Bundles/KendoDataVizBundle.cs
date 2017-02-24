using System.Web.Optimization;

namespace Decision.Web.Infrastructure.Bundles
{
    public sealed class KendoDataVizBundle : Bundle
    {
        public KendoDataVizBundle(string virtualPath)
            : base(virtualPath, new CssUrlTransform(), new CssMinify())
        {
            Include("~/Content/kendo/kendo.common.min.css")
                .Include("~/Content/kendo/kendo.rtl.min.css")
                .Include("~/Content/kendo/kendo.bootstrap.min.css")
                .Include("~/Content/kendo/kendo.dataviz.min.css")
                .Include("~/Content/kendo/kendo.dataviz.bootstrap.min.css");
        }
    }
}