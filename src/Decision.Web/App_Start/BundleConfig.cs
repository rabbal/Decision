using System.Collections.Generic;
using System.Web.Optimization;

namespace Decision.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/libs/jquery-{version}.js",
                        "~/Scripts/libs/jquery.unobtrusive-ajax.min.js"));

            var jqueryVal = new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/libs/jqueryval-default.min.js")
                .Include("~/Scripts/libs/jquery.validate*"
                );
            jqueryVal.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(jqueryVal);

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/libs/modernizr-*"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/libs/bootstrap.min.js",
                "~/Scripts/libs/respond.min.js",
                "~/Scripts/libs/site.min.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
              "~/Content/style.css",
                "~/Content/site.min.css"));

            bundles.Add(new StyleBundle("~/Content/admin").Include(
                "~/Content/style.css",
                "~/Content/plugins/bootstrap-datepicker.min.css",
                "~/Content/plugins/fileinput.min.css",
                 "~/Content/site.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                "~/Scripts/libs/bootstrap.min.js",
                "~/Scripts/libs/respond.min.js",
                "~/Scripts/plugins/jquery.noty.packaged.min.js",
                "~/Scripts/plugins/jquery.dlmenu.min.js",
                "~/Scripts/plugins/masonry.pkgd.min.js",
                "~/Scripts/plugins/jquery.mvc.deleteItem.min.js",
                "~/Scripts/plugins/jquery.mvc.asyncLoad.min.js",
                "~/Scripts/plugins/jquery.mvc.scan.min.js",
                "~/Scripts/plugins/jquery.mvc.getJson.min.js",
                "~/Scripts/customs/noty.alerts.min.js",
                "~/Scripts/customs/ajax.methods.min.js",
                "~/Scripts/plugins/fileinput.min.js",
                "~/Scripts/plugins/jquery.farsiInput.min.js",
                "~/Scripts/customs/site.min.js"
                ));


            bundles.Add(new ScriptBundle("~/bundles/datePicker").Include(
                "~/Scripts/plugins/bootstrap-datepicker.min.js",
                "~/Scripts/plugins/bootstrap-datepicker.fa.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/rating").Include(
              "~/Scripts/plugins/star-rating.min.js"
              ));
            bundles.Add(new StyleBundle("~/Content/rating").Include(
            "~/Content/plugins/star-rating.min.css"));

            var ckeditor = new ScriptBundle("~/bundles/editor").Include(
                "~/Scripts/ckeditor/ckeditor.js")
                .Include("~/Scripts/ckeditor/adapters/jquery.js");

            ckeditor.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(ckeditor);

            bundles.Add(new ScriptBundle("~/bundles/formData").Include("~/Scripts/plugins/jquery.form.min.js",
                "~/Scripts/customs/jquery.form.config.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/mustache").Include(
                      "~/Scripts/plugins/mustache.min.js"));

            BundleTable.EnableOptimizations = true;

        }

    }
    class NonOrderingBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}
