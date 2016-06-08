using System.Web.Optimization;

namespace RecruitmentManagementSystem.App
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/library").Include(
                "~/node_modules/jquery/dist/jquery.js",
                "~/node_modules/angular/angular.js",
                "~/node_modules/angular-messages/angular-messages.js",
                "~/node_modules/bootstrap/dist/js/bootstrap.js",
                "~/node_modules/angular-ui-bootstrap/dist/ui-bootstrap-tpls.js",
                "~/node_modules/ng-file-upload/dist/ng-file-upload.js",
                "~/node_modules/toastr/toastr.js",
                "~/node_modules/lodash/lodash.js",
                "~/node_modules/summernote/dist/summernote.js",
                "~/node_modules/angular-summernote/dist/angular-summernote.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/node_modules/jquery-validation/dist/jquery.validate.js",
                "~/node_modules/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js"));

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
                "~/Scripts/quiz/quiz.module.js",
                "~/Scripts/quiz/controllers/quiz.controller.js",
                "~/Scripts/quiz/controllers/quiz.question.controller.js",

                "~/Scripts/users/users.module.js",
                "~/Scripts/users/controllers/users.controller.js",
                "~/Scripts/users/controllers/users.education.modal.controller.js",
                "~/Scripts/users/controllers/users.experience.modal.controller.js",
                "~/Scripts/users/controllers/users.project.modal.controller.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/Styles/bootstrap-paper.css",
                "~/node_modules/font-awesome/css/font-awesome.css",
                "~/node_modules/summernote/dist/summernote.css",
                "~/node_modules/toastr/build/toastr.css",
                "~/Content/Styles/site.css"));
        }
    }
}
