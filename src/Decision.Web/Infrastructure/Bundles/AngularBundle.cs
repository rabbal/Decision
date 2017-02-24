using System.Web.Optimization;

namespace Decision.Web.Infrastructure.Bundles
{
    public sealed class AngularBundle : Bundle
    {
        public AngularBundle(string virtualPath)
            : base(virtualPath, new JsMinify())
        {
            Include("~/Scripts/angular.js")
                .Include("~/Client/app.js")
                .IncludeDirectory("~/Client/", "*.js", true);
        }
    }
}