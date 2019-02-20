using System.Web;
using System.Web.Optimization;

namespace Company.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/vendor/jquery-2.2.4.min.js",
                        "~/Scripts/vendor/bootstrap.min.js",
                        "~/Scripts/easing.min.js",
                        "~/Scripts/hoverIntent.js",
                        "~/Scripts/superfish.min.js",
                        "~/Scripts/mn-accordion.js",
                        "~/Scripts/jquery.ajaxchimp.min.js",
                        "~/Scripts/jquery.magnific-popup.min.js",
                        "~/Scripts/owl.carousel.min.js",
                        "~/Scripts/jquery.nice-select.min.js",
                        "~/Scripts/jquery.circlechart.js",
                        "~/Scripts/mail-script.js",
                        "~/Scripts/main.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/Content/bootstrap.css",
                    "~/Content/linearicons.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/bootstrap.css",
                      "~/Content/magnific-popup.css",
                      "~/Content/nice-select.css",
                      "~/Content/animate.min.css",
                      "~/Content/owl.carousel.css",
                      "~/Content/main.css"));
        }
    }
}
