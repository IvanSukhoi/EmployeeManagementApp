using System.Web.Optimization;

namespace EmployeeManagement.WebUI
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
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/clientfeaturesscripts")
                    .Include("~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/handlebars-v4.0.11.js"));

            bundles.Add(new ScriptBundle("~/bundles/employee").Include(
                   "~/Scripts/employee.js"));

            bundles.Add(new ScriptBundle("~/bundles/department").Include(
                    "~/Scripts/department.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapstyle")
                .Include("~/Scripts/ajax-jquery-v3.2.0.min.js",
                         "~/Scripts/bootstrap-v3.3.7.min.js"
                ));
        }
    }
}
