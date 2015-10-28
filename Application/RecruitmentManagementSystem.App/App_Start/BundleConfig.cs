using System.Web.Optimization;

namespace RecruitmentManagementSystem.App
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/library").Include(
                "~/Vendors/bower_components/jquery/dist/jquery.js",
                "~/Vendors/bower_components/angular/angular.js",
                "~/Vendors/bower_components/angular-messages/angular-messages.js",
                "~/Vendors/bower_components/angular-bootstrap/ui-bootstrap-tpls.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Vendors/bower_components/jquery-validation/dist/jquery.validate.js",
                "~/Vendors/bower_components/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/application").Include(
                "~/Scripts/core/config/config.js",
                "~/Scripts/core/config/init.js",
                "~/Scripts/questions/questions.module.js",
                "~/Scripts/questions/controllers/questions.controller.js",
                "~/Scripts/candidates/candidates.module.js",
                "~/Scripts/candidates/controllers/candidates.controller.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Vendors/bower_components/bootstrap/dist/css/bootstrap.css",
                "~/Content/site.css"));
        }
    }
}