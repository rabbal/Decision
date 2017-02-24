using System.Web.Optimization;
using Decision.Common.MvcToolkit.Bundles;

namespace Decision.Web.Infrastructure.Bundles
{
    public sealed class ScriptsBundle : Bundle
    {
        /// <summary>
        ///     failover Core Bundle
        /// </summary>
        /// <param name="virtualPath"></param>
        public ScriptsBundle(string virtualPath)
            : base(virtualPath, new JsMinify())
        {
            Orderer = new AsIsBundleOrderer();
            Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/plugins/icheck.min.js",
                    "~/Scripts/plugins/jquery.fs.selecter.min.js",
                    "~/Scripts/plugins/jquery.fs.stepper.min.js")
                .Include("~/Scripts/jquery.validate.js")
                .Include("~/Scripts/jquery.validate.*")
                .Include("~/Scripts/plugins/nprogress.js")
                .Include("~/Scripts/framework.js")
                .Include("~/Scripts/jquery.unobtrusive-ajax.js")
                .Include("~/Scripts/plugins/jquery.noty.packaged.min.js")
                .Include("~/Scripts/bootstrap.min.js")
                .Include("~/Scripts/respond.js");
        }
    }
}