using System.Collections.Generic;
using System.Web.Optimization;
using Decision.Web.Infrastructure.Bundles;
using Decision.Web.Infrastructure.Constants;

namespace Decision.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            AddCss(bundles);
            AddJavaScript(bundles);
        }

        private static void AddCss(BundleCollection bundles)
        {
            bundles.Add(new ContentBundle("~/Content/css"));
            
            bundles.Add(new KendoContentBundle("~/Content/Kendo"));

            bundles.Add(new KendoDataVizBundle("~/Content/dataviz"));

            bundles.Add(new AdminContentBundle("~/Content/admin"));
            
        }

        private static void AddJavaScript(BundleCollection bundles)
        {
            var modernizrBundle = new ScriptBundle(
                "~/bundles/modernizr",
                ContentDeliveryNetwork.Microsoft.ModernizrUrl)
                .Include("~/Scripts/modernizr-*");
            bundles.Add(modernizrBundle);

            bundles.Add(new ScriptsBundle("~/bundles/scripts"));

            bundles.Add(new AngularBundle("~/bundles/app"));
            
            bundles.Add(new KendoScriptsBundle("~/bundles/kendo"));

            bundles.Add(new ScriptBundle("~/bundles/admin"));
            
            bundles.Add(new MomentBundle("~/bundles/moment"));
        }
    }
}
