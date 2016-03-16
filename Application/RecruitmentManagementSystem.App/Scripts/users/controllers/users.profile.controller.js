(function(app) {
    "use strict";

    app.controller("UsersProfileController", [
        "$http", "notifierService", function($http, notifierService) {
            var vm = this;

            vm.user = {};

            vm.find = function(id) {
                $http.get("/Account/PersonalInformation?userId=" + id).success(function(data) {
                    vm.user = data;
                }).error(function(response) {
                    notifierService.notifyError(response);
                });
            };

            vm.save = function() {
                vm.form.submitted = true;

                if (vm.form.$valid) {

                    var model = {
                        id: vm.user.id,
                        firstName: vm.user.firstName,
                        lastName: vm.user.lastName,
                        email: vm.user.email,
                        phoneNumber: vm.user.phoneNumber,
                        __RequestVerificationToken: angular.element(":input:hidden[name*='RequestVerificationToken']").val()
                    };

                    $http.post("/Account/PersonalInformation", model).success(function() {
                        notifierService.notifySuccess("Profile updated.");
                    }).error(function(response) {
                        notifierService.notifyError(response);
                    });
                }
            };
        }
    ]);
})(angular.module("users"));