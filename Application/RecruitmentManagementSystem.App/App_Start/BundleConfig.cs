using System.Web.Optimization;

namespace RecruitmentManagementSystem.App
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/library").Include(
                "~/bower_components/jquery/dist/jquery.js",
                "~/bower_components/angular/angular.js",
                "~/bower_components/angular-messages/angular-messages.js",
                "~/bower_components/angular-bootstrap/ui-bootstrap-tpls.js",
                "~/bower_components/ng-file-upload/ng-file-upload.js",
                "~/bower_components/toastr/toastr.js",
                "~/bower_components/lodash/lodash.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/bower_components/jquery-validation/dist/jquery.validate.js",
                "~/bower_components/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/application").Include(
                "~/Scripts/core/config/config.js",
                "~/Scripts/core/config/init.js",
                "~/Scripts/core/core.module.js",
                "~/Scripts/core/services/core.file.service.js",
                "~/Scripts/core/services/core.notifier.service.js",
                "~/Scripts/core/directives/core.keyboard.enter.directive.js",
                "~/Scripts/questions/questions.module.js",
                "~/Scripts/questions/controllers/questions.controller.js",
                "~/Scripts/questions/constants/questions.constants.js",
                "~/Scripts/candidates/candidates.module.js",
                "~/Scripts/candidates/controllers/candidates.controller.js",
                "~/Scripts/candidates/controllers/candidates.education.controller.js",
                "~/Scripts/candidates/controllers/candidates.experience.controller.js",
                "~/Scripts/candidates/controllers/candidates.project.controller.js",
                "~/Scripts/accounts/accounts.module.js",
                "~/Scripts/accounts/controllers/accounts.controller.js",
                "~/Scripts/accounts/controllers/accounts.profile.controller.js",
                "~/Scripts/quiz/quiz.module.js",
                "~/Scripts/quiz/controllers/quiz.controller.js",
                "~/Scripts/quiz/controllers/quiz.question.controller.js",

                "~/Scripts/users/users.module.js",
                "~/Scripts/users/controllers/users.controller.js",
                "~/Scripts/users/controllers/users.profile.controller.js",
                "~/Scripts/users/controllers/users.password.controller.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/bower_components/bootstrap/dist/css/bootstrap.css",
                "~/bower_components/toastr/toastr.css",
                "~/bower_components/font-awesome/css/font-awesome.css",
                "~/Content/Styles/main.css"));
        }
    }
}