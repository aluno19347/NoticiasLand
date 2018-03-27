using System.Web;
using System.Web.Optimization;

namespace NoticiasLand
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Content/scripts/jquery.validate.unobtrusive.js",
                "~/Content/scripts/jquery.validate.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/scripts/bootstrap.js",
                      "~/Content/scripts/respond.matchmedia.addListener.js",
                      "~/Content/scripts/respond.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/css/boostrap/bootstrap-grid.css",
                      "~/Content/css/boostrap/bootstrap-reboot.css",
                      "~/Content/css/boostrap/bootstrap.css",
                      "~/Content/css/site.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
