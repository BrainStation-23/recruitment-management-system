(function(app) {
    "use strict";

    app.controller("PasswordController", [
        "$http", "notifierService", function($http, notifierService) {
            var vm = this;

            vm.save = function() {
                vm.form.submitted = true;

                if (vm.form.$valid) {

                    var model = {
                        id: vm.user.id,
                        oldPassword: vm.user.oldPassword,
                        newPassword: vm.user.newPassword,
                        confirmPassword: vm.user.confirmPassword,
                        __RequestVerificationToken: angular.element(":input:hidden[name*='RequestVerificationToken']").val()
                    };

                    $http.post("/Account/ChangePassword", model).success(function () {
                        notifierService.notifySuccess("Password updated.");
                    }).error(function(response) {
                        notifierService.notifyError(response);
                    });
                }
            };
        }
    ]);
})(angular.module("users"));