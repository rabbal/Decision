using System.Web.Optimization;

namespace Decision.Web.Infrastructure.Bundles
{
    public sealed class MomentBundle : Bundle
    {
        public MomentBundle(string virtualPath)
            : base(virtualPath, new JsMinify())
        {
            Include("~/Scripts/cultures/moment.min.js")
                .Include("~/Scripts/cultures/moment-jalaali.js");
        }
    }
}