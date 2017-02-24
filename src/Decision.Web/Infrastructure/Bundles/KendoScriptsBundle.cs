using System.Web.Optimization;

namespace Decision.Web.Infrastructure.Bundles
{
    public sealed class KendoScriptsBundle : Bundle
    {
        public KendoScriptsBundle(string virtualPath)
            : base(virtualPath, new JsMinify())
        {
            Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/kendo/kendo.all.min.js")
                .Include("~/Scripts/cultures/kendo.culture.fa-IR.min.js")
                .Include("~/Scripts/cultures/kendo.culture.fa.min.js")
                .Include("~/Scripts/kendo/messages/kendo.fa-IR.js");
        }
    }
}