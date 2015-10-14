using System.Web.Optimization;

namespace RecruitmentManagementSystem.App
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Vendors/bower_components/jquery/dist/jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Vendors/bower_components/jquery-validation/dist/jquery.validate.js",
                "~/Vendors/bower_components/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/application").Include(
                "~/Scripts/Question/create.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Vendors/bower_components/bootstrap/dist/js/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Vendors/bower_components/bootstrap/dist/css/bootstrap.css",
                "~/Content/site.css"));
        }
    }
}